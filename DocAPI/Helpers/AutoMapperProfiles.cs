using DocAPI.DTOs;
using DocAPI.Entities;
using System;
using AutoMapper;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DocAPI.Helpers
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();

            CreateMap<UpdateHistoriesDTO, UpdateDocHistory>().ReverseMap();
            CreateMap<UpdateHistoriesCreationDTO, UpdateDocHistory>()
                .ForMember(x => x.Logo, options => options.Ignore());
            //.ForMember(x => x.DocumetRelation, options => options.MapFrom(MapDocumetRelation));

            CreateMap<DocumentDTO, Document>().ReverseMap();
            CreateMap<DocumentCreationDto, Document>()
                .ForMember(x => x.Logo, options => options.Ignore());

            CreateMap<IdentityUser, UserDTO>();
        }
        


    }
}
