using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Eventos
{
    public class FichajeUbicacionUtillajeEventArgs:EventArgs
    {
        public string CodigoEtiqueta { get; set; }
        public string CodigoUbicacion { get; set; }

        public FichajeUbicacionUtillajeEventArgs(string codigoEtiqueta, string codigoUbicacion)
        {
            CodigoEtiqueta = codigoEtiqueta;
            CodigoUbicacion = codigoUbicacion;
        }
    }
}
