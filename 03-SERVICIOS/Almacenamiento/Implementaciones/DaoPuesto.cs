using Almacenamiento.Interfaces;
using Entidades.DB;
using SistemaGlobal.Select.Puestos;
using SistemaGlobal.Update.Puestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class DaoPuesto : IDaoPuesto
    {
        public List<MaquinasColasTrabajo> ActualizarColaTrabajo(string codigoBarquilla, List<int> idsTareas, int? agrupacion, int idMaquina, int idOperario, double cantidad)
        {
            return Update.ActualizarColaTrabajo(codigoBarquilla, idsTareas, agrupacion, idMaquina, idOperario, cantidad);
        }

        public List<MaquinasColasTrabajo> ObtenerColaTrabajoMaquina(int idMaquina)
        {
            return Select.ObtenerColaTrabajoMaquinaPorId(idMaquina);
        }

        public Maquinas ObtenerMaquinaConColaTrabajo(int idMaquina)
        {
            return Select.ObtenerMaquinaConColaTrabajo(idMaquina);
        }

        public List<Maquinas> ObtenerMaquinasEnSecciones(List<string> secciones)
        {
            return Select.ObtenerMaquinasEnSecciones(secciones);
        }
    }
}
