using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Eventos
{
    public class CambiarIpPDAEventArgs:EventArgs
    {
        public string IpPDA { get; private set; }

        public CambiarIpPDAEventArgs(string ipPDA)
        {
            IpPDA = ipPDA;
        }
    }
}
