using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaServicio.Api.CarritoCompra.Aplicacion;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.DTOS;
using System;

namespace TiendaServicio.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class CarritoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarritoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoSesionDTO>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSesionId = id });
        }

    }
}
