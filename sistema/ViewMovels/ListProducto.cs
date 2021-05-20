using System;

namespace AdminFerreteria.Request
{
    /*todas las clases que contienen la palabra List me sirven para crear listas ya que utilizan 
    propiedades virtuales en la de models y me interfieren con la logica de Ajax
    asi que obtengo las propiedades de las foraneas con linq usando consultas multi tablas (JOIN)
     */
    public class ListProducto
    {
        public string Nombreunidad { get; set; }
        public string Nombrestock { get; set; }
        public string Nombresubunidad { get; set; }
        public long Iidproducto { get; set; }
        public int Iidunidadmedida { get; set; }
        public string Codigoproducto { get; set; }
        public string Descripcion { get; set; }
        public long? Existencias { get; set; }
        public decimal? Preciocompra { get; set; }
        public decimal? Iva { get; set; }
        public int Porcentajeganancia { get; set; }
        public decimal? Ganancia { get; set; }
        public decimal? Precioventa { get; set; }
        public string Proveedor { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public string Bhabilitado { get; set; }
        public int Iidstock { get; set; }
        public int? Subunidad { get; set; }
        public decimal? Subpreciounitario { get; set; }
        public decimal? Subiva { get; set; }
        public int? Subporcentaje { get; set; }
        public decimal? Subganancia { get; set; }
        public decimal? Subprecioventa { get; set; }
        public decimal? Equivalencia { get; set; }
        public decimal? Subexistencia { get; set; }
        public decimal? Restantes { get; set; }
    }
}
