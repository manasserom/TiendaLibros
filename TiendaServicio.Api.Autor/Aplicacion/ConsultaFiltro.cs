using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicio.Api.Autor.DTOS;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            public readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await
                    _contexto.AutorLibros.Where(x => x.AutorLibroGuid == request.AutorGuid)
                    .FirstOrDefaultAsync();

                //mapeador
                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);

                if (autorDto == null)
                {
                    throw new Exception("No se encontró el autor solicitado");
                }

                return autorDto;

            }
        }
    }
}
