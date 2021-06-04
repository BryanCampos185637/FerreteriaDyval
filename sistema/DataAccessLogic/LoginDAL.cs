using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AdminFerreteria.DataAccessLogic
{
    public class LoginDAL
    {
        public int LogIn(Usuario user)
        {
            using (var db = new BDFERRETERIAContext())
            {
                Usuario usuario = db.Usuario.Where(p => p.Nombreusuario == user.Nombreusuario.ToUpper() && p.Bhabilitado == "A")
                    .Include(p => p.IidempleadoNavigation).FirstOrDefault();
                if (usuario != null)
                {
                    string passwordEncrypt = UtilidadesController.encryptPassword(user.Contraseña);
                    if (usuario.Bhabilitado == "A")
                    {
                        if (usuario.Contraseña == passwordEncrypt)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
        public Usuario ObtenerDataUsuarioLog(Usuario user)
        {
            string passwordEncrypt = UtilidadesController.encryptPassword(user.Contraseña);
            using (var db = new BDFERRETERIAContext())
            {
                Usuario usuario = db.Usuario.Where(p => p.Nombreusuario == user.Nombreusuario.ToUpper() && p.Bhabilitado == "A"
                && p.Contraseña.Equals(passwordEncrypt)).Include(p => p.IidempleadoNavigation).FirstOrDefault();
                return usuario;
            }
        }
        public int RegistrarUserNuevo(Empleado empleado, string nombreusuario, string contraseña)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    using(var db = new BDFERRETERIAContext())
                    {
                        empleado.Bhabilitado = "A"; empleado.Fechacreacion = DateTime.Now; empleado.Empleadotieneusuario = "S";
                        db.Empleado.Add(empleado);
                        db.SaveChanges();
                        Usuario usuario = new Usuario();
                        usuario.Iidempleado = empleado.Iidempleado;
                        usuario.Contraseña = UtilidadesController.encryptPassword(contraseña);
                        usuario.Nombreusuario = nombreusuario;
                        usuario.Iidtipousuario = 1;
                        usuario.Fechacreacion = DateTime.Now;
                        usuario.Bhabilitado = "A";
                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                    }
                    transaction.Complete();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public object ListarPaginasMenu(int idTipoUsuario)
        {
            try
            {
                
                using (var bd = new BDFERRETERIAContext())
                {
                    var usuario = bd.Usuario.Where(p => p.Iidusuario.Equals(idTipoUsuario)).First();//capturamos el objeto 
                    List<Pagina> lista = (from ptu in bd.Paginatipousuario
                                          join pagina in bd.Pagina on ptu.Iidpagina equals pagina.Iidpagina
                                          where ptu.Bhabilitado == "A" && ptu.Iidtipousuario == usuario.Iidtipousuario
                                          && pagina.Bhabilitado == "A"
                                          select new Pagina
                                          {
                                              Mensaje = pagina.Mensaje,
                                              Accion = pagina.Accion,
                                              Controlador = pagina.Controlador,
                                              Icono = pagina.Icono
                                          }).ToList();
                    return lista;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
