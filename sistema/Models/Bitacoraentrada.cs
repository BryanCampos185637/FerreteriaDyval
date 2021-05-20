
namespace AdminFerreteria.Models
{
    public partial class Bitacoraentrada
    {
        public long Iidbotacorabodega { get; set; }
        public long Iidentrada { get; set; }
        public long Iidproducto { get; set; }
        public int Iidbodega { get; set; }
        public int Iidstock { get; set; }
        public long Cantidad { get; set; }
        public decimal Subcantidad { get; set; }

        public virtual Entrada IidentradaNavigation { get; set; }
    }
}
