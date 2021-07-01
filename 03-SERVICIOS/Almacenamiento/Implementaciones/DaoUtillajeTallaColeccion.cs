using Almacenamiento.Interfaces;
using SistemaGlobal.Update.UtillajesTallasColeccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class DaoUtillajeTallaColeccion : IDaoUtillajeTallaColeccion
    {
        public void Ubicar(string codEtiqueta, string codUbicacion)
        {
            Update.Ubicar(codEtiqueta, codUbicacion);
        }
    }
}
