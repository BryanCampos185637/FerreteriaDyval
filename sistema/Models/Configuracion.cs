using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Configuracion
    {
        public int Iidconfiguracion { get; set; }
        public long Iniciofactura { get; set; }
        public long Finfactura { get; set; }
        public long Noactualfactura { get; set; }
        public long Iniciocotizacion { get; set; }
        public long Fincotizacion { get; set; }
        public long Noactualcotizacion { get; set; }
        public int Nodigitosfactura { get; set; }
        public int Nodigitoscotizacion { get; set; }
        public long Iniciocreditofiscal { get; set; }
        public long Fincreditofiscal { get; set; }
        public long Noactualcreditofiscal { get; set; }
        public int Nodigitoscreditofiscal { get; set; }
    }
}
