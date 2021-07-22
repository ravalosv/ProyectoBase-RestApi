namespace JMusik.Dtos
{

    // wadtodet + 2 veces tab, para importar el codesniped

    public class DetalleOrdenDto
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
        

}// fin del namespace
