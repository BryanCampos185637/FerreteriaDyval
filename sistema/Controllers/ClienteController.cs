using System;
using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ClienteController : Controller
    {
        ClienteBL bl = new ClienteBL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listCliente()
        {
            return Json(bl.listar());
        }
        [HttpPost]
        public int saveCliente(Cliente cliente)
        {
            cliente.Fechacreacion = DateTime.Now;
            return bl.guardar(cliente);
        }
        [HttpGet]
        public JsonResult getClienteById(Int64 id)
        {
            return Json(bl.obtenerPorId(id));
        }
        [HttpGet]
        public int deleteCliente(Int64 id)
        {
            return bl.eliminar(id);
        }
    }
}
