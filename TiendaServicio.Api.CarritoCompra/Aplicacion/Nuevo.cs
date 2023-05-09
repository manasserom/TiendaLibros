using AutoMapper;
using MediatR;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Persistencia;
using TiendaServicio.Api.CarritoCompra.DTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace TiendaServicio.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCompra { get; set; }
            public List<string> ProductosSeleccionado { get; set; }
            //public int CarritoSesionId { get; set; } //utilizado para cargar un datos a un carrito existentes
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly CarritoContexto _contexto;
            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                    var carrito = new CarritoSesion
                    {
                        FechaCreacion = DateTime.Now,                        
                    };
                    _contexto.CarritoCompras.Add(carrito);
                var valor = await _contexto.SaveChangesAsync();

                //_contexto.SaveChanges();
                valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                {
                    throw new Exception("No se pudo insertar el ---Carrito Sesion---");

                }
                foreach(var item in request.ProductosSeleccionado)
                {
                    var detalle = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = carrito.CarritoSesionId,
                        ProductoSeleccionado = item
                    };
                    _contexto.CarritoSesionDetalles.Add(detalle);
                }
                valor = await _contexto.SaveChangesAsync();
                if(valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el ---Carrito Detakke---");
                //Si hubiera algun error
                #region NotUsedCode
                //codigo original no utilizado debido a que durante la clase se realizo de forma diferente

                //int aux = 0;
                ////En caso de que no exista una sesion 
                ////if (request.CarritoSesionId == 0)
                ////{
                //    var carrito = new CarritoSesion
                //    {
                //        FechaCreacion = DateTime.Now,                        
                //    };
                //    _contexto.CarritoCompras.Add(carrito);
                ////podria ser asincronico    
                //_contexto.SaveChanges();
                //    aux = carrito.CarritoSesionId;
                //}
                //Utilizar el nuevo valor o el dado por el usuario
                //int SesionId = request.CarritoSesionId == 0 ?
                //aux : request.CarritoSesionId;
                ////crear el nuevo Carrito
                //var detalle = new CarritoSesionDetalle
                //{   
                //    FechaCreacion = DateTime.Now,
                //    ProductoSeleccionado = request.ProductoSeleccionado,
                //    CarritoSesionId = SesionId,                  
                //};

                ////Guardar cambios para luego obtener
                //var valor = await _contexto.SaveChangesAsync();

                ////usamos el contexto para agregar al nuevo carrito
                //_contexto.CarritoSesionDetalles.Add(detalle);

                //valor = await _contexto.SaveChangesAsync();
                //if (valor > 0)
                //{
                //    return Unit.Value;
                //}
                ////Si hubiera algun error
                //throw new Exception("No se pudo insertar el ---Carrito---");                

                #endregion


            }
        }
    }
}
