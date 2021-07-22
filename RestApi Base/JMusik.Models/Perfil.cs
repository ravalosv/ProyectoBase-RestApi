using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace JMusik.Models
{
    [Table("Perfil", Schema = "tienda")]
    public class Perfil
    {
        public Perfil()
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }

        [InverseProperty(nameof(Usuario.Perfil))]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
