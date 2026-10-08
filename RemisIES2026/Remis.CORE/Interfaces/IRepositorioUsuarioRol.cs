using Remis.CORE.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remis.CORE.Interfaces
{
    public interface IRepositorioUsuarioRol
    {
        Task<IEnumerable<UsuarioRol>> ObtenerPorUsuarioAsync(int idUsuario_);
        Task<IEnumerable<UsuarioRol>> ObtenerPorRolAsync(int idRol_);
        Task<bool> AsignarAsync(int idUsuario_, int idRol_);
        Task<bool> EliminarAsync(int idUsuario_, int idRol_);
        Task<bool> EliminarTodosPorUsuarioAsync(int idUsuario_);
    }
}
