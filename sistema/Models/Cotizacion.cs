using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Cotizacion
    {
        public Cotizacion()
        {
            Detallecotizacion = new HashSet<Detallecotizacion>();
        }

        public long Iidcotizacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime Fechavencimiento { get; set; }
        public string Cotizacionfacturada { get; set; }
        public string Bhabilitado { get; set; }
        public int Iidusuario { get; set; }
        public decimal? Total { get; set; }
        public string Nombrecliente { get; set; }
        public string Cotizacionemitida { get; set; }
        public string Nocotizacion { get; set; }

        public virtual Usuario IidusuarioNavigation { get; set; }
        public virtual ICollection<Detallecotizacion> Detallecotizacion { get; set; }
    }
}
