using Almacenamiento.Interfaces;
using Entidades.DB;
using SistemaGlobal.Select.Tarea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class DaoTarea : IDaoTarea
    {
        public OperacionesControles BuscarControl(int idOperacion, int tipo)
        {
            return Select.BuscarControlOperacion(idOperacion, tipo);
        }

        public OperacionesControles BuscarControlGuardado(int idOperacion,int tipo)
        {
            var ctrl =  Memoria.Memoria.BuscarControl(idOperacion);
            if(ctrl == null)
            {
                ctrl= BuscarControl(idOperacion, tipo);
            }
            return ctrl;
        }

        public List<SP_BarquillaBuscarInformacionEnSeccion_Result> BuscarInformacionBarquilla(string codEtiqueta, string codSeccion)
        {
            return Select.BuscarInformacionBarquilla(codEtiqueta, codSeccion);
        }

        public void GuardarControl(OperacionesControles control)
        {
            Memoria.Memoria.GuardarControl(control);

        }

        public List<MaquinasRegistrosDatos> ObtenerHistoricoParesOperario(int idOperario, string ipAutomata, int posicion, DateTime fechaIni, DateTime fechaFin)
        {
            return Select.ObtenerHistoricoParesOperario(idOperario, ipAutomata, posicion, fechaIni, fechaFin);
        }

        public List<MaquinasRegistrosDatos> ObtenerHistoricoParesOperario(int idOperario, DateTime fechaIni, DateTime fechaFin)
        {
            return Select.ObtenerHistoricoParesOperario(idOperario, fechaIni, fechaFin);
        }

        public List<OperacionesControles> ObtenerOperacionesControles()
        {
            return Select.ObtenerOperacionesControles();
        }
    }
}
