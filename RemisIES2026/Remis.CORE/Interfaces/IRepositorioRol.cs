using Remis.CORE.Entidades;

namespace Remis.CORE.Interfaces
{
    public interface IRepositorioRol
    {
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
        Task<Rol?> ObtenerPorIdAsync(int id_);
        Task<Rol?> ObtenerPorNombreAsync(string nombre_);
        Task<int> CrearAsync(Rol rol_);
        Task<bool> ActualizarAsync(Rol rol_);
        Task<bool> EliminarAsync(int id_);
        Task<bool> ExisteNombreAsync(string nombre_, int? idExcluir = null);
    }
}
