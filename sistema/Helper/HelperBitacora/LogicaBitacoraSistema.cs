using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Models;

namespace AdminFerreteria.Helper.HelperBitacora
{
    public class LogicaBitacoraSistema
    {
        public static void InsertarBitacoraBL(string mensaje, int? UsuarioLogueado)
        {
            BitacoraSistemaBL bitacora = new BitacoraSistemaBL();
            bitacora.insertarBitacora(new Bitacorasistema
            {
                Iidusuario = (int)UsuarioLogueado,
                Descripcionbitacora = mensaje,
                Fechaactividad = System.DateTime.Now
            });
        }
    }
}
