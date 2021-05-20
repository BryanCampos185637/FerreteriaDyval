using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Detallecotizacion = new HashSet<Detallecotizacion>();
            Detallepedido = new HashSet<Detallepedido>();
            Entrada = new HashSet<Entrada>();
            Inventario = new HashSet<Inventario>();
        }

        public long Iidproducto { get; set; }
        public int Iidunidadmedida { get; set; }
        public string Codigoproducto { get; set; }
        public string Descripcion { get; set; }
        public long? Existencias { get; set; }
        public decimal? Preciocompra { get; set; }
        public decimal? Iva { get; set; }
        public int Porcentajeganancia { get; set; }
        public decimal? Ganancia { get; set; }
        public decimal? Precioventa { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }
        public int Iidstock { get; set; }
        public int? Subunidad { get; set; }
        public decimal? Subpreciounitario { get; set; }
        public decimal? Subiva { get; set; }
        public int? Subporcentaje { get; set; }
        public decimal? Subganancia { get; set; }
        public decimal? Subprecioventa { get; set; }
        public decimal? Equivalencia { get; set; }
        public decimal? Subexistencia { get; set; }
        public decimal? Restantes { get; set; }

        public virtual Stock IidstockNavigation { get; set; }
        public virtual Unidadmedida IidunidadmedidaNavigation { get; set; }
        public virtual ICollection<Detallecotizacion> Detallecotizacion { get; set; }
        public virtual ICollection<Detallepedido> Detallepedido { get; set; }
        public virtual ICollection<Entrada> Entrada { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
