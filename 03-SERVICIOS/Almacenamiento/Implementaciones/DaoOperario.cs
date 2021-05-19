using Almacenamiento.Interfaces;
using Entidades.DB;
using SistemaGlobal.Select.Operarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class DaoOperario : IDaoOperario
    {
        public Operarios BuscarPorCodigo(string cod)
        {
            return Select.BuscarOperarioPorCodigo(cod);
        }

        public void Login(Operarios op)
        {
            
        }

        public void Logout(Operarios op)
        {
            
        }
    }
}
