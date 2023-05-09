using AutoMapper;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.DTOS;

namespace TiendaServicio.Api.Autor.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //mapeamos
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
