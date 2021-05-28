using AdminFerreteria.Controllers;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminFerreteria.Helper.HelperSeguridad
{
    public class FiltroDeAutenticacionValidacion : IActionFilter
    {
        //antes de retornar el view
        public void OnActionExecuted(ActionExecutedContext context)
        {
            #region validacion de validacion
            int? sesionActiva = HelperSession.Cookies.obtenerObjetoSesion(context.HttpContext.Session, "UsuarioLogueado");
            if (sesionActiva == null)
                context.Result = new RedirectResult("/Login/Index");//devolvemos a la vista login
            #endregion
        }
        //antes de entrar al iaction
        public void OnActionExecuting(ActionExecutingContext context)
        {
            #region validacion de usuario
            int? sesionActiva = HelperSession.Cookies.obtenerObjetoSesion(context.HttpContext.Session, "UsuarioLogueado");
            if (sesionActiva == null || sesionActiva <= 0) 
                context.Result = new RedirectResult("/Login/Index");//devolvemos a la vista login
            #endregion

            #region autorizacion
            else
            {
                #region obtener nombre del action, y el nombre del controller
                string DescripcionController = context.ActionDescriptor.DisplayName;//obtenemos la ruta
                var matriz = DescripcionController.Split('.');//hacemos una matriz siempre y cuando encuentre un punto
                //creamos las variables necesarias
                string action = matriz[3].Replace("(" + matriz[0] + ")", "").Trim();//obtenemos el nombre de la accion
                string controller = matriz[2].Replace("Controller", "").Trim();//obtenemos el nombre del controlador
                #endregion

                #region logica de autorizacion del usuario
                if (controller != "BitacoraSistema")
                {
                    if (Autorizacion.ValidarPermisos(controller.ToUpper(), action.ToUpper(), (int)sesionActiva))
                    {
                        #region BITACORA
                        BitacoraSistemaDAL bitacora = new BitacoraSistemaDAL();
                        bitacora.insertarBitacora(new Bitacorasistema
                        {
                            Iidusuario = (int)sesionActiva,
                            Descripcionbitacora = "ENTRO A LA VISTA " + UtilidadesController.obtenerNombrePagina(action, controller) + ".",
                            Fechaactividad = System.DateTime.Now
                        });
                        #endregion
                    }
                    else
                    {
                        #region BITACORA
                        BitacoraSistemaDAL bitacora = new BitacoraSistemaDAL();
                        bitacora.insertarBitacora(new Bitacorasistema
                        {
                            Iidusuario = (int)sesionActiva,
                            Descripcionbitacora = "INTENTO ENTRAR A LA VISTA " + UtilidadesController.obtenerNombrePagina(action, controller) + ", PERO FUE RECHAZADO POR NO TENER PERMISOS.",
                            Fechaactividad = System.DateTime.Now
                        });
                        #endregion
                        context.Result = new RedirectResult("/Home/error");
                    }
                }
                #endregion
            }
            #endregion
        }

    }
}
