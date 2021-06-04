using AdminFerreteria.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class SeguridadDAL
    {
        public static int TieneAutorizacion(string controller, string action, int idUsuario)
        {
            using (var bd = new BDFERRETERIAContext())
            {
                var tipoUsuario = bd.Usuario.Where(p => p.Iidusuario == idUsuario).First();//capturamos el tipo de usuario de la cookie
                if (tipoUsuario.Bhabilitado == "A")
                {
                   return (from ptu in bd.Paginatipousuario
                              join pagina in bd.Pagina on ptu.Iidpagina equals pagina.Iidpagina
                              where pagina.Accion.ToLower() == action.ToLower() &&
                              pagina.Controlador.ToLower() == controller.ToLower() &&
                              ptu.Iidtipousuario == tipoUsuario.Iidtipousuario && ptu.Bhabilitado == "A"
                              select new Pagina { Iidpagina = pagina.Iidpagina }).Count();
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
