using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Detallecotizacion
    {
        public long Iiddetallecotizacio { get; set; }
        public long Iidcotizacion { get; set; }
        public long Iidproducto { get; set; }
        public decimal? Precioactual { get; set; }
        public long Cantidad { get; set; }
        public int Porcentajedescuento { get; set; }
        public decimal? Descuento { get; set; }
        public int Porcentajecomision { get; set; }
        public decimal? Comision { get; set; }
        public decimal? Subtotal { get; set; }
        public DateTime Fechavencimiento { get; set; }
        public string Bhabilitado { get; set; }
        public string Essubproducto { get; set; }

        public virtual Cotizacion IidcotizacionNavigation { get; set; }
        public virtual Producto IidproductoNavigation { get; set; }
    }
}
