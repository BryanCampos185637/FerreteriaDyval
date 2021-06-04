namespace AdminFerreteria.Request
{
    //esta clase funcionara solo para obtener la cadena de conexion utilizando el constructor de la clase
    public class ConexionSQL
    {

        #region conexiones a la bd
        //Scaffold-DbContext "server=DESKTOP-TUVI7D5\\SPARTANDEV;database=BDFERRETERIA; integrated security=true;" -OutputDir Models -Force Microsoft.EntityFrameworkCore.SqlServer
        private readonly string cadLocal = "server=DESKTOP-TUVI7D5\\SPARTANDEV;database=BDFERRETERIA; user=sa; password=triz7+10";
        private readonly string cadFerreteria = "server=WIN-D38DOKP8IOM;database=BDFERRETERIA; user=sa; password=Laterminal21";
        private readonly string cadHost = "Data Source=SQL5080.site4now.net;Initial Catalog=DB_A6868B_SpartanDev; User Id = DB_A6868B_SpartanDev_admin; Password=Campos2020";
        #endregion

        public string cadenaConexion { get; set; }
        public ConexionSQL()
        {
            cadenaConexion = cadLocal;
        }
    }
}
