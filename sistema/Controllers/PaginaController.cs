using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using AdminFerreteria.Models;

namespace AdminFerreteria.Controllers
{
    public class PaginaController : Controller
    {
        public IActionResult Index(int contra)
        {
            if (contra == 1998185637)
            {
                return View();
            }
            else
            {
                return Redirect("/home/index");
            }
        }
        [HttpGet]
        public JsonResult list()
        {
            using(var db = new BDFERRETERIAContext())
            {
                var list = db.Pagina.ToList();
                return Json(list);
            }
        }
        [HttpGet]
        public JsonResult getById(int id)
        {
            using (var db = new BDFERRETERIAContext())
            {
                var list = db.Pagina.Where(p => p.Iidpagina == id).First();
                return Json(list);
            }
        }
        [HttpPost]
        public int save(Pagina pagina)
        {
            try
            {
                using (var db = new BDFERRETERIAContext())
                {
                    if (pagina.Iidpagina == 0)
                    {
                        pagina.Icono= pagina.Icono.ToLower();
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
            catch(Exception e)
            {
                return 0;
            }
        }
        [HttpGet]
        public string eliminar(int id)
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
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
