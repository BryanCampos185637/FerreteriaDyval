using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDAL dal = new ClienteDAL();
        public IActionResult Index()
        {
            int? idUsuario = 0;
            idUsuario = HttpContext.Session.GetInt32("UsuarioLogueado");
            if (idUsuario > 0 && idUsuario != null)
            {
                if (UtilidadesController.youHavePermissionToViewPage("cliente", "index", (int)idUsuario))
                {
                    return View();
                }
                else
                {
                    return Redirect("/Home/error");
                }
            }
            else
            {
                return Redirect("/Login/index");
            }
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
