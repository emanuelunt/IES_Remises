using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Remis.CORE.Entidades
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("id_persona")]
        public int? IdPersona { get; set; }

        [Column("usuario")]
        [MaxLength(100)]
        public string? UsuarioNombre { get; set; }  // ← "usuario" es palabra reservada en algunos ORM

        [Column("password_hash")]
        [MaxLength(255)]
        public string? PasswordHash { get; set; }

        [Column("ultimoLogin")]
        public DateTime? UltimoLogin { get; set; }

        [Column("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [Column("fechaActualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        [Column("activo")]
        public bool? Activo { get; set; }

        //Propiedades de navegación
        [ForeignKey("IdPersona")]
        public Persona? Persona { get; set; }

        public ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
    }
}
