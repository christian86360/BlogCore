using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Modelos;
using System;
using System.Linq;


namespace BlogCore.AccesoDatos.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloqueUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDb.LockoutEnd = DateTime.Now.AddYears(100);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
