using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGlobal.Select.Puestos
{
    public static class Select
    {
        public static List<Bancadas> ObtenerListaBancadas()
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                return db.Bancadas.ToList();
            }
        }

        public static List<Maquinas> ObtenerMaquinasEnSecciones(List<string> secciones)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                return db.Maquinas.Where(x => secciones.Contains(x.CodSeccion)).ToList();
            }
        }

        public static Bancadas ObtenerBancadaConMaquinas(int idBancada)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var bancada = db.Bancadas
                    .Include("Maquinas")
                    .FirstOrDefault(x => x.ID == idBancada);
                return bancada;

            }
        }

        public static List<MaquinasColasTrabajo> ObtenerColaTrabajoMaquinaPorId(int idMaquina)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var cola = db.MaquinasColasTrabajo
                    .Include("OrdenesFabricacionOperacionesTallasCantidad.OrdenesFabricacionOperacionesTallas.OrdenesFabricacionOperaciones.OrdenesFabricacion.Campos_ERP")
                    .Include("OrdenesFabricacionOperacionesTallasCantidad.OrdenesFabricacionProductos")
                    .Where(m => m.IdMaquina == idMaquina).ToList();


                return cola;
            }
        }

        public static Maquinas ObtenerMaquinaCompleta(int idMaquina)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var maquina = db.Maquinas
                    .Include("MaquinasColasTrabajo" +
                    ".OrdenesFabricacionOperacionesTallasCantidad" +
                    ".OrdenesFabricacionOperacionesProductos")

                    .Include("MaquinasColasTrabajo" +
                    ".OrdenesFabricacionOperacionesTallasCantidad" +
                    ".OrdenesFabricacionOperacionesTallas" +
                    ".OrdenesFabricacionOperaciones" +
                    ".OrdenesFabricacion" +
                    ".Campos_ERP")

                    .FirstOrDefault(x => x.ID == idMaquina);
                return maquina;
            }
        }
    }
}
