using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            
            int existe = 0;
            try
            {
                using (var bd = new BDFERRETERIAContext())
                {
                    var tipoUsuario = bd.Usuario.Where(p => p.Iidusuario == idUsuario).First();//capturamos el tipo de usuario de la cokie
                    if (tipoUsuario.Bhabilitado == "A")
                    {
                        existe = (from ptu in bd.Paginatipousuario
                                  join pagina in bd.Pagina on ptu.Iidpagina equals pagina.Iidpagina
                                  where pagina.Accion.ToLower() == action.ToLower() &&
                                  pagina.Controlador.ToLower() == controller.ToLower() &&
                                  ptu.Iidtipousuario == tipoUsuario.Iidtipousuario && ptu.Bhabilitado == "A"
                                  select new Pagina { Iidpagina = pagina.Iidpagina }).Count();
                        if (existe > 0)//si el tipo de usuario tiene relacionada esa pagina se retorna un true
                            return true;
                        else//de lo contrario un false
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
