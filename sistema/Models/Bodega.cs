using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Bodega
    {
        public Bodega()
        {
            Inventario = new HashSet<Inventario>();
        }

        public int Iidbodega { get; set; }
        public string Nombrebodega { get; set; }
        public string Bhabilitado { get; set; }

        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
