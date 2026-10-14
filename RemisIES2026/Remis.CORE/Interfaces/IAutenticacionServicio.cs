using Remis.CORE.Entidades;

namespace Remis.CORE.Interfaces
{
    public interface IAutenticacionServicio
    {
        Task<Usuario?> ValidarCredencialesAsync(string usuario_, string password_);
        string GenerarToken(Usuario usuario_, List<string> roles_);
    }
}
