using AdminFerreteria.BussinesLogic;
using System;

namespace AdminFerreteria.Helper.HelperSeguridad
{
    public class ObtenerNombreUsuarioLogueado
    {
        public static string obtenerNombre(int id)
        {
            try
            {
                EmpleadoBL bl = new EmpleadoBL();
                return bl.obtenerEmpleado(id).Nombrecompleto;
            }
            catch (Exception)
            {
                return "No hay sesión activa.";
            }
        } 
    }
}
