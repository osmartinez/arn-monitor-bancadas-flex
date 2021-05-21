using Almacenamiento.Interfaces;
using Entidades.DB;
using Entidades.Local;
using Memoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class GuiOperario : IGuiOperario
    {
        public void Entrar(Pantalla p, Operarios op)
        {
            Memoria.Memoria.EntrarOperario(op,p);
        }

        public List<Maquinas> ObtenerMisMaquinas(Operarios op)
        {
            return Memoria.Memoria.ObtenerMaquinasPorOperario(op);
        }

        public List<Maquinas> ObtenerMisMaquinas(Pantalla p)
        {
            return Memoria.Memoria.ObtenerMaquinasPorPantalla(p);
        }

        public void Salir(Pantalla p, Operarios op)
        {
            Memoria.Memoria.SalirOperario(op, p);
        }
    }
}
