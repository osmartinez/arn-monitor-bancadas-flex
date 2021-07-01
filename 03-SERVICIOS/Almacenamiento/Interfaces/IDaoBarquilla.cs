using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IDaoBarquilla
    {
        void Ubicar(string codBarquilla, string codUbicacion);
    }
}
