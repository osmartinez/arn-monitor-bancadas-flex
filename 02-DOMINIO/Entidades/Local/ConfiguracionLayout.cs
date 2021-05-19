using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Local
{
    public class ConfiguracionLayout
    {
        public Guid Id { get; set; }
        public List<Pantalla> Pantallas { get; set; } = new List<Pantalla>();
        public ConfiguracionLayout()
        {
            this.Id = Guid.NewGuid();
            Pantallas = new List<Pantalla>();
        }
    }
}
