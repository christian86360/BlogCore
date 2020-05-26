using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Modelos.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BlogCore.IU.Portal.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEvironment = hostingEvironment;

        }
        [HttpGet]
        public IActionResult Index()
        {
           
        return View();

        }
        [HttpGet]
        public IActionResult Create()
        {
            // ArticuloVM artivm = new ArticuloVM()
            // {
            //// ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias()
            //};
            ArticuloVM articuloVM = new ArticuloVM();
               articuloVM.ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(articuloVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if(artiVM.Articulo.id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using(var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    artiVM.Articulo.UrlImagen = @"imagenes\articulos\" + nombreArchivo + extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

      [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Modelos.Articulo(),
                ListaCategoria = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
           if(id != null)
            {
                artivm.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }

            return View(artivm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesDb = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.id);
                if (archivos.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal,articuloDesDb.UrlImagen.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    artiVM.Articulo.UrlImagen = @"imagenes\articulos\" + nombreArchivo + nuevaExtension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //aqui es cuando la imagen ya existe y no se remplaza se conserva
                    artiVM.Articulo.UrlImagen = articuloDesDb.UrlImagen;
                
                }
                _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        } 
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostingEvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            if(articuloDesdeDb == null)
            {
                return Json(new { success = false, message = "Error al borrar artículo" });
                
            }
            _contenedorTrabajo.Articulo.Remove(articuloDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = false, message = "Artículo  borrado con éxito" });
        }
        #region LLamadas a la Api


        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }

    
        #endregion
    }
}