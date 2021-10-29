using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public class MensajeTCPFichaje:MensajeTCP
    {
        public string CodigoBarquilla { get; set; }
        public string CodigoMaquina { get; set; }
        public override string ToTexto()
        {
            return String.Format("{0};{1};{2}", (int)this.Tipo, this.CodigoMaquina,this.CodigoBarquilla);
        }
    }
}
