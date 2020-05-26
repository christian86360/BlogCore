using BlogCore.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListaCategorias();
        void Update(Categoria categoria);
    }
}
