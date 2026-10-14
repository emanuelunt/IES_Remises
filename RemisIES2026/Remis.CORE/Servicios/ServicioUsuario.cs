using Remis.CORE.Entidades;
using Remis.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Remis.CORE.Servicios
{
    public class ServicioUsuario
    {
        private readonly IRepositorioUsuario _repositorio;

        public ServicioUsuario(IRepositorioUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Usuario>> AllAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int _id)
        {
            //if (_id <= 0) // validad en el controlador
            //    throw new ArgumentException("El ID debe ser mayor a 0");

            return await _repositorio.ObtenerPorIdAsync(_id);
        }

        public async Task<Usuario?> GetByUsuarioAsync(string _usuario)
        {
            //if (string.IsNullOrWhiteSpace(_usuario)) // validar en el controlador
            //    throw new ArgumentException("El nombre de usuario no puede estar vacío");

            return await _repositorio.ObtenerPorUsuarioAsync(_usuario);
        }

        public async Task<IEnumerable<Rol>> GetRolesAsync(int _idUsuario)
        {
            //if (_idUsuario <= 0) // validad en el controlador
            //    throw new ArgumentException("El ID de usuario debe ser mayor a 0");

            return await _repositorio.ObtenerRolesAsync(_idUsuario);
        }

        public async Task<int> CrearAsync(Usuario _usuario, List<int> _roles)
        {
            // ✅ Validaciones de negocio
            if (_usuario == null)
                throw new ArgumentNullException(nameof(_usuario), "El usuario no puede ser nulo");

            if (string.IsNullOrWhiteSpace(_usuario.UsuarioNombre))
                throw new ArgumentException("El nombre de usuario es obligatorio");

            if (string.IsNullOrWhiteSpace(_usuario.PasswordHash))
                throw new ArgumentException("La contraseña es obligatoria");

            // ✅ Validar que el usuario no exista
            if (await _repositorio.ExisteUsuarioAsync(_usuario.UsuarioNombre))
                throw new InvalidOperationException($"Ya existe el usuario {_usuario.UsuarioNombre}");

            // ✅ Hashear la contraseña
         //   _usuario.PasswordHash = ServicioPassword.HashPassword(_usuario.PasswordHash);

            // ✅ Valores por defecto
            _usuario.FechaCreacion = DateTime.Now;
            _usuario.Activo = true;

            // ✅ Crear usuario
            int idUsuario = await _repositorio.CrearAsync(_usuario);

            // ✅ Asignar roles
            if (_roles != null && _roles.Count > 0)
            {
                foreach (var idRol in _roles)
                {
                    await _repositorio.AsignarRolAsync(idUsuario, idRol);
                }
            }

            return idUsuario;
        }

        // ═══════════════════════════════════════════════════════
        // ACTUALIZAR
        // ═══════════════════════════════════════════════════════

        public async Task<bool> ActualizarAsync(Usuario _usuario)
        {
            // ✅ Validaciones de negocio
            if (_usuario == null)
                throw new ArgumentNullException(nameof(_usuario), "El usuario no puede ser nulo");

            if (_usuario.IdUsuario <= 0)
                throw new ArgumentException("El ID no es válido");

            if (string.IsNullOrWhiteSpace(_usuario.UsuarioNombre))
                throw new ArgumentException("El nombre de usuario es obligatorio");

            // ✅ Verificar que el usuario existe
            var usuarioExistente = await _repositorio.ObtenerPorIdAsync(_usuario.IdUsuario);
            if (usuarioExistente == null)
                throw new KeyNotFoundException($"No se encontró el usuario con ID {_usuario.IdUsuario}");

            // ✅ Validar que el nombre de usuario no esté duplicado
            if (await _repositorio.ExisteUsuarioAsync(_usuario.UsuarioNombre, _usuario.IdUsuario))
                throw new InvalidOperationException($"Ya existe otro usuario con el nombre {_usuario.UsuarioNombre}");

            // ✅ Si se envía contraseña, hashearla
            if (!string.IsNullOrWhiteSpace(_usuario.PasswordHash))
            {
         //       _usuario.PasswordHash = PasswordService.HashPassword(_usuario.PasswordHash);
            }
            else
            {
                // ✅ Mantener la contraseña existente
                _usuario.PasswordHash = usuarioExistente.PasswordHash;
            }

            // ✅ Asegurar que Activo tenga un valor
            _usuario.Activo ??= true;

            // ✅ Fecha de actualización
            _usuario.FechaActualizacion = DateTime.Now;

            // ✅ Llamar al repositorio
            return await _repositorio.ActualizarAsync(_usuario);
        }

        // ═══════════════════════════════════════════════════════
        // ELIMINAR
        // ═══════════════════════════════════════════════════════

        public async Task<bool> EliminarAsync(int _id)
        {
            if (_id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0");

            var usuarioExistente = await _repositorio.ObtenerPorIdAsync(_id);
            if (usuarioExistente == null)
                throw new KeyNotFoundException($"No se encontró el usuario con ID {_id}");

            return await _repositorio.EliminarAsync(_id);
        }

        // ═══════════════════════════════════════════════════════
        // ROLES
        // ═══════════════════════════════════════════════════════

        public async Task<bool> AsignarRolAsync(int _idUsuario, int _idRol)
        {
            if (_idUsuario <= 0)
                throw new ArgumentException("El ID de usuario debe ser mayor a 0");

            if (_idRol <= 0)
                throw new ArgumentException("El ID de rol debe ser mayor a 0");

            // ✅ Verificar que el usuario existe
            var usuarioExistente = await _repositorio.ObtenerPorIdAsync(_idUsuario);
            if (usuarioExistente == null)
                throw new KeyNotFoundException($"No se encontró el usuario con ID {_idUsuario}");

            return await _repositorio.AsignarRolAsync(_idUsuario, _idRol);
        }

        public async Task<bool> EliminarRolesAsync(int _idUsuario)
        {
            if (_idUsuario <= 0)
                throw new ArgumentException("El ID de usuario debe ser mayor a 0");

            var usuarioExistente = await _repositorio.ObtenerPorIdAsync(_idUsuario);
            if (usuarioExistente == null)
                throw new KeyNotFoundException($"No se encontró el usuario con ID {_idUsuario}");

            return await _repositorio.EliminarRolesAsync(_idUsuario);
        }

        // ═══════════════════════════════════════════════════════
        // LOGIN
        // ═══════════════════════════════════════════════════════

        public async Task ActualizarUltimoLoginAsync(int _idUsuario)
        {
            if (_idUsuario <= 0)
                throw new ArgumentException("El ID de usuario debe ser mayor a 0");

            await _repositorio.ActualizarUltimoLoginAsync(_idUsuario);
        }
    }
}

