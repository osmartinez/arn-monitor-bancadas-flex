using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public class MensajeTCPCambioIpPda: MensajeTCP
    {
        public string Ip { get; set; }
        public override string ToTexto()
        {
            return String.Format("{0};{1}", (int)this.Tipo, this.Ip);
        }
    }
}
