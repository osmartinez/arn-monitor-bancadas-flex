using Almacenamiento.Interfaces;
using Entidades.DB;
using SistemaGlobal.Select.Puestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class DaoPuesto : IDaoPuesto
    {
        public List<Maquinas> ObtenerMaquinasEnSecciones(List<string> secciones)
        {
            return Select.ObtenerMaquinasEnSecciones(secciones);
        }
    }
}
