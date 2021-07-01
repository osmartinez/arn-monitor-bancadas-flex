using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGlobal.Update.UtillajesTallasColeccion
{
    public static class Update
    {
        public static void Ubicar(string codigoEtiqueta, string codUbicacion)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var utc = db.UtillajesTallasColeccion.FirstOrDefault(x => x.CodigoEtiqueta == codigoEtiqueta);
                if(utc != null)
                {
                    utc.CodUbicacion = codUbicacion;
                }
                db.SaveChanges();
            }
        }
    }
}
