using Entidades.DB;
using Entidades.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IGuiOperario
    {
        List<Maquinas> ObtenerMisMaquinas(Operarios op);
        void Entrar(Pantalla p, Operarios op);
        void Salir(Pantalla p, Operarios op);
    }
}
