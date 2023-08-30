using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto {Id =1, Nombre= "Primera Villa"},
            new VillaDto {Id =2, Nombre= "Segunda Villa"}
        }; 
    }
}
