using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Tipousuario
    {
        public Tipousuario()
        {
            Paginatipousuario = new HashSet<Paginatipousuario>();
            Usuario = new HashSet<Usuario>();
        }

        public int Iidtipousuario { get; set; }
        public string Nombretipousuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }

        public virtual ICollection<Paginatipousuario> Paginatipousuario { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
