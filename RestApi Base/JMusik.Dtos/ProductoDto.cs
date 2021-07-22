using System.ComponentModel.DataAnnotations;

namespace JMusik.Dtos
{

    public class ProductoDto
    {
      //  wadtopro + 2 tab para importar el code sniped
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [Display(Name = "Producto")]
        public string Nombre { get; set; }

        public decimal Precio { get; set; }
    } // fin de la clase ProdutctoDto       
        
}// fin del namespace
