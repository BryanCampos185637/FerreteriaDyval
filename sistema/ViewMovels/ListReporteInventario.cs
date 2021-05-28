
namespace AdminFerreteria.Request
{
    public class ListReporteInventario
    {
        public long Iidinventario { get; set; }
        public int Iidbodega { get; set; }
        public long Iidproducto { get; set; }
        public long Cantidad { get; set; }
        public decimal Subcantidad { get; set; }
        public int Iidstock { get; set; }
        public string Nombrebodega { get; set; }
        public string Nombreproducto { get; set; }
        public string Codigoproducto { get; set; }
        public string Nombrestock { get; set; }
        public string Nombreunidad { get; set; }
        public string Nombresubunidad { get; set; }
        public string Precio { get; set; }
        public string Subprecio { get; set; }
        public string Proveedor { get; set; }
    }
}
