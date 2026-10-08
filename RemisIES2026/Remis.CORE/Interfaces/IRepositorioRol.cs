using Remis.CORE.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remis.CORE.Interfaces
{
    public interface IRepositorioRol
    {
        // ═══════════════════════════════════════════════════════
        // CONSULTAS
        // ═══════════════════════════════════════════════════════
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
        Task<Rol?> ObtenerPorIdAsync(int id_);
        Task<Rol?> ObtenerPorNombreAsync(string nombre_);

        // ═══════════════════════════════════════════════════════
        // ESCRITURA
        // ═══════════════════════════════════════════════════════
        Task<int> CrearAsync(Rol rol_);
        Task<bool> ActualizarAsync(Rol rol_);
        Task<bool> EliminarAsync(int id_);

        // ═══════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════
        Task<bool> ExisteNombreAsync(string nombre_, int? idExcluir = null);
    }
}
