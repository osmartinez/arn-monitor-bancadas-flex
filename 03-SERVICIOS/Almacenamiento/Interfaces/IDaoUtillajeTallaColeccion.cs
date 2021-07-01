using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IDaoUtillajeTallaColeccion
    {
        void Ubicar(string codEtiqueta, string codUbicacion);
    }
}
