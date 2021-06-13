using AdminFerreteria.Helper.HelperEncriptar;

namespace AdminFerreteria.ViewModels
{
    //esta clase funcionara solo para obtener la cadena de conexion utilizando el constructor de la clase
    public class ConexionSQL
    {

        #region conexiones a la bd
        //Scaffold-DbContext "server=DESKTOP-TUVI7D5\\SPARTANDEV;database=BDFERRETERIA; integrated security=true;" -OutputDir Models -Force Microsoft.EntityFrameworkCore.SqlServer
        private readonly string cadLocalCifrada = "xIp5VgiLKjQlTSyQC9LmFwFY45pHaaqJdv5SNZKT6yOqwKdLUo9MLT/nDKBfuOsw2XFVtdBOHQfOH5b6bFn5O+JqbpPvHSDmmqW3AB2aeAyGZwyeBNLz/A==";
        private readonly string cadHost = "Data Source=SQL5080.site4now.net;Initial Catalog=DB_A6868B_SpartanDev; User Id = DB_A6868B_SpartanDev_admin; Password=Campos2020";
        private readonly string cadFerreteriaCifrada = "hndHPG/s+cEbccbtBz/XdAyVHKiSlOpwc/MOPo5SerE4ymaYvur1CGkh11MMdE9eWCqr+FjXek9GAC981f9WayI5pbhBBvMlgBG4vg+633Y=";
        private readonly string cadenaFerreteria = "server=WIN-D38DOKP8IOM;database=BDFERRETERIA; user=sa; password=Laterminal21";
        #endregion

        public string cadenaConexion { get; set; }
        public ConexionSQL()
        {
            //cadenaConexion = cadenaFerreteria;
            cadenaConexion = CifrarCadenaConLlave.descifrar(cadFerreteriaCifrada);
        }
    }
}
