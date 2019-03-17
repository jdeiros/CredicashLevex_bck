using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MvcPicashWeb.Controllers
{
    public class RecuperatorioController : Controller
    {
        // GET: Recuperatorio
        public ActionResult Index()
        {
            return View();
        }

        // GET: Recuperatorio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recuperatorio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recuperatorio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Recuperatorio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recuperatorio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Recuperatorio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recuperatorio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}