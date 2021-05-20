using System;

namespace AdminFerreteria.Request
{
    /*todas las clases que contienen la palabra List me sirven para crear listas ya que utilizan 
    propiedades virtuales en la de models y me interfieren con la logica de Ajax
    asi que obtengo las propiedades de las foraneas con linq usando consultas multi tablas (JOIN)
     */
    public class ListCotizacion
    {
        public Int64 iidcotizacion { get; set; }
        public int iidusuario { get; set; }
        public string nocotizacion { get; set; }
        public string fechacreacion { get; set; }
        public string fechavencimiento { get; set; }
        public string cotizacionfacturada { get; set; }
        public string nombreusuario { get; set; }
        public decimal total { get; set; }
        public string nombrecliente { get; set; }
    }
}
