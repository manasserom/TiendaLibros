using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;
            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }
            
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //crear eel nuevo autorlibro
                var autorLibro = new AutorLibro
                {
                    Nombre= request.Nombre,
                    Apellido= request.Apellido,
                    FechaNacimiento= request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid()),
                };

                //usamos el contexto para agregar al nuevo autor libro
                _contexto.AutorLibros.Add(autorLibro);

                var valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                //si hubiera algun error
                throw new Exception("No se pudo insertar el autor");
            }
        }
    }

    
}
