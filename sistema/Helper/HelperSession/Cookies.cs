using Microsoft.AspNetCore.Http;

namespace AdminFerreteria.Helper.HelperSession
{
    public static class Cookies
    {
        //antes de retornar el viex
        public static void crearCookieSession(this ISession session, string key, int? value)
        {
            session.SetInt32(key, (int)value);

        }
        //antes de entrar al iaction
        public static int? obtenerObjetoSesion(this ISession session, string key)
        {
            var value = session.GetInt32(key);
            return value; 
        }
    }
}
