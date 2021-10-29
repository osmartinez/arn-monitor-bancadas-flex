using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Enum
{
    public enum TipoMensajeTCP
    {
        ack=-1,
        cambiarIpPda= 0,
        fichaje = 1,
        errorPrensa= 2,
        okPrensa=3,
    }
}
