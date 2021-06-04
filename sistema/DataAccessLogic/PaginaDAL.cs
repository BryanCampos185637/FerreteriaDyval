using AdminFerreteria.Models;
using AdminFerreteria.Request;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class PaginaDAL
    {
        public int InsertarPaginas()
        {
            ConexionSQL conexion = new ConexionSQL();
            using (SqlConnection con = new SqlConnection(conexion.cadenaConexion))
            {
                con.Open();
                SqlCommand command = new SqlCommand("sp_crear_paginas_sistema", con);
                command.CommandType = CommandType.StoredProcedure;
                return command.ExecuteNonQuery();
            }
        }
        public bool ExistenPaginasRegistradasEnSistema()
        {
            using(var db = new BDFERRETERIAContext())
            {
                var cantidad = db.Pagina.Where(p => p.Bhabilitado.Equals("A")).Count();
                if (cantidad > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
