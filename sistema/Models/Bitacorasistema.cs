using System;

namespace AdminFerreteria.Models
{
    public partial class Bitacorasistema
    {
        public long Iidbitacorasistema { get; set; }
        public int Iidusuario { get; set; }
        public string Descripcionbitacora { get; set; }
        public DateTime? Fechaactividad { get; set; }

        public virtual Usuario IidusuarioNavigation { get; set; }
    }
}
