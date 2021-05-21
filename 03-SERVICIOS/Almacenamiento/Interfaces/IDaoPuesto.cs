using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IDaoPuesto
    {
        List<Maquinas> ObtenerMaquinasEnSecciones(List<string> secciones);
        List<MaquinasColasTrabajo> ObtenerColaTrabajoMaquina(int idMaquina);
        List<MaquinasColasTrabajo> ActualizarColaTrabajo(string codigoBarquilla, List<int> idsTareas, int? agrupacion, int idMaquina, int idOperario, double cantidad);
    }
}
