using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGlobal.Select.Tarea
{
    public static class Select
    {
        public static List<SP_BarquillaBuscarInformacionEnSeccion_Result> BuscarInformacionBarquilla(string codEtiqueta, string codSeccion)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                return db.SP_BarquillaBuscarInformacionEnSeccion(codEtiqueta, codSeccion).ToList();
            }
        }

        public static OperacionesControles BuscarControlOperacion(int idOfo, int idTipoMaquina)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var ofo = db.OrdenesFabricacionOperaciones.Find(idOfo);
                if (ofo.IdOperacionMaestra == null)
                {
                    return OperacionesControles.Default;
                }
                var control = db.OperacionesControles.FirstOrDefault(x => x.IdOperacion == ofo.IdOperacionMaestra && x.IdTipoMaquina == idTipoMaquina);
                if (control == null)
                {
                    return OperacionesControles.Default;

                }
                else
                {
                    return control;
                }
            }
        }

        public static List<MaquinasRegistrosDatos> ObtenerHistoricoParesOperario(int idOperario, DateTime fechaInicio, DateTime fechaFin)
        {
            fechaInicio = fechaInicio.ToUniversalTime();
            fechaFin = fechaFin.ToUniversalTime();
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var registros =
                db.MaquinasRegistrosDatos
                    .Where(x => x.IdOperario == idOperario &&
                (fechaInicio <= x.Fecha && x.Fecha <= fechaFin)).ToList();

                foreach (var registro in registros)
                {
                    if (registro.IdTarea != 0)
                    {
                        registro.OrdenesFabricacionOperacionesTallasCantidad = db.OrdenesFabricacionOperacionesTallasCantidad
                            .Include("OrdenesFabricacionOperacionesTallas.OrdenesFabricacionOperaciones.OrdenesFabricacion")
                            .FirstOrDefault(x => x.ID == registro.IdTarea);
                    }
                }

                return registros;
            }
        }

        public static List<MaquinasRegistrosDatos> ObtenerHistoricoParesOperario(int idOperario, string ipAutomata, int posicion, DateTime fechaInicio, DateTime fechaFin)
        {
            fechaInicio = fechaInicio.ToUniversalTime();
            fechaFin = fechaFin.ToUniversalTime();
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var registros =
                db.MaquinasRegistrosDatos
                    .Where(x => x.IdOperario == idOperario &&
                    x.IpAutomata == ipAutomata &&
                    x.PosicionMaquina == posicion &&
                (fechaInicio <= x.Fecha && x.Fecha <= fechaFin)).ToList();

                foreach (var registro in registros)
                {
                    if (registro.IdTarea != 0)
                    {
                        registro.OrdenesFabricacionOperacionesTallasCantidad = db.OrdenesFabricacionOperacionesTallasCantidad
                            .Include("OrdenesFabricacionOperacionesTallas.OrdenesFabricacionOperaciones.OrdenesFabricacion")
                            .FirstOrDefault(x => x.ID == registro.IdTarea);
                    }
                }

                return registros;
            }
        }

        public static List<OperacionesControles> ObtenerOperacionesControles()
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                return db.OperacionesControles.Include("MaquinasTipos.Maquinas").Include("Operaciones").ToList();
            }
        }
    }
}
