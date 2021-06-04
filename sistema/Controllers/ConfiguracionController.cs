﻿using AdminFerreteria.BussinesLogic;
using AdminFerreteria.Helper.HelperSeguridad;
using AdminFerreteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminFerreteria.Controllers
{
    [ServiceFilter(typeof(FiltroDeAcciones))]
    public class ConfiguracionController : Controller
    {
        [ServiceFilter(typeof(FiltroDeAutenticacionValidacion))]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult _FormularioRegistro()
        {
            return View();
        }
        ConfiguracionBL dal = new ConfiguracionBL();

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
