using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Modelos.ViewModel
{
  public  class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Articulo> listaArticulo { get; set; }
    }
}
