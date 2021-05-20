using System;
using System.Collections.Generic;

namespace AdminFerreteria.Models
{
    public partial class Cliente
    {
        public long Iidcliente { get; set; }
        public string Nombrecompleto { get; set; }
        public string Direccion { get; set; }
        public string Registro { get; set; }
        public string Giro { get; set; }
        public string Nit { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }
    }
}
