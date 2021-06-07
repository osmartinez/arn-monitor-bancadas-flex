using Almacenamiento.Interfaces;
using Entidades.Local;
using Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class GuiConfiguracion : IGuiConfiguracion
    {
        public void GuardarConfiguracion(ConfiguracionGlobal cfg)
        {
            Fichero.EscribirConfiguracion(cfg);
        }

        public ConfiguracionGlobal LeerConfiguracion()
        {
            return Fichero.LeerConfiguracion();
        }

        public string ObtenerModo()
        {
            return Fichero.LeerConfiguracion().Modo;
        }

        public int ObtenerNumeroPantallas()
        {
            return Fichero.LeerConfiguracion().ConfiguracionLayoutActiva.Pantallas.Count;
        }

        public List<Pantalla> ObtenerPantallas()
        {
            return Fichero.LeerConfiguracion().ConfiguracionLayoutActiva.Pantallas;
        }
    }
}
