using AutoMapper;
using DocAPI.DTOs;
using DocAPI.Entities;
using DocAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace DocAPI.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentController: Controller
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "document";
        private readonly UserManager<IdentityUser> userManager;

        public DocumentController(ApplicationDbContext context, IMapper mapper,
            IFileStorageService fileStorageService, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            
            var document = await context.Documents.ToListAsync();
            return Json(mapper.Map<List<DocumentDTO>>(document));
        }


        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<DocumentDTO>> Get(int id)
        {
            //var max = db.Products.OrderByDescending(p => p.ID).FirstOrDefault().ID;
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await userManager.FindByEmailAsync(email);
            var documents = await context.Documents.OrderByDescending(p => p.Id).FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (documents == null)
            {
                return NotFound($"Employee with Id = {id} not found");
            }

            return mapper.Map<DocumentDTO>(documents);
        }

        [HttpGet("searchByTitle/{query}")]
        public async Task<ActionResult<List<DocumentDTO>>> SearchByName(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) { return new List<DocumentDTO>(); }

            return await context.Documents
                .Where(x => x.Title.Contains(query))
                .OrderBy(x => x.Title)
                .Select(x => new DocumentDTO {  Title = x.Title,  Body =x.Body,
                    NumberOfCopy= x.NumberOfCopy , IsUpdated=x.IsUpdated,CreatedDate=x.CreatedDate,IsArchived=x.IsArchived,
                SerialNumber=x.SerialNumber})
                .Take(5)
                .ToListAsync();
        }



        [HttpGet("filter")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<DocumentDTO>>> Filter([FromQuery] FilterDocumentDTO filterDocumentDTO)
        {
            //var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await userManager.FindByEmailAsync(filterDocumentDTO.email);
            var userId = user.Id;
            var documentsQueryable = context.Documents.AsQueryable();

            if (!string.IsNullOrEmpty(filterDocumentDTO.Title))
            {
                documentsQueryable = documentsQueryable.Where(x => x.Title.Contains(filterDocumentDTO.Title));
            }

            if (!string.IsNullOrEmpty(filterDocumentDTO.Title))
            {
                documentsQueryable = documentsQueryable.Where(x => x.Body.Contains(filterDocumentDTO.Title));

            }

            if (userId != null)
                {

                documentsQueryable = documentsQueryable.Where(x => x.UserId.Contains(userId));
            }

            await HttpContext.InsertParametersPaginationInHeader(documentsQueryable);
            var document = await documentsQueryable.OrderByDescending(x => x.CreatedDate).ToListAsync();
            return mapper.Map<List<DocumentDTO>>(document);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] DocumentCreationDto documentCreationDto)
        {
            var document = await context.Documents.FirstOrDefaultAsync(x => x.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            document = mapper.Map(documentCreationDto, document);

            if (documentCreationDto.Logo != null)
            {
                document.Logo = await fileStorageService.EditFile(containerName,
                        documentCreationDto.Logo, document.Logo);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromForm] DocumentCreationDto documentCreationDto)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

            if(userId != null) { 
            var document = new Document();
            
            document.Title = documentCreationDto.Title;
            document.Body = documentCreationDto.Body;
            document.NumberOfCopy = documentCreationDto.NumberOfCopy;
            document.SerialNumber = documentCreationDto.SerialNumber;
            document.IsUpdated = documentCreationDto.IsUpdated;
            document.IsArchived = documentCreationDto.IsArchived;
            document.CreatedDate = documentCreationDto.CreatedDate;
            document.UserId = userId;

            if (documentCreationDto.Logo != null)
            {
                document.Logo = await fileStorageService.SaveFile(containerName, documentCreationDto.Logo);
            }

            context.Add(document);
            }
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var document = await context.Documents.FirstOrDefaultAsync(x => x.Id == id);
            
            if (document == null)
            {
                return NotFound($"Employee with Id = {id} not found");
            }

            context.Remove(document);
            await context.SaveChangesAsync();
            //await fileStorageService.DeleteFile(document.Logo, containerName);
            return NoContent();
        }


    }
}
