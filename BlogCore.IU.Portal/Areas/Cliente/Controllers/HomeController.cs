using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Modelos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BlogCore.IU.Portal.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM()
            {
                Slider = _contenedorTrabajo.Slider.GetAll(),
                listaArticulo = _contenedorTrabajo.Articulo.GetAll()
            };
            return View(homeVm);
            


        }
        public IActionResult Details(int id)
        {
            var articulosdesdeDb = _contenedorTrabajo.Articulo.GetFirsOrDefult(a => a.id == id);
            return View(articulosdesdeDb);
        }

        
    }
}
