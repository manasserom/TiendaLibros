using AutoMapper;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.DTOS;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //mapeamos
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
