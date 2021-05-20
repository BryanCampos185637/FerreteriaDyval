using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Factura
    {
        public Factura()
        {
            Detallepedido = new HashSet<Detallepedido>();
        }

        public long Iidfactura { get; set; }
        public int Iidusuario { get; set; }
        public string Tipocomprador { get; set; }
        public string Nombrecliente { get; set; }
        public string Direccion { get; set; }
        public string Registro { get; set; }
        public string Giro { get; set; }
        public string Nit { get; set; }
        public decimal? Total { get; set; }
        public string Bhabilitado { get; set; }
        public DateTime Fechacreacion { get; set; }
        public string Facturaemitida { get; set; }
        public string Nofactura { get; set; }
        public decimal Totalcomision { get; set; }
        public decimal Totaldescuento { get; set; }
        public int Porcentajedescuentoglobal { get; set; }
        public decimal Descuentoglobal { get; set; }
        public decimal? Efectivo { get; set; }
        public decimal? Cambio { get; set; }
        public string Original { get; set; }

        public virtual Usuario IidusuarioNavigation { get; set; }
        public virtual ICollection<Detallepedido> Detallepedido { get; set; }
    }
}
