using System;

namespace AdminFerreteria.Models
{
    public partial class Paginatipousuario
    {
        public int Iidpaginatipousuario { get; set; }
        public int Iidpagina { get; set; }
        public int Iidtipousuario { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }

        public virtual Pagina IidpaginaNavigation { get; set; }
        public virtual Tipousuario IidtipousuarioNavigation { get; set; }
    }
}
