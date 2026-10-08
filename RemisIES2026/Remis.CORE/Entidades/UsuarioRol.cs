using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Remis.CORE.Entidades
{
    [Table("usuario_rol")]
    public class UsuarioRol
    {
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        //Propiedades de navegación
        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
    }
}
