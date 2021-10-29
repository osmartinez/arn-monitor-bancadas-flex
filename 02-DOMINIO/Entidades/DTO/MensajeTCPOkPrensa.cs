using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public class MensajeTCPOkPrensa:MensajeTCPPrensa
    {
        public MensajeTCPOkPrensa()
        {
            this.Tipo = Enum.TipoMensajeTCP.okPrensa;
        }
        public override string ToTexto()
        {
            return String.Format("{0};{1}", (int)this.Tipo, this.NombrePrensa);

        }
    }
}
