using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Interfaces
{
    public interface IDaoOperario
    {
        Operarios BuscarPorCodigo(string cod);
        void Login(Operarios op);
        void Logout(Operarios op);
    }
}
