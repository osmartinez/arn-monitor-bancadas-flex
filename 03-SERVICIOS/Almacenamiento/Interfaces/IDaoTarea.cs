using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IDaoTarea
    {
        List<SP_BarquillaBuscarInformacionEnSeccion_Result> BuscarInformacionBarquilla(string codEtiqueta, string codSeccion);
        OperacionesControles BuscarControlGuardado(int idOperacion,int tipo);
        void GuardarControl(OperacionesControles control);
        OperacionesControles BuscarControl(int idOperacion, int tipo);
        List<MaquinasRegistrosDatos> ObtenerHistoricoParesOperario(int idOperario, string ipAutomata, int posicion, DateTime fechaIni, DateTime fechaFin);
        List<MaquinasRegistrosDatos> ObtenerHistoricoParesOperario(int idOperario,  DateTime fechaIni, DateTime fechaFin);
        List<OperacionesControles> ObtenerOperacionesControles();
    }
}
