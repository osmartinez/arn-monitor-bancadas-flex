using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Eventos
{
    public class OperarioSaleEventArgs: EventArgs
    {
        public Operarios Operario { get; set; }

        public OperarioSaleEventArgs(Operarios operario)
        {
            Operario = operario;
        }
    }
}
