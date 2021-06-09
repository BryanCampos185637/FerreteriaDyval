using AdminFerreteria.Models;
using AdminFerreteria.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
        public List<Pagina> ListarPaginas()
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Pagina.ToList();
            }
        }
        public Pagina ObtenerPaginaPorId(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Pagina.Where(p => p.Iidpagina == id).First();
            }
        }
        public int Guardar(Pagina pagina)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    if (pagina.Iidpagina == 0)
                    {
                        pagina.Icono = pagina.Icono.ToLower();
                        pagina.Fechacreacion = DateTime.Now;
                        pagina.Bhabilitado = "A";
                        db.Pagina.Add(pagina);
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        var data = db.Pagina.Where(p => p.Iidpagina == pagina.Iidpagina).First();
                        data.Mensaje = pagina.Mensaje;
                        data.Controlador = pagina.Controlador;
                        data.Accion = pagina.Accion;
                        data.Icono = pagina.Icono.ToLower();
                        db.SaveChanges();
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public string Eliminar(int id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    var nveces = db.Paginatipousuario.Where(p => p.Bhabilitado == "A" && p.Iidpagina == id).Count();
                    if (nveces == 0)
                    {
                        db.Pagina.Remove(db.Pagina.Where(p => p.Iidpagina == id).First());
                        db.SaveChanges();
                        return "ok";
                    }
                    else
                    {
                        return "uso";
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string ObtenerNombrePagina(string action, string controller)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Pagina.Where(p => p.Accion.Equals(action) && p.Controlador.Equals(controller) && p.Bhabilitado == "A").First().Mensaje;
            }
        }
    }
}
