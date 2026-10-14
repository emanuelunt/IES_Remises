using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Remis.CORE.Entidades
{
    [Table("choferes")]
    public class Chofer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdChofer")]
        public int IdChofer { get; set; }

        [Column("id_persona")]
        public int? IdPersona { get; set; }

        [Column("nro_licencia")]
        [MaxLength(20)]
        public string? NroLicencia { get; set; }

        [Column("categoria_licencia")]
        [MaxLength(50)]
        public string? CategoriaLicencia { get; set; }

        [Column("vencimiento_licencia")]
        public DateTime? VencimientoLicencia { get; set; }

        [Column("fecha_ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [Column("tipo_relacion")]
        [MaxLength(50)]
        public string? TipoRelacion { get; set; }

        [Column("activo")]
        public bool? Activo { get; set; }

        // Propiedad de navegación
        [ForeignKey("IdPersona")]
        public Persona? Persona { get; set; }
    }
}
