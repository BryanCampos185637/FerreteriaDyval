using AdminFerreteria.BussinesLogic;
using System;

namespace AdminFerreteria.Helper.HelperInicioSistema
{
    public class CreacionPaginasRol
    {
        public static void iniciarSistema()
        {
            try
            {
                var pagina = new PaginaBL();
                var tipo = new TipoUsuarioBL();
                if (!pagina.existePaginaEnSistema())
                {
                    var rptPagina = pagina.insertarPaginas();
                    if (rptPagina > 0)
                        tipo.insertarRolAdministrador();
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
        }
    }
}
