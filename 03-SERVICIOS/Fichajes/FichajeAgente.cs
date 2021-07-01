using Entidades.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Fichajes
{
    public static class FichajeAgente
    {
        private static string UltimaEtiqueta { get; set; } = "";
        private static string CodigoBarquilla { get; set; } = "";
        private static string CodigoUtillaje { get; set; } = "";
        private static string CodigoMaquina { get; set; } = "";

        public static event EventHandler<FichajeAsociacionEventArgs> OnFichajeAsociacion;
        public static event EventHandler<FichajeUbicacionUtillajeEventArgs> OnFichajeUbicacionUtillaje;

        private static DispatcherTimer timer = new DispatcherTimer();

        static FichajeAgente()
        {
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (EsEtiquetaMaquina(UltimaEtiqueta) && CodigoBarquilla == "")
            {
                Limpiar();
            }
            timer.Stop();
        }

        private static void Limpiar()
        {
            CodigoBarquilla = "";
            CodigoMaquina = "";
            CodigoUtillaje = "";
            UltimaEtiqueta = "";
        }

        private static void FichajeAsociacion()
        {
            if (OnFichajeAsociacion != null)
            {
                OnFichajeAsociacion(null, new FichajeAsociacionEventArgs(CodigoMaquina, CodigoBarquilla));
            }
            Limpiar();
        }

        private static void FichajeUbicacionUtillaje()
        {
            if (OnFichajeUbicacionUtillaje != null)
            {
                OnFichajeUbicacionUtillaje(null, new FichajeUbicacionUtillajeEventArgs(CodigoUtillaje, CodigoMaquina));
            }
            Limpiar();
        }

        private static bool EsEtiquetaMaquina(string cod)
        {
            return cod.StartsWith("02") || cod.StartsWith("2");
        }

        private static bool EsEtiquetaBarquilla(string cod)
        {
            return cod.StartsWith("05") || cod.StartsWith("5");
        } 
        
        private static bool EsEtiquetaUtillaje(string cod)
        {
            return cod.StartsWith("06") || cod.StartsWith("6");
        }

        public static void EtiquetaFichada(string cod)
        {
            if (cod.Length > 0)
            {
                if (cod[0] == '5')
                {
                    // barquilla
                    BarquillaFichada("0" + cod);
                }
                else if (cod[0] == '2')
                {
                    // maquina
                    MaquinaFichada("0" + cod);

                }
                else if(cod[0] == '6')
                {
                    UtillajeFichado("0" + cod);
                }
            }
           
        }

        private static void BarquillaFichada(string cod)
        {
            // cod es barquilla

            if (EsEtiquetaBarquilla(UltimaEtiqueta))
            {
                CodigoMaquina = "";
                CodigoBarquilla = "";
                UltimaEtiqueta = "";
            }
            else if (EsEtiquetaMaquina(UltimaEtiqueta))
            {
                CodigoBarquilla = cod;
                UltimaEtiqueta = CodigoBarquilla;
                FichajeAsociacion();
            }
        }

        private static void UtillajeFichado(string cod)
        {
            // cod es codigoetiqueta del utillaje

            if (EsEtiquetaMaquina(UltimaEtiqueta))
            {
                CodigoUtillaje = cod;
                UltimaEtiqueta = CodigoUtillaje;
                FichajeUbicacionUtillaje();
            }
        }

        private static void MaquinaFichada(string cod)
        {
            CodigoBarquilla = "";
            CodigoMaquina = cod;
            UltimaEtiqueta = cod;
            timer.Stop();
            timer.Start();
        }
    }
}
