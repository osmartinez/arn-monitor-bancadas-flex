using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Local
{
    public class Store
    {
        public Operarios Operario { get; set; }
        public Pantalla Pantalla { get; set; }

        public List<Maquinas> Maquinas
        {
            get
            {
                if(Pantalla!=null)
                {
                    return Pantalla.Maquinas;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
