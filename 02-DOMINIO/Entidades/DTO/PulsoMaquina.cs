using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
   public  class PulsoMaquina
    {
        public bool PiezaIntroducida { get; set; }
        public string CodigoEtiqueta { get; set; }
        public OperacionesControles Control { get; set; }
        public int Pares { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaLocal
        {
            get
            {
                return Fecha.ToLocalTime();
            }
        }
        public double Ciclo { get; set; }
        public int PosicionGlobal { get; set; }
        public int IdOperario { get; set; }
    }
}
