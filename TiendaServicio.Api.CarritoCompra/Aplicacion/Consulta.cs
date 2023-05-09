using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicio.Api.CarritoCompra.DTOS;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Persistencia;
using TiendaServicio.Api.CarritoCompra.ServicioRemoto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using TiendaServicio.Api.CarritoCompra.InterfazRemota;
using System;

namespace TiendaServicio.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoSesionDTO>
        {
            public int CarritoSesionId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, CarritoSesionDTO>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibroServices _servicio;
            public Manejador(CarritoContexto contexto, ILibroServices servicio)
            {
                _contexto = contexto;
                _servicio = servicio;
            }

            public async Task<CarritoSesionDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Carrito
                var carritoSesion = await
                    _contexto.CarritoCompras
                    .FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);

                //Detalle
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalles
                    .Where(x => x.CarritoSesionId == request.CarritoSesionId)
                    .ToListAsync();
                //Lista
                var listaDetalleDto = new List<CarritoSesionDetalleDTO>();

                foreach (var libro in carritoSesionDetalle)
                {
                    //llamada
                    
                    var response = await _servicio.GetLibro( Guid.Parse(libro.ProductoSeleccionado));
                    //Guid aux = new System.Guid(libro.ProductoSeleccionado);
                    //var response = await _servicio.GetLibro(aux);//await _servicio.GetLibro(new System.Guid(libro.ProductoSeleccionado));
                    //var response = await _servicio.GetLibro(new System.Guid(libro.CarritoSesionId));
                    if (response.resultado)
                    {
                        var LibroRemoto = response.Libro;
                        var carritoDetalle = new CarritoSesionDetalleDTO
                        {
                            TituloLibro = LibroRemoto.Titulo,
                            FechaPublicacion = LibroRemoto.FechaPublicacion,
                            LibroId = LibroRemoto.LibreriaMaterialId,
                            AutorLibro = LibroRemoto.AutorLibro.ToString(),
                            //titilo
                            //titulo = objetoLibro.Titulo,
                            //FechaPublicacion = objetoLibro.FechaPublicacion,
                            //LibroId = objetoLibro.LibreriaMaterialId
                        };

                        listaDetalleDto.Add(carritoDetalle);
                    }
                }
                //estamos devolviendo un carritodto
                var carritoDto = new CarritoSesionDTO
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaDetalleDto
                };

                return carritoDto;


                //throw new System.NotImplementedException();
            }
        }
    }
}
