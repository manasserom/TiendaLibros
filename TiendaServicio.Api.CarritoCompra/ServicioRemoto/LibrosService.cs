using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicio.Api.CarritoCompra.InterfazRemota;
using TiendaServicio.Api.CarritoCompra.ModeloRemoto;

namespace TiendaServicio.Api.CarritoCompra.ServicioRemoto
{
    public class LibrosService : ILibroServices
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> _logger;
        public LibrosService(IHttpClientFactory httpClient, ILogger<LibrosService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<(bool resultado, LibroRemoto Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                //Conexion
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"/api/LibreriaMaterial/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var resultado = JsonSerializer.Deserialize<LibroRemoto>(contenido, options);
                    return (true, resultado, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return (false, null, e.Message);

                
            }
        }
    }
}
