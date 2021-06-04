using AdminFerreteria.DataAccessLogic;
using AdminFerreteria.Models;
using System;
using System.Collections.Generic;

namespace AdminFerreteria.BussinesLogic
{
    public class EmpleadoBL
    {
        EmpleadoDAL dal = new EmpleadoDAL();
        public static Empleado obtenerElPrimerEmpleado()
        {
            return EmpleadoDAL.ObtenerElPrimerEmpleado();
        }
        public List<Empleado> listarEmpleados()
        {
            return dal.ListarEmpleados();
        }
        public Empleado obtenerEmpleado(int id)
        {
            return obtenerEmpleado(id);
        }
        public int eliminar(int id)
        {
            return eliminar(id);
        }
        public int guardarEmpleado(Empleado empleado, Usuario usuario)
        {
            return dal.GuardarEmpleado(empleado, usuario);
        }
        public List<Tipousuario> listarTipoUsuario()
        {
            return dal.ListarTipoUsuario();
        }
        public object obtenerUsuario(Int64 id)
        {
            return dal.ObtenerUsuario(id);
        }
    }
}
