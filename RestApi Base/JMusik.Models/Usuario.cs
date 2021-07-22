using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using JMusik.Models.Enum;

using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JMusik.Models
{
    [Table("Usuario", Schema = "tienda")]
    [Index(nameof(PerfilId), Name = "IX_Usuario_PerfilId")]
    public class Usuario
    {
        public Usuario()
        {
            Ordens = new HashSet<Orden>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(256)]
        public string Apellidos { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(25)]
        public string Username { get; set; }
        [StringLength(512)]
        public string Password { get; set; }
        public EstatusUsuario Estatus { get; set; }
        public int PerfilId { get; set; }

        [ForeignKey(nameof(PerfilId))]
        [InverseProperty("Usuarios")]
        public virtual Perfil Perfil { get; set; }
        [InverseProperty(nameof(Orden.Usuario))]
        public virtual ICollection<Orden> Ordens { get; set; }
    }
}
