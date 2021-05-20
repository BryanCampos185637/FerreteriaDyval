using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Entrada
    {
        public Entrada()
        {
            Bitacoraentrada = new HashSet<Bitacoraentrada>();
        }

        public long Iidentrada { get; set; }
        public long Iidproducto { get; set; }
        public long Existenciasproducto { get; set; }
        public DateTime? Fechaexpedicionccf { get; set; }
        public DateTime? Fechainiciocredito { get; set; }
        public DateTime? Fechavencimiento { get; set; }
        public string Condicionventa { get; set; }
        public string Numeroccf { get; set; }
        public long Cantidad { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }
        public string Proveedor { get; set; }
        public decimal? Preciocompra { get; set; }

        public virtual Producto IidproductoNavigation { get; set; }
        public virtual ICollection<Bitacoraentrada> Bitacoraentrada { get; set; }
    }
}
