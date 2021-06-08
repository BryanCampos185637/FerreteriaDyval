
namespace AdminFerreteria.ViewModels
{
    /*todas las clases que contienen la palabra List me sirven para crear listas ya que utilizan 
    propiedades virtuales en la de models y me interfieren con la logica de Ajax
    asi que obtengo las propiedades de las foraneas con linq usando consultas multi tablas (JOIN)
     */
    public class ListUsuario
    {
        public int iidusuario { get; set; }
        public string nombreempleado { get; set; }
        public string nombretipousuario { get; set; }
        public string nombreusuario { get; set; }
    }
}
