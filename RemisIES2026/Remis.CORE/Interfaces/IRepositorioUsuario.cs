using Remis.CORE.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remis.CORE.Interfaces
{
    public interface IRepositorioUsuario
    {
        // ═══════════════════════════════════════════════════════
        // CONSULTAS
        // ═══════════════════════════════════════════════════════
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id_);
        Task<Usuario?> ObtenerPorUsuarioAsync(string usuario_);
        Task<Usuario?> ObtenerPorPersonaAsync(int idPersona_);

        // ═══════════════════════════════════════════════════════
        // ESCRITURA
        // ═══════════════════════════════════════════════════════
        Task<int> CrearAsync(Usuario usuario_);
        Task<bool> ActualizarAsync(Usuario usuario_);
        Task<bool> EliminarAsync(int id_);

        // ═══════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════
        Task<bool> ExisteUsuarioAsync(string usuario_, int? idExcluir = null);

        // ═══════════════════════════════════════════════════════
        // ROLES
        // ═══════════════════════════════════════════════════════
        Task<IEnumerable<Rol>> ObtenerRolesAsync(int idUsuario_);
        Task<bool> AsignarRolAsync(int idUsuario_, int idRol_);
        Task<bool> EliminarRolesAsync(int idUsuario_);
    }
}
