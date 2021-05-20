using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Iidempleado { get; set; }
        public string Nombrecompleto { get; set; }
        public int Edad { get; set; }
        public int? Telefono { get; set; }
        public string Dui { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Empleadotieneusuario { get; set; }
        public string Bhabilitado { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
