using System;

namespace AdminFerreteria.Request
{
    public class DetalleVenta
    {
        public string codigoproducto { get; set; }
        public Int64 iidcotizacion { get; set; }
        public Int64 iidproducto { get; set; }
        public string nombreproducto { get; set; }
        public Int64 cantidad { get; set; }
        public decimal preciounitario { get; set; }
        public decimal descuento { get; set; }
        public decimal comision { get; set; }
        public decimal total { get; set; }
        //extras para saber cuanto porcentaje les agrego
        public int pdescuento { get; set; }
        public int pcomision { get; set; }
        public string unidadmedida { get; set; }
        public string nocotizacion { get; set; }
        public string nofactura { get; set; }
        public decimal precioActual { get; set; }
        public bool Essubproducto { get; set; }
        public string Nombresubunidad { get; set; }
        public string subproducto { get; set; }
        public decimal iva { get; set; }
        public decimal precioconcomision { get; set; }
        public decimal? subiva { get; set; }
        public Int64 Idlista { get; set; }
    }
}
