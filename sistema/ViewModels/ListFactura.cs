using System;

namespace AdminFerreteria.ViewModels
{/*todas las clases que contienen la palabra List me sirven para crear listas ya que utilizan 
    propiedades virtuales en la de models y me interfieren con la logica de Ajax
    asi que obtengo las propiedades de las foraneas con linq usando consultas multi tablas (JOIN)
     */
    public class ListFactura
    {
        public Int64 iidfactura { get; set; }
        public int iidusuario { get; set; }
        public string nombrecomprador { get; set; }
        public string tipocomprador { get; set; }
        public string nombrevendedor { get; set; }
        public string fechaemitida { get; set; }
        public decimal total { get; set; }
        public string direccion { get; set; }
        public string nit { get; set; }
        public string giro { get; set; }
        public string registro { get; set; }
        public string nofactura { get; set; }
        public int porcentajedescuento { get; set; }
        public decimal totalcomision { get; set; }
        public decimal descuentogeneral { get; set; }
        public decimal totaldescuento { get; set; }
        public decimal cambio { get; set; }
        public decimal efectivo { get; set; }
        public string original { get; set; }
    }
}
