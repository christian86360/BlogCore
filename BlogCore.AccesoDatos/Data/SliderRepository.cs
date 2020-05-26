using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(s => s.id == slider.id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;
        }
    }
}
