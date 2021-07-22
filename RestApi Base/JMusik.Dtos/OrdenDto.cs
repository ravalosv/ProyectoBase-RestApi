using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using JMusik.Models.Enum;

namespace JMusik.Dtos
{
    //  wadtoord + 2 tab para importar code sniped

    public class OrdenDto
    {
        public OrdenDto()
        {
            DetalleOrden = new List<DetalleOrdenDto>();
        }

        public int Id { get; set; }
        public decimal CantidadArticulos { get; set; }
        public decimal Importe { get; set; }

       // [Required]   // se comenta porque se hace desde el servidor
        public DateTime? FechaRegistro { get; set; }

        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        // public EstatusOrden EstatusOrden { get; set; } // se comenta porque se modifica desde el servidor
        public List<DetalleOrdenDto> DetalleOrden { get; set; }
    }// fin de la clase OrdenDto
        

}// fin del namespace
