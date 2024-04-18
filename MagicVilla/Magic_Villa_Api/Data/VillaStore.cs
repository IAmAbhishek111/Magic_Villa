using Magic_Villa_Api.Models.Dto;

namespace Magic_Villa_Api.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto> {
            new VillaDto { Id = 1, Name= "Pool View"},
            new VillaDto { Id = 2, Name = "Beach View"}

            };
    }
}
