using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Local
{
    public class Pantalla
    {
        public Guid Id { get; set; }
        public List<Maquinas> Maquinas { get; set; } = new List<Maquinas>();

        public Pantalla()
        {
            this.Id = Guid.NewGuid();
            this.Maquinas = new List<Maquinas>();
        }
    }
}
