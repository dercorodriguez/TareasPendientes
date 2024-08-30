using TareasServer.Clases;

namespace Tareas.Cliente.Services
{
    public interface ITareaServices
    {
        Task<List<TareasDA>> Lista();
        Task<TareasDA> Buscar(int id);
        Task<int> Guardar(TareasDA tarea);
        Task<int> Editar(TareasDA tarea);
        Task<bool> Eliminar(int id);
    }
}
