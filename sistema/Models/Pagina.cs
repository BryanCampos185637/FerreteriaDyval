using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Pagina
    {
        public Pagina()
        {
            Paginatipousuario = new HashSet<Paginatipousuario>();
        }

        public int Iidpagina { get; set; }
        public string Mensaje { get; set; }
        public string Accion { get; set; }
        public string Controlador { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }
        public string Icono { get; set; }

        public virtual ICollection<Paginatipousuario> Paginatipousuario { get; set; }
    }
}
