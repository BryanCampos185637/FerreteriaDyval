using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Bitacorasistema = new HashSet<Bitacorasistema>();
            Cotizacion = new HashSet<Cotizacion>();
            Factura = new HashSet<Factura>();
        }

        public int Iidusuario { get; set; }
        public int Iidempleado { get; set; }
        public int Iidtipousuario { get; set; }
        public string Nombreusuario { get; set; }
        public string Contraseña { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }

        public virtual Empleado IidempleadoNavigation { get; set; }
        public virtual Tipousuario IidtipousuarioNavigation { get; set; }
        public virtual ICollection<Bitacorasistema> Bitacorasistema { get; set; }
        public virtual ICollection<Cotizacion> Cotizacion { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
