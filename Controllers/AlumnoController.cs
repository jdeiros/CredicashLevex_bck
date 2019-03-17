using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcPicashWeb.Models;

namespace MvcPicashWeb.Controllers
{
    public class AlumnoController : Controller
    {
        public IActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var alumno = from alumn in _context.Alumnos
                                 where alumn.Id == id
                                 select alumn;

                return View(alumno.SingleOrDefault());
            }
            else
            {
                return View("MultiAlumno", _context.Alumnos);
            }
        }

        public IActionResult MultiAlumno()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;

            return View("MultiAlumno", _context.Alumnos);
        }

        private ApplicationDbContext _context;
        public AlumnoController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}