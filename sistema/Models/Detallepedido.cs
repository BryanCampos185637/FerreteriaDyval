using System;

namespace AdminFerreteria.Models
{
    public partial class Detallepedido
    {
        public long Iiddetallepedido { get; set; }
        public long Iidfactura { get; set; }
        public long Iidproducto { get; set; }
        public decimal? Precioactual { get; set; }
        public long Cantidad { get; set; }
        public int Porcentajedescuento { get; set; }
        public decimal? Descuento { get; set; }
        public int Porcentajecomision { get; set; }
        public decimal? Comision { get; set; }
        public decimal? Subtotal { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }
        public string Essubproducto { get; set; }

        public virtual Factura IidfacturaNavigation { get; set; }
        public virtual Producto IidproductoNavigation { get; set; }
    }
}
