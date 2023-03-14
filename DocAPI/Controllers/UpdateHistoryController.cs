using AutoMapper;
using DocAPI.DTOs;
using DocAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocAPI.Controllers
{
    [Route("api/updatehistories")]
    [ApiController]
    public class UpdateHistoryController: ControllerBase
    {
        private readonly string containerName = "document";
        private readonly IFileStorageService fileStorageService;
        private readonly ILogger<GenresController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;


        public UpdateHistoryController(ILogger<GenresController> logger, ApplicationDbContext context, IMapper mapper)
        {

            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UpdateHistoriesDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.UpdateDocHistories.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var updatehistory = await queryable.OrderBy(x => x.Title).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<UpdateHistoriesDTO>>(updatehistory);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UpdateHistoriesDTO>> Get(int id)
        {
            var updatehistory = await context.UpdateDocHistories.FirstOrDefaultAsync(x => x.Id == id);

            if (updatehistory == null)
            {
                return NotFound();
            }

            return mapper.Map<UpdateHistoriesDTO>(updatehistory);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] UpdateHistoriesCreationDTO UHCreationDTO)
        {
            var updatehistory = mapper.Map<UpdateHistoriesDTO>(UHCreationDTO);

            if (UHCreationDTO != null)
            {
                updatehistory.Logo = await fileStorageService.SaveFile(containerName, UHCreationDTO.Logo);
            }

            context.Add(updatehistory);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var updatehistory = await context.UpdateDocHistories.FirstOrDefaultAsync(x => x.Id == id);

            if (updatehistory == null)
            {
                return NotFound();
            }

            context.Remove(updatehistory);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(updatehistory.Logo, containerName);
            return NoContent();
        }

    }
}
