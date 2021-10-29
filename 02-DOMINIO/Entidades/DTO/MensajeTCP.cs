using Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public abstract class MensajeTCP
    {
        public TipoMensajeTCP Tipo { get; set; }
        public virtual string ToTexto() { return ((int)Tipo).ToString(); }
    }
}
