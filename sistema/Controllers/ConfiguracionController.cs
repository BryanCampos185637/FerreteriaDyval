﻿using AdminFerreteria.DAL;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ConfiguracionController : Controller
    {
        [ServiceFilter(typeof(FiltroDePaginaTipoUsuario))]
        public IActionResult Index()
        {
            return View();
        }
        ConfiguracionDAL dal = new ConfiguracionDAL();

        public JsonResult getConfiguration()
        {
            return Json(dal.obtenerConfiguracionSistema());
        }
        [HttpPost]
        public int updateConfiguracion(Configuracion configuracion, string usuario,string contra)
        {
            return dal.ActualizarConfiguracion(configuracion, usuario, contra);
        }
    }
}
