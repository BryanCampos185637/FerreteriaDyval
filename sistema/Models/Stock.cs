using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Stock
    {
        public Stock()
        {
            Inventario = new HashSet<Inventario>();
            Producto = new HashSet<Producto>();
        }

        public int Iidstock { get; set; }
        public string Nombrestock { get; set; }
        public DateTime Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }

        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
