using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminFerreteria.Helper.HelperSeguridad
{
    public class FiltroDeAcciones : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            #region validacion de validacion
            int? sesionActiva = HelperSession.Cookies.obtenerObjetoSesion(context.HttpContext.Session, "UsuarioLogueado");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Login/Index");//devolvemos a la vista login
            #endregion
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            #region validacion de validacion
            int? sesionActiva = HelperSession.Cookies.obtenerObjetoSesion(context.HttpContext.Session, "UsuarioLogueado");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Login/Index");//devolvemos a la vista login
            #endregion
        }
    }
}
