using Entidades.DB;
using Entidades.Enum;
using Horario;
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
    /// Lógica de interacción para VueltasControl.xaml
    /// </summary>
    public partial class VueltasControl : UserControl,INotifyPropertyChanged
    {
        public int Vueltas { get; set; } = 0;
        private List<Maquinas> maquinas = new List<Maquinas>();
        private Operarios operario = null;
        private DispatcherTimer timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 20) };

        public event PropertyChangedEventHandler PropertyChanged;

        public VueltasControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        public void Notifica(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.maquinas.Count > 0 && this.operario !=null)
            {
                Turno turno = HorarioTurnos.CalcularTurnoAFecha(DateTime.Now);
                DateTime inicio = DateTime.Now;
                DateTime fin = DateTime.Now;
                HorarioTurnos.CalcularHorarioTurno(turno, DateTime.Now, out inicio, out fin);
                int maxPulsos = 0;
                Maquinas maxMaquina = null;
                foreach(var maquina in this.maquinas)
                {
                    int cont = maquina.Pulsos.Where(x => x.IdOperario == operario.Id
                    && (inicio <= x.FechaLocal && x.FechaLocal <= fin)).Count();
                    if(cont > maxPulsos)

                    {
                        maxPulsos = cont;
                        maxMaquina = maquina;
                    }
                }
                
                    Vueltas = maxPulsos;
                
            }
            else
            {
                Vueltas = 0;
            }
            Notifica("Vueltas");
        }

        public void SetMaquinas(List<Maquinas> maquinas,Operarios op)
        {
            this.maquinas = maquinas;
            this.operario = op;
        }
    }
}
