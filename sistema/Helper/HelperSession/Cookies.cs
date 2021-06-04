using Microsoft.AspNetCore.Http;

namespace AdminFerreteria.Helper.HelperSession
{
    public static class Cookies
    {
        //crea las cookies
        public static void crearCookieSession(this ISession session, string key, int? value)
        {
            session.SetInt32(key, (int)value);

        }
        //devuelve el valor de la cookie
        public static int? obtenerObjetoSesion(this ISession session, string key)
        {
            var value = session.GetInt32(key);
            return value; 
        }
    }
}
