using Remis.CORE.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remis.CORE.Interfaces
{
    public interface IRepositorioChofer
    {
        
        // CONSULTAS        
        Task<IEnumerable<Chofer>> ObtenerTodosAsync();
        Task<Chofer> ObtenerPorIdAsync(int id_);
        Task<Chofer> ObtenerPorLicenciaAsync(string nroLicencia_);
        Task<Chofer> ObtenerPorPersonaAsync(int idPersona_);
        Task<IEnumerable<Chofer>> BuscarAsync(string buscar_);
                
        // ESCRITURA
        
        Task<int> CrearAsync(Chofer chofer_);
        Task<bool> ActualizarAsync(Chofer chofer_);
        Task<bool> EliminarAsync(int id_);

        // VALIDACIONES        
        Task<bool> ExisteLicenciaAsync(string nroLicencia_, int? idExcluir = null);
        Task<bool> ExistePersonaAsync(int idPersona_, int? idExcluir = null);
    }
}
