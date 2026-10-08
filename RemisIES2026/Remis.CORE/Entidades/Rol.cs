using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Remis.CORE.Entidades
{
    [Table("roles")]
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdRol")]
        public int IdRol { get; set; }

        [Column("nombre")]
        [MaxLength(255)]
        public string? Nombre { get; set; }

        [Column("descripcion")]
        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Column("nivel_acceso")]
        public byte? NivelAcceso { get; set; }  // TINYINT → byte

        //Propiedad de navegación (relación muchos a muchos)
        public ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
    }
}
