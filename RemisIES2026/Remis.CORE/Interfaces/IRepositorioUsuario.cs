using Remis.CORE.Entidades;

namespace Remis.CORE.Interfaces
{
    public interface IRepositorioUsuario
    {
        // Consultas
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id_);
        Task<Usuario?> ObtenerPorUsuarioAsync(string usuario_);

        // Escritura
        Task<int> CrearAsync(Usuario usuario_);
        Task<bool> ActualizarAsync(Usuario usuario_);
        Task<bool> EliminarAsync(int id_);

        // Validaciones
        Task<bool> ExisteUsuarioAsync(string usuario_, int? idExcluir = null);

        // Roles
        Task<IEnumerable<Rol>> ObtenerRolesAsync(int idUsuario_);
        Task<bool> AsignarRolAsync(int idUsuario_, int idRol_);
        Task<bool> EliminarRolesAsync(int idUsuario_);

        // Login
        Task ActualizarUltimoLoginAsync(int idUsuario_);
    }
}
