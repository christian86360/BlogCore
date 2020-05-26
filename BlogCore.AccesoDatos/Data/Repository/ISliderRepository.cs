using BlogCore.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
  public interface ISliderRepository : IRepository<Slider>
    {
        void Update(Slider slider);
    }
}
