using AutoMapper;
using Magic_Villa_Api.Models;
using Magic_Villa_Api.Models.Dto;

namespace Magic_Villa_Api
{
    public class MappingConfig: Profile
    {

        public MappingConfig() 
        { 
        
            //mapping synatx
            CreateMap<Villa , VillaDto>();
            CreateMap<VillaDto, Villa>();


            CreateMap<Villa , CreateVillaDto>().ReverseMap();
            CreateMap<Villa, UpdateVillaDto>().ReverseMap();


        }

    }
}
