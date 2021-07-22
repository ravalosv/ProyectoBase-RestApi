using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using JMusik.Models.Enum;

#nullable disable

namespace JMusik.Models
{
    [Table("Producto", Schema = "tienda")]
    public class Producto
    {
        public Producto()
        {
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Nombre { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }
        public EstatusProducto Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }

        [InverseProperty(nameof(Models.DetalleOrden.Producto))]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
