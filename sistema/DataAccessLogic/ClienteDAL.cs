using AdminFerreteria.Controllers;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminFerreteria.DataAccessLogic
{
    public class ClienteDAL
    {
        public List<Cliente> ListarClientes()
        {
            using (var db = new BDFERRETERIAContext())
            {
                List<Cliente> lst = db.Cliente.Where(p => p.Bhabilitado == "A").ToList();
                return lst;
            }
        }
        public int GuardarCliente(Cliente cliente)
        {
            try
            {
                int nveces = UtilidadesController.existCliente(cliente);
                if (nveces == 0)
                {
                    using (var db = new BDFERRETERIAContext())
                    {
                        if (cliente.Iidcliente == 0)
                        {
                            db.Cliente.Add(cliente);
                        }
                        else
                        {
                            var data = db.Cliente.Where(p => p.Iidcliente.Equals(cliente.Iidcliente)).First();
                            data.Nombrecompleto = cliente.Nombrecompleto;
                            data.Direccion = cliente.Direccion;
                            data.Registro = cliente.Registro;
                            data.Giro = cliente.Giro;
                            data.Nit = cliente.Nit;
                        }
                        db.SaveChanges();
                    }
                    return 1;
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
        public Cliente ObtenerClientePorId(Int64 id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                Cliente lst = db.Cliente.Where(p => p.Iidcliente == id).First();
                return lst;
            }
        }
        public int EliminarCliente(Int64 id)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    Cliente lst = db.Cliente.Where(p => p.Iidcliente == id).First();
                    lst.Bhabilitado = "D";
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static int ExistCliente(Cliente cliente)
        {
            using (var db = new BDFERRETERIAContext())
            {
                return db.Cliente.Where(p => p.Iidcliente != cliente.Iidcliente &&
                    p.Nombrecompleto == cliente.Nombrecompleto && p.Registro == cliente.Registro &&
                    p.Nit == cliente.Nit && p.Bhabilitado == "A").Count();
            }
        }
    }
}
