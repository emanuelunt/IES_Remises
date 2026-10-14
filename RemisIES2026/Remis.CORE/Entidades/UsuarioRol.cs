using System.ComponentModel.DataAnnotations.Schema;


namespace Remis.CORE.Entidades
{
    [Table("usuario_rol")]
    public class UsuarioRol
    {
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
    }
}
