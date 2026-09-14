using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioPokemon {

    public class Elemento {

        public int Id { get ; set ; }

        public string Descripcion { get; set ; }

        public override string ToString() {

                return Descripcion ; // sobreescribo el metodo toString para que traiga una prop de un obj determinado
        }


    }
}
