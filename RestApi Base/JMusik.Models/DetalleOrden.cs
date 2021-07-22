using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JMusik.Models
{
    [Table("DetalleOrden", Schema = "tienda")]
    [Index(nameof(OrdenId), Name = "IX_DetalleOrden_OrdenId")]
    [Index(nameof(ProductoId), Name = "IX_DetalleOrden_ProductoId")]
    public class DetalleOrden
    {
        [Key]
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        [ForeignKey(nameof(OrdenId))]
        [InverseProperty("DetalleOrden")]
        public virtual Orden Orden { get; set; }
        [ForeignKey(nameof(ProductoId))]
        [InverseProperty("DetalleOrden")]
        public virtual Producto Producto { get; set; }
    }
}
