using BlogCore.AccesoDatos.Data;
using BlogCore.Modelos;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BlogCore.AccesoDatos.Inicializador
{
    public class Inicializador : IInicializador
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Inicializador(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void IInicializador()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
            if (_db.Roles.Any(ro => ro.Name == CNT.Admin)) return;
            _roleManager.CreateAsync(new  IdentityRole(CNT.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Usuario)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "clugo",
                Email = "christian86360@gmail.com",
                EmailConfirmed = true,
                Nombre = "christian lugo marroquin"
            }, "Admin123*").GetAwaiter().GetResult();
            ApplicationUser usuario = _db.ApplicationUser.Where(us => us.Email == "christian86360@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(usuario, CNT.Admin).GetAwaiter().GetResult();
        }
    }
}
