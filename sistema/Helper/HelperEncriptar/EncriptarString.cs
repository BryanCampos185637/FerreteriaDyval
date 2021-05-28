using System;
using System.Security.Cryptography;
using System.Text;

namespace AdminFerreteria.Helper.HelperEncriptar
{
    public class EncriptarString
    {
        public static string EncriptarTexto(string pPassword)
        {
            SHA256Managed sha = new SHA256Managed();//instancia
            byte[] dataNoCifrada = Encoding.Default.GetBytes(pPassword);//convertimos a arreglo de byte la contraseña
            byte[] dataCifrada = sha.ComputeHash(dataNoCifrada);//pasamos el array de byte para cifrar
            var passwordEncrypt = BitConverter.ToString(dataCifrada);//convertimos a texto
            passwordEncrypt = passwordEncrypt.Replace("-", "");//reemplazamos los - por vacios
            return passwordEncrypt;//retornamos la contraseña en una cadena hexadecimal
        }
    }
}
