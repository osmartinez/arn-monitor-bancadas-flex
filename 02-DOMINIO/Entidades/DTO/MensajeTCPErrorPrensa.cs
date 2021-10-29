using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public class MensajeTCPErrorPrensa: MensajeTCPPrensa
    {
        public MensajeTCPErrorPrensa()
        {
            this.Tipo = Enum.TipoMensajeTCP.errorPrensa;
        }
        public double ParesFabricar { get; set; }
        public double ParesFabricados { get; set; }
        public override string ToTexto()
        {
            return String.Format("{0};{1};{2}/{3}", (int)this.Tipo, this.NombrePrensa, ParesFabricados, ParesFabricar);
        }
    }
}
