using MySqlConnector;
using Remis.CORE.Entidades;
using Remis.CORE.Interfaces;
using Remis.INFRASTRUCTURE.Contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Remis.INFRASTRUCTURE.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly IMyConexion _conn;

        public RepositorioUsuario(IMyConexion conn)
        {
            _conn = conn;
        }
        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_obtener_todos";

                using IDataReader reader = _comando.ExecuteReader();

                List<Usuario> listaUsuarios = new List<Usuario>();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = reader.GetInt32(0);
                    usuario.IdPersona = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                    usuario.UsuarioNombre = reader.IsDBNull(3) ? null : reader.GetString(3);
                    usuario.PasswordHash = reader.IsDBNull(4) ? null : reader.GetString(4);
                    usuario.UltimoLogin = reader.IsDBNull(5) ? null : reader.GetDateTime(5);
                    usuario.FechaCreacion = reader.IsDBNull(6) ? null : reader.GetDateTime(6);
                    usuario.FechaActualizacion = reader.IsDBNull(7) ? null : reader.GetDateTime(7);
                    usuario.Activo = reader.IsDBNull(8) ? null : reader.GetBoolean(8);

                    listaUsuarios.Add(usuario);
                }

                return listaUsuarios;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return Enumerable.Empty<Usuario>();
            }
        } // fin del método ObtenerTodosAsync

        public async Task<Usuario?> ObtenerPorIdAsync(int id_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_obtener_por_id";

                IDbDataParameter parametro = _comando.CreateParameter();
                parametro.ParameterName = "_id";
                parametro.Value = id_;
                _comando.Parameters.Add(parametro);

                using IDataReader reader = _comando.ExecuteReader();

                if (reader.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = reader.GetInt32(0);
                    usuario.IdPersona = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                    usuario.UsuarioNombre = reader.IsDBNull(3) ? null : reader.GetString(3);
                    usuario.PasswordHash = reader.IsDBNull(4) ? null : reader.GetString(4);
                    usuario.UltimoLogin = reader.IsDBNull(5) ? null : reader.GetDateTime(5);
                    usuario.FechaCreacion = reader.IsDBNull(6) ? null : reader.GetDateTime(6);
                    usuario.FechaActualizacion = reader.IsDBNull(7) ? null : reader.GetDateTime(7);
                    usuario.Activo = reader.IsDBNull(8) ? null : reader.GetBoolean(8);

                    return usuario;
                }

                return null;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return null;
            }
        } // fin del método ObtenerPorIdAsync

        public async Task<Usuario?> ObtenerPorUsuarioAsync(string usuario_) // Lo utilizamos para el login
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_obtener_por_usuario";

                IDbDataParameter parametro = _comando.CreateParameter();
                parametro.ParameterName = "_usuario";
                parametro.Value = usuario_;
                _comando.Parameters.Add(parametro);

                using IDataReader reader = _comando.ExecuteReader();

                if (reader.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = reader.GetInt32(0);
                    usuario.IdPersona = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                    usuario.UsuarioNombre = reader.IsDBNull(3) ? null : reader.GetString(3);
                    usuario.PasswordHash = reader.IsDBNull(4) ? null : reader.GetString(4);
                    usuario.UltimoLogin = reader.IsDBNull(5) ? null : reader.GetDateTime(5);
                    usuario.FechaCreacion = reader.IsDBNull(6) ? null : reader.GetDateTime(6);
                    usuario.FechaActualizacion = reader.IsDBNull(7) ? null : reader.GetDateTime(7);
                    usuario.Activo = reader.IsDBNull(8) ? null : reader.GetBoolean(8);

                    return usuario;
                }

                return null;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return null;
            }
        } // fin del método ObtenerPorUsuarioAsync

        public async Task<int> CrearAsync(Usuario usuario_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_crear";

                IDbDataParameter parametroIdPersona = _comando.CreateParameter();
                parametroIdPersona.ParameterName = "_id_persona";
                parametroIdPersona.Value = usuario_.IdPersona ?? (object)DBNull.Value;
                _comando.Parameters.Add(parametroIdPersona);

                IDbDataParameter parametroUsuario = _comando.CreateParameter();
                parametroUsuario.ParameterName = "_usuario";
                parametroUsuario.Value = usuario_.UsuarioNombre;
                _comando.Parameters.Add(parametroUsuario);

                IDbDataParameter parametroPassword = _comando.CreateParameter();
                parametroPassword.ParameterName = "_password_hash";
                parametroPassword.Value = usuario_.PasswordHash;
                _comando.Parameters.Add(parametroPassword);

                IDbDataParameter parametroActivo = _comando.CreateParameter();
                parametroActivo.ParameterName = "_activo";
                parametroActivo.Value = usuario_.Activo;
                _comando.Parameters.Add(parametroActivo);

                int resultado = Convert.ToInt32(_comando.ExecuteScalar());

                return resultado;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return 0;
            }
        } // fin del método CrearAsync

        public async Task<bool> ActualizarAsync(Usuario usuario_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_actualizar";

                IDbDataParameter parametroId = _comando.CreateParameter();
                parametroId.ParameterName = "_id";
                parametroId.Value = usuario_.IdUsuario;
                _comando.Parameters.Add(parametroId);

                IDbDataParameter parametroIdPersona = _comando.CreateParameter();
                parametroIdPersona.ParameterName = "_id_persona";
                parametroIdPersona.Value = usuario_.IdPersona ?? (object)DBNull.Value;
                _comando.Parameters.Add(parametroIdPersona);

                IDbDataParameter parametroUsuario = _comando.CreateParameter();
                parametroUsuario.ParameterName = "_usuario";
                parametroUsuario.Value = usuario_.UsuarioNombre;
                _comando.Parameters.Add(parametroUsuario);

                IDbDataParameter parametroPassword = _comando.CreateParameter();
                parametroPassword.ParameterName = "_password_hash";
                parametroPassword.Value = usuario_.PasswordHash ?? (object)DBNull.Value;
                _comando.Parameters.Add(parametroPassword);

                IDbDataParameter parametroActivo = _comando.CreateParameter();
                parametroActivo.ParameterName = "_activo";
                parametroActivo.Value = usuario_.Activo;
                _comando.Parameters.Add(parametroActivo);

                int filasAfectadas = Convert.ToInt32(_comando.ExecuteScalar());

                return filasAfectadas > 0;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return false;
            }
        }// fin del método ActualizarAsync

        public async Task<bool> EliminarAsync(int id_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_eliminar";

                IDbDataParameter parametro = _comando.CreateParameter();
                parametro.ParameterName = "_id";
                parametro.Value = id_;
                _comando.Parameters.Add(parametro);

                int filasAfectadas = Convert.ToInt32(_comando.ExecuteScalar());

                return filasAfectadas > 0;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return false;
            }
        } // fin del método EliminarAsync

        public async Task<bool> ExisteUsuarioAsync(string usuario_, int _id)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_existe";

                IDbDataParameter parametroUsuario = _comando.CreateParameter();
                parametroUsuario.ParameterName = "_usuario";
                parametroUsuario.Value = usuario_;
                _comando.Parameters.Add(parametroUsuario);

                IDbDataParameter parametroIdExcluir = _comando.CreateParameter();
                parametroIdExcluir.ParameterName = "_id";
                parametroIdExcluir.Value = _id;
                _comando.Parameters.Add(parametroIdExcluir);

                int resultado = Convert.ToInt32(_comando.ExecuteScalar());

                return resultado == 1;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return false;
            }
        } // fin del método ExisteUsuarioAsync

        public async Task<bool> AsignarRolAsync(int idUsuario_, int idRol_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_asignar_rol";

                IDbDataParameter parametroUsuario = _comando.CreateParameter();
                parametroUsuario.ParameterName = "_id_usuario";
                parametroUsuario.Value = idUsuario_;
                _comando.Parameters.Add(parametroUsuario);

                IDbDataParameter parametroRol = _comando.CreateParameter();
                parametroRol.ParameterName = "_id_rol";
                parametroRol.Value = idRol_;
                _comando.Parameters.Add(parametroRol);

                int filasAfectadas = _comando.ExecuteNonQuery();

                return filasAfectadas > 0;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return false;
            }
        } // fin del método AsignarRolAsync

        public async Task<bool> EliminarRolesAsync(int idUsuario_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_eliminar_roles";

                IDbDataParameter parametro = _comando.CreateParameter();
                parametro.ParameterName = "_id_usuario";
                parametro.Value = idUsuario_;
                _comando.Parameters.Add(parametro);

                int filasAfectadas = _comando.ExecuteNonQuery();

                return filasAfectadas > 0;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return false;
            }
        } // fin del método EliminarRolesAsync

        public async Task<IEnumerable<Rol>> ObtenerRolesAsync(int idUsuario_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_obtener_roles";

                IDbDataParameter parametro = _comando.CreateParameter();
                parametro.ParameterName = "_id_usuario";
                parametro.Value = idUsuario_;
                _comando.Parameters.Add(parametro);

                using IDataReader reader = _comando.ExecuteReader();

                List<Rol> listaRoles = new List<Rol>();

                while (reader.Read())
                {
                    Rol rol = new Rol();

                    rol.IdRol = reader.GetInt32(0);
                    rol.Nombre = reader.IsDBNull(1) ? null : reader.GetString(1);
                    rol.Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2);
                    rol.NivelAcceso = reader.IsDBNull(3) ? null : reader.GetByte(3);

                    listaRoles.Add(rol);
                }

                return listaRoles;
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
                return Enumerable.Empty<Rol>();
            }
        } // fin del método ObtenerRolesAsync

        public async Task ActualizarUltimoLoginAsync(int idUsuario_)
        {
            try
            {
                using IDbConnection _conexion = await _conn.obtenerConexionAsync();
                using IDbCommand _comando = _conexion.CreateCommand();
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.CommandText = "sp_usuario_actualizar_ultimo_login";

                IDbDataParameter parametro = _comando.CreateParameter();
                parametro.ParameterName = "_id_usuario";
                parametro.Value = idUsuario_;
                _comando.Parameters.Add(parametro);

                _comando.ExecuteNonQuery();
            }
            catch (MySqlException error)
            {
                ArchivoLog.RegistrarErrores(error);
            }
        }// fin del método ActualizarUltimoLoginAsync
    } // fin de la clase RepositorioUsuario
}
