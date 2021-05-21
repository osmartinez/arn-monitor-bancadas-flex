using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Eventos
{
    public class FichajeAsociacionEventArgs:EventArgs
    {
        public DateTime Fecha { get; set; }
        public string CodigoMaquina { get; set; }
        public string CodigoBarquilla { get; set; }
        public OperacionesControles Control { get; set; }

        public FichajeAsociacionEventArgs(string codigoMaquina, string codigoBarquilla)
        {
            CodigoMaquina = codigoMaquina;
            CodigoBarquilla = codigoBarquilla;
            Fecha = DateTime.Now;
        }
    }
}
