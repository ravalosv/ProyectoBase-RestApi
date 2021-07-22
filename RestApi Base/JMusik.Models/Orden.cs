using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using JMusik.Models.Enum;

using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JMusik.Models

{
    [Table("Orden", Schema = "tienda")]
    [Index(nameof(UsuarioId), Name = "IX_Orden_UsuarioId")]
    public class Orden
    {
        public Orden()
        {
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CantidadArticulos { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Importe { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int UsuarioId { get; set; }
        public EstatusOrden EstatusOrden { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty("Ordens")]
        public virtual Usuario Usuario { get; set; }
        [InverseProperty(nameof(Models.DetalleOrden.Orden))]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
