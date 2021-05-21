﻿using System;
using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class TipoUsuarioController : Controller
    {
        TipoUsuarioDAL dal = new TipoUsuarioDAL();
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult getTipoUsuarioById(int id)
        {
            return Json(dal.ObtenerTipoUsuario(id));
        }
        [HttpGet]
        public JsonResult listTipoUsuario()
        {
            return Json(dal.Listar());
        }
        [HttpPost]
        public int saveTipoUsuario(Tipousuario tipousuario, int[] idPaginas)
        {
            return dal.guardar(tipousuario, idPaginas);
        }
        [HttpGet]
        public JsonResult listPaginasAsignadas(Int64 id)
        {
            return Json(dal.listarPaginasAsignadas(id));
        }
        [HttpGet]
        public JsonResult listPaginas()
        {
            return Json(dal.listarPaginasExistentes());
        }
        [HttpGet]
        public int deleteTipoUsuario(int id)
        {
            return dal.eliminar(id);
        }
    }
}
