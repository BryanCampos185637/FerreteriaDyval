namespace AdminFerreteria.ViewModels
{
    //esta clase funcionara solo para obtener la cadena de conexion utilizando el constructor de la clase
    public class ConexionSQL
    {
        public string cadenaConexion { get; set; }
        public ConexionSQL()
        {
            //cadenaConexion = "Data Source=SQL5080.site4now.net;Initial Catalog=db_a89c60_ferreteriadyval;User Id=db_a89c60_ferreteriadyval_admin;Password=produccion5";
            //cadenaConexion = "Data Source=SQL8002.site4now.net;Initial Catalog=db_a89d52_ferreteriadyval;User Id=db_a89d52_ferreteriadyval_admin;Password=ABCDabcd1234";
            cadenaConexion = "Data Source=DESKTOP-TUVI7D5\\SQLEXPRESS01;Initial Catalog=db_a89d52_ferreteriadyval;Integrated Security=True";
        }
    }
}
