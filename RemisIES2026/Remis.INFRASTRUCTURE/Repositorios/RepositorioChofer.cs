using Dapper;
using Remis.CORE.Entidades;
using Remis.CORE.Interfaces;
using Remis.INFRASTRUCTURE.Contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Remis.INFRASTRUCTURE.Repositorios
{
    public class RepositorioChofer : IRepositorioChofer
    {
        private readonly IMyConexion conexion_;
        
        public RepositorioChofer(IMyConexion cnn)
        {
            conexion_ = cnn;
        }


        public Task<bool> ActualizarAsync(Chofer chofer_)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Chofer>> BuscarAsync(string buscar_)
        {
            throw new NotImplementedException();
        }

        public Task<int> CrearAsync(Chofer chofer_)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id_)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExisteLicenciaAsync(string nroLicencia_, int? idExcluir = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistePersonaAsync(int idPersona_, int? idExcluir = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Chofer> ObtenerPorIdAsync(int id_)
        {
            using var conexionDapper = await conexion_.obtenerConexionAsync();

            var parametro = new DynamicParameters();
            parametro.Add("_id_", id_, DbType.Int32, ParameterDirection.Input);
            var chofer = await conexionDapper.QueryFirstOrDefaultAsync<Chofer>(
                "sp_choferes_obtener_por_id", parametro,
                commandType: CommandType.StoredProcedure);
           
            return (chofer);
        }

        public Task<Chofer> ObtenerPorLicenciaAsync(string nroLicencia_)
        {
            throw new NotImplementedException();
        }

        public Task<Chofer> ObtenerPorPersonaAsync(int idPersona_)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Chofer>> ObtenerTodosAsync()
        {
            using var connectionDapper = await conexion_.obtenerConexionAsync();

            var choferes = await connectionDapper.QueryAsync<Chofer>("sp_choferes_obtener_todos", commandType: CommandType.StoredProcedure);

            return (choferes);            
        }
    }
}
