using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface ILog
    {
        void Escribir(string msg);
        void Escribir(Exception ex);
        string Leer();
    }
}
