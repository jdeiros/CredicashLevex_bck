using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcPicashWeb.Models;

namespace MvcPicashWeb.Controllers
{
    public class AsignaturaController : Controller
    {
        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{asignaturaId}")]
        public IActionResult Index(string asignaturaId)
        {
            if(!string.IsNullOrEmpty(asignaturaId))
            {
                var asignatura = from asig in _context.Asignaturas
                                 where asig.Id == asignaturaId
                                 select asig;

                return View(asignatura.SingleOrDefault());
            }
            else
            {
                return View("MultiAsignatura", _context.Asignaturas);
            }
        }

        public IActionResult MultiAsignatura()
        {            

            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiAsignatura", _context.Asignaturas);
        }

        private ApplicationDbContext _context;
        public AsignaturaController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}