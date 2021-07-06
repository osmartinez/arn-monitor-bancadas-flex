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
        public static void UbicarBarquilla(Dictionary<int,int> ordenesOperaciones, string seccion, string codBarquilla ,string codUbicacion)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                var barquilla = db.Barquillas.FirstOrDefault(x => x.CodigoEtiqueta == codBarquilla);
                if(barquilla != null)
                {
                    db.SP_BarquillaUbicar(codBarquilla, codUbicacion);
                    foreach(var contenido in barquilla.BarquillasContenidos)
                    {
                        var parEncontrados = ordenesOperaciones.Where(x => x.Key == contenido.IdOrden);
                        if(parEncontrados.Any())
                        {
                            db.BarquillasConsumos.Add(new BarquillasConsumos
                            {
                                FechaConsumo = DateTime.Now,
                                IdContenidoBarquilla = contenido.Id,
                                IdOperacionConsumo = parEncontrados.First().Value,
                                CodSeccion = seccion,

                            });
                        }
                        
                    }

                }
                db.SaveChanges();
            }
        }
    }
}
