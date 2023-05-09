using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicio.Api.Libro.DTOS;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroMaterialUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibreriaMaterialId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroMaterialUnico, LibroMaterialDto>
        {
            public readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDto> Handle(LibroMaterialUnico request, CancellationToken cancellationToken)
            {
                var libro = await
                    _contexto.LibreriaMateriales.Where(x => x.LibreriaMaterialId == request.LibreriaMaterialId)
                    .FirstOrDefaultAsync();

                //mapeador
                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);

                if (libroDto == null)
                {
                    throw new Exception("No se encontró el ---Material--- solicitado");
                }

                return libroDto;

            }
        }
    }
}
