using Entidades.DB;
using Entidades.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoria
{
    public static class Memoria
    {
        private static List<Store> stores = new List<Store>();


        public static void EntrarOperario(Operarios op, Pantalla p)
        {
            var storeExistente = stores.FirstOrDefault(x => x.Pantalla.Id == p.Id);
            if (storeExistente != null)
            {
                storeExistente.Operario = op;
            }
            else
            {
                stores.Add(new Store
                {
                    Pantalla = p,
                    Operario = op,
                });
            }
        }

        public static void SalirOperario(Operarios op, Pantalla p)
        {
            var storeExistente = stores.FirstOrDefault(x => x.Pantalla.Id == p.Id);
            if (storeExistente != null)
            {
                storeExistente.Operario = null;
            }
        }

        public static List<Maquinas> ObtenerMaquinasPorOperario(Operarios op)
        {
            var storeExistente = stores.FirstOrDefault(x => x.Operario.Id == op.Id);
            return storeExistente.Maquinas;
        }


    }
}
