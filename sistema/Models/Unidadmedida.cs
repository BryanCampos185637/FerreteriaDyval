using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Unidadmedida
    {
        public Unidadmedida()
        {
            Producto = new HashSet<Producto>();
        }

        public int Iidunidadmedida { get; set; }
        public string Nombreunidad { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
