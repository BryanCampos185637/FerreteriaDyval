

namespace AdminFerreteria.Models
{
    public partial class Inventario
    {
        public long Iidinventario { get; set; }
        public int Iidbodega { get; set; }
        public long Iidproducto { get; set; }
        public long Cantidad { get; set; }
        public string Bhabilitado { get; set; }
        public int Iidstock { get; set; }

        public virtual Bodega IidbodegaNavigation { get; set; }
        public virtual Producto IidproductoNavigation { get; set; }
        public virtual Stock IidstockNavigation { get; set; }
    }
}
