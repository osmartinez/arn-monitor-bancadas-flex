using Entidades.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MonitorWPF.Controles
{
    /// <summary>
    /// Lógica de interacción para RitmoControl.xaml
    /// </summary>
    public partial class RitmoControl : UserControl,INotifyPropertyChanged
    {
        public double RitmoHora { get; set; } = 0;
        private List<Maquinas> maquinas = new List<Maquinas>();
        private Operarios operario = null;
        private DispatcherTimer timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 5) };

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notifica(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RitmoControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Cada x segundos mira cuantos pulsos hay registrados y calcula el ritmo/hora
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.operario == null ||maquinas.Count == 0) return;

            var pulsos = maquinas.SelectMany(x => x.Pulsos.Where(y => y.IdOperario == this.operario.Id)).OrderBy(x=>x.Fecha).ToList();
            if (pulsos.Count > 2)
            {
                var ini = pulsos.First();
                var fin = pulsos.Last();
                var paresTotales = pulsos.Sum(x => x.Pares);
                var horasTotales = fin.Fecha.Subtract(ini.Fecha).TotalHours;

                /*
                 * paresTotales -> horasTotales
                 * x            -> 1
                 */

                var paresHora = paresTotales / horasTotales;

                RitmoHora = paresHora;
                Notifica("RitmoHora");

            }
            
        }

        /// <summary>
        /// asigna el conjunto de maquinas y el operario para que pueda calcular el ritmo cada intervalo
        /// </summary>
        /// <param name="op"></param>
        /// <param name="maquinas"></param>
        public void SetMaquinas(Operarios op,List<Maquinas> maquinas)
        {
            this.operario = op;
            this.maquinas = maquinas;
        }
    }
}
