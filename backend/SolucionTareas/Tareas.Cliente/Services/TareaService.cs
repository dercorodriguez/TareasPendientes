using TareasServer.Clases;
using System.Net.Http.Json;
using System.Threading;

namespace Tareas.Cliente.Services
{
    public class TareaService: ITareaServices
    {
        private readonly HttpClient _httpCliente;

        public TareaService(HttpClient _http)
        {
            _httpCliente = _http;
        }

        public async Task<List<TareasDA>> Lista()
        {
            var result = await _httpCliente.GetFromJsonAsync<ResponseAPI<List<TareasDA>>>("/api/Tareas/Consulta");
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result!.Mensaje);
            }
        }
        public async Task<TareasDA> Buscar(int id)
        {
            var result = await _httpCliente.GetFromJsonAsync<ResponseAPI<TareasDA>>($"/api/Tareas/Buscar/{id}");
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result!.Mensaje);
            }
        }

        public async Task<int> Guardar(TareasDA tarea)
        {
            var result = await _httpCliente.PostAsJsonAsync($"/api/Tareas/Grabar", tarea);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response!.Mensaje);
            }
        }

        public async Task<int> Editar(TareasDA tarea)
        {
            var result = await _httpCliente.PutAsJsonAsync($"/api/Tareas/Editar/{tarea.Id}", tarea);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response!.Mensaje);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _httpCliente.DeleteAsync($"/api/Tareas/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response!.EsCorrecto;
            }
            else
            {
                throw new Exception(response!.Mensaje);
            }
        }
        
    }
}
