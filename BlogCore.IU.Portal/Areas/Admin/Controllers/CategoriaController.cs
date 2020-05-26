using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.IU.Portal.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriaController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null) 
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        #region LLamadas a la Api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Categoria.Get(id);
            if(objFromDb == null)
            {
                return Json(new { sucess = false, message = "Error borrando Categoria." });
            }
            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { sucess = true, message = "Categoria borrada corectamente." });
        }

        #endregion
    }
}