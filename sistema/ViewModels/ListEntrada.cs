using System;

namespace AdminFerreteria.ViewModels
{
    public class ListEntrada
    {
        /*todas las clases que contienen la palabra List me sirven para crear listas ya que utilizan 
    propiedades virtuales en la de models y me interfieren con la logica de Ajax
    asi que obtengo las propiedades de las foraneas con linq usando consultas multi tablas (JOIN)
     */
        public Int64 iidentrada { get; set; }
        public string descripcionproducto { get; set; }
        public Int64 existencias { get; set; }
        public string fechaexpedicionccf { get; set; }
        public string fechainiciocredito { get; set; }
        public string fechavencimiento { get; set; }
        public Int64 entrada { get; set; }
        public string proveedor { get; set; }
        public decimal preciocompra { get; set; }
    }
}
