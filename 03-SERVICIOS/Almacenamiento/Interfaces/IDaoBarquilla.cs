using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IDaoBarquilla
    {
        void Ubicar(Dictionary<int,int> idOperacion, string codSeccion, string codBarquilla, string codUbicacion);
    }
}
