using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ClienteController : Controller
    {
        ClienteDAL dal = new ClienteDAL();
        [ServiceFilter(typeof(FiltroDePaginaTipoUsuario))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listCliente()
        {
            return Json(dal.listar());
        }
        [HttpPost]
        public int saveCliente(Cliente cliente)
        {
            cliente.Fechacreacion = DateTime.Now;
            return dal.guardar(cliente);
        }
        [HttpGet]
        public JsonResult getClienteById(Int64 id)
        {
            return Json(dal.obtenerPorId(id));
        }
        [HttpGet]
        public int deleteCliente(Int64 id)
        {
            return dal.eliminar(id);
        }
    }
}
