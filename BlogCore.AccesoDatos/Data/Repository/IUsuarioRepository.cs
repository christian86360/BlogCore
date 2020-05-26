using BlogCore.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Repository
{
   public interface IUsuarioRepository: IRepository<ApplicationUser>
    {
        void BloqueUsuario(string IdUsuario);
        void DesbloquearUsuario(string IdUsuario);

    }
}
