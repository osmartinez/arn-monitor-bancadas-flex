using Entidades.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IGuiConfiguracion
    {
        string ObtenerModo();
        int ObtenerNumeroPantallas();
        List<Pantalla> ObtenerPantallas();
        void GuardarConfiguracion(ConfiguracionGlobal cfg);
        ConfiguracionGlobal LeerConfiguracion();
    }
}
