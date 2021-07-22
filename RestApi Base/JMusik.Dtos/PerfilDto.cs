using System.ComponentModel.DataAnnotations;

namespace JMusik.Dtos
{
    //   wadtoper + 2 tab para agregar codesniped

    public class PerfilDto
    {
        public int Id { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "El nombre del perfil es requerido")]
        public string Nombre { get; set; }
    }
        

}// fin del namespace
