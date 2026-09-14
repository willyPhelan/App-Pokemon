using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioPokemon {
    public class Pokemon {

    public int Numero { get ; set ; }

    public string Nombre { get ; set ; } 

    public string Descripcion { get ; set ; }

   public string ImagenUrl { get ; set; } // La propiedad para obtener el la imagen

    public Elemento Tipo { get ; set ; }

    public Elemento Debilidad { get ; set ; }
                                         

    }
}
