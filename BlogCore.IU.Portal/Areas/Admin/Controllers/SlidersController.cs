using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.IU.Portal.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SlidersController(IContenedorTrabajo contenedorTrabajo,IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }
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
        public  IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                //Nuevo Articulo
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal,@"imagenes\sliders");
                var extension = Path.GetExtension(archivos[0].FileName);

                using(var fileStreams = new FileStream(Path.Combine(subidas,nombreArchivo+extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                slider.UrlImagen = @"imagenes\sliders\" + nombreArchivo + extension;
                _contenedorTrabajo.Slider.Add(slider);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                var slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var sliderDesdeDb = _contenedorTrabajo.Slider.Get(slider.id);
                if (archivos.Count() > 0)
                {
                    string nombreAchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeDb.UrlImagen.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    using(var filesStream = new FileStream(Path.Combine(subidas, nombreAchivo + nuevaExtencion), FileMode.Create))
                    {
                        archivos[0].CopyTo(filesStream);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreAchivo + nuevaExtencion;
                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.UrlImagen = sliderDesdeDb.UrlImagen;
                }
                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #region LLAMADAS A LA API
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Slider.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }
            _contenedorTrabajo.Slider.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = false, message = "Slider borrado" });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        #endregion
    }
}
