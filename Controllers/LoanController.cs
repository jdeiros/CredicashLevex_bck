using Microsoft.AspNetCore.Mvc;
using MvcPicashWeb.Models;
using System;

namespace MvcPicashWeb.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            var loan = new Loan
            {
                AñoFundacion = 2005,
                LoanId = Guid.NewGuid().ToString(),
                Nombre = "platzi school"
            };

            /*
             * bolsa de cosas dinamicas, es lo mismo que ViewData["CosaDinamica"]
             * son distintas formas de accedera a info extra que le mando desde aca
             */
            ViewBag.CosaDinamica = "La monja"; 

            return View(loan);
        }
    }
}
