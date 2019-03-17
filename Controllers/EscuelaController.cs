using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcPicashWeb.Models;

namespace MvcPicashWeb.Controllers
{
    public class EscuelaController : Controller
    {
        

        public IActionResult Index()
        {
            /*
             * bolsa de cosas dinamicas, es lo mismo que ViewData["CosaDinamica"]
             * son distintas formas de accedera a info extra que le mando desde aca
             */
            ViewBag.Date = DateTime.Now; //por ser dynamic le puedo asignar cualquier cosa, en este caso un datetime

            var escuela = _context.Escuelas.FirstOrDefault();

            return View(escuela);
        }

        private ApplicationDbContext _context;
        public EscuelaController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}