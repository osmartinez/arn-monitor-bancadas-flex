using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Eventos
{
    public class MensajeTCPRecibidoEventArgs:EventArgs
    {
        public string Mensaje { get; private set; }

        public MensajeTCPRecibidoEventArgs(string mensaje)
        {
            Mensaje = mensaje;
        }
    }
}
