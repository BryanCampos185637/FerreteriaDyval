using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminFerreteria.Request
{
    public class ListInventario
    {
        public long Iidinventario { get; set; }
        public int Iidbodega { get; set; }
        public long Iidproducto { get; set; }
        public long Cantidad { get; set; }
        public string Bhabilitado { get; set; }
        public int Iidstock { get; set; }
        public string Nombrebodega { get; set; }
        public string Nombreproducto { get; set; }
        public string Codigoproducto { get; set; }
        public string Nombrestock { get; set; }
    }
}
