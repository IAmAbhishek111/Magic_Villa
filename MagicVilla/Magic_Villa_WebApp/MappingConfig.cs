using AutoMapper;
using Magic_Villa_WebApp.Models.Dto;


namespace Magic_Villa_WebApp
{
    public class MappingConfig: Profile
    {

        public MappingConfig() 
        {
            CreateMap<VillaDto, CreateVillaDto>().ReverseMap();
            CreateMap<VillaDto, UpdateVillaDto>().ReverseMap();

            CreateMap<VillaNumberDto, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberUpdateDto>().ReverseMap();

        }

    }
}
