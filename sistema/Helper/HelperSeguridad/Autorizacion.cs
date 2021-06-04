using AdminFerreteria.BussinesLogic;
using System;
namespace AdminFerreteria.Helper.HelperSeguridad
{
    public class Autorizacion
    {
        /// <summary>
        /// este metodo funciona como filtro para evitar que los usuarios accedan mediante url
        /// </summary>
        /// <param name="controller">pasamos el nombre del controlador</param>
        /// <param name="action">El nombre de la accion</param>
        /// <param name="idUsuario">y el id del usuario logueado para obtener su tipo de usuario</param>
        /// <returns></returns>
        public static bool ValidarPermisos(string controller, string action, int idUsuario)
        {
            try
            {
                int existe = SeguridadBL.tieneAutorizacion(controller, action, idUsuario);
                if (existe > 0)//si el tipo de usuario tiene relacionada esa pagina se retorna un true
                    return true;
                else//de lo contrario un false
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
