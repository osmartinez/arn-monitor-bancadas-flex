using Entidades.DB;
using Entidades.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Eventos
{
    public class OperarioEntraEventArgs:EventArgs
    {
        public Operarios Operario { get; set; }
        public Pantalla Pantalla { get; set; }
        public OperarioEntraEventArgs(Operarios operario, Pantalla pantalla)
        {
            Operario = operario;
            Pantalla = pantalla;
        }
    }
}
