using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGlobal.Select.Operarios
{
    public static class Select
    {
        public static Entidades.DB.Operarios BuscarOperarioPorCodigo(string cod)
        {
            using(SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                return db.Operarios.FirstOrDefault(x => x.CodigoObrero.Contains(cod));
            }
        } 

        public static void Login(Entidades.DB.Operarios op)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {
                
            }
        }

        public static void Logout(Entidades.DB.Operarios op)
        {
            using (SistemaGlobalPREEntities db = new SistemaGlobalPREEntities())
            {

            }
        }
    }
}
