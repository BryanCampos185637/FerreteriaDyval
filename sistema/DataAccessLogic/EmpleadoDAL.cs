using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace AdminFerreteria.DataAccessLogic
{
    public class EmpleadoDAL
    {
        public static int ExistUsuario(Usuario oUsuario)
        {
            using (var db = new BDFERRETERIAContext())
            {
               return db.Usuario.Where(p => p.Iidusuario != oUsuario.Iidusuario &&
                    p.Nombreusuario.ToLower() == oUsuario.Nombreusuario.ToLower() &&
                    p.Bhabilitado == "A").Count();
            }
        }
        public static int ExistEmpleado(Empleado oEmpleado)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var nveces = db.Empleado.Where(p => p.Iidempleado != oEmpleado.Iidempleado &&
                    p.Dui.ToLower() == oEmpleado.Dui.ToLower() &&
                    p.Bhabilitado == "A").Count();
                return nveces;
            }
        }
        public static Empleado ObtenerElPrimerEmpleado()
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Empleado.Where(p => p.Iidempleado == 2).First();
            }
        }
        public List<Empleado> ListarEmpleados()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<Empleado> lst = db.Empleado.Where(p => p.Bhabilitado == "A").ToList();
                lst = lst.OrderByDescending(p => p.Iidempleado).ToList();
                return lst;
            }
        }
        public Empleado ObtenerEmpleado(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                Empleado oEmpleado = db.Empleado.Where(p => p.Iidempleado == id).First();
                return oEmpleado;
            }
        }
        public int EliminarEmpleado(int id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    using (var transaction = new TransactionScope())
                    {
                        if (id > 1)
                        {
                            Empleado oEmpleado = db.Empleado.Where(p => p.Iidempleado == id).First();
                            Usuario oUsuario = db.Usuario.Where(p => p.Iidempleado == id).FirstOrDefault();
                            oEmpleado.Bhabilitado = "D";
                            db.SaveChanges();
                            if (oUsuario != null)
                            {
                                oUsuario.Bhabilitado = "D";
                                db.SaveChanges();
                            }
                        }
                        transaction.Complete();
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int GuardarEmpleado(Empleado empleado, Usuario usuario)
        {
            try
            {
                if (UtilidadesController.existEmpleado(empleado) == 0)
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        using (var transaction = new TransactionScope())
                        {
                            if (empleado.Iidempleado == 0)
                            {
                                empleado.Fechacreacion = DateTime.Now;
                                empleado.Empleadotieneusuario = "S";
                                db.Empleado.Add(empleado);
                                db.SaveChanges();
                                //ahora guardamos el usuario
                                if (UtilidadesController.existUsuario(usuario) == 0)
                                {
                                    usuario.Iidempleado = empleado.Iidempleado;
                                    usuario.Fechacreacion = DateTime.Now;
                                    usuario.Contraseña = UtilidadesController.encryptPassword(usuario.Contraseña);
                                    db.Usuario.Add(usuario);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    return -2;
                                }
                            }
                            else
                            {
                                Empleado oEmpleado = db.Empleado.Where(p => p.Iidempleado.Equals(empleado.Iidempleado)).First();
                                oEmpleado.Nombrecompleto = empleado.Nombrecompleto;
                                oEmpleado.Edad = empleado.Edad;
                                oEmpleado.Telefono = empleado.Telefono;
                                oEmpleado.Dui = empleado.Dui;
                                db.SaveChanges();
                                if (UtilidadesController.existUsuario(usuario) == 0)
                                {
                                    Usuario oUsuario = db.Usuario.Where(p => p.Iidusuario.Equals(usuario.Iidusuario)).First();
                                    if (oUsuario.Iidempleado > 1)//el primer usuario no se puede cambiar el rol ya que es el dueño
                                        oUsuario.Iidtipousuario = usuario.Iidtipousuario;
                                    if (oUsuario.Contraseña != usuario.Contraseña)
                                    {
                                        oUsuario.Contraseña = UtilidadesController.encryptPassword(usuario.Contraseña);
                                    }
                                    db.SaveChanges();
                                }
                                else
                                {
                                    return -2;
                                }
                            }
                            transaction.Complete();
                            return 1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public List<Tipousuario> ListarTipoUsuario()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<Tipousuario> lst = db.Tipousuario.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public object ObtenerUsuario(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var data = db.Usuario.Where(p => p.Iidempleado == id && p.Bhabilitado == "A").FirstOrDefault();
                return data;
            }
        }
    }
}
