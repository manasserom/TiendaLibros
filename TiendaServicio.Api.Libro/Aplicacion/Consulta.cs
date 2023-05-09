using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicio.Api.Libro.DTOS;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;


namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class ListaLibroMaterial : IRequest<List<LibroMaterialDto>>
        {

        }

        public class Manejador : IRequestHandler<ListaLibroMaterial, List<LibroMaterialDto>>
        {

            public readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDto>> Handle(ListaLibroMaterial request, CancellationToken cancellationToken)
            {
                //var autoresAll = await _contexto.AutorLibros.ToListAsync();
                //return autoresAll;
                var libroAll = await _contexto.LibreriaMateriales.ToListAsync();
                //usamos nuestro mapeador
                var libroDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(libroAll);

                return libroDto;
            }
        }
    }
}
