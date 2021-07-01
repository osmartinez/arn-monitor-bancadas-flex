using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGlobal.Update.Barquillas
{
    public static class Update
    {
        public static void UbicarBarquilla(string codBarquilla ,string codUbicacion)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var barquilla = db.Barquillas.FirstOrDefault(x => x.CodigoEtiqueta == codBarquilla);
                if(barquilla != null)
                {
                    barquilla.CodUbicacion = codUbicacion;
                }
                db.SaveChanges();
            }
        }
    }
}
