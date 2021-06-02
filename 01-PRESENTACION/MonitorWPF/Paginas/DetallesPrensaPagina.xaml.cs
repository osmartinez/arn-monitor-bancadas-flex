using Almacenamiento.Implementaciones;
using Entidades.DB;
using Entidades.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MonitorWPF.Paginas
{
    /// <summary>
    /// Lógica de interacción para DetallesPrensaPagina.xaml
    /// </summary>
    public partial class DetallesPrensaPagina : Page
    {
        public ObservableCollection<ClaveValor<string, string>> Informacion { get; set; } = new ObservableCollection<ClaveValor<string, string>>();
        public event EventHandler OnSalir;
        private Maquinas maquina;
        public DetallesPrensaPagina(Maquinas maq)
        {
            InitializeComponent();
            this.DataContext = this;
            this.maquina = maq;
            CargarInformacion();
            this.maquina.OnColaTrabajoActualizada += Maquina_OnColaTrabajoActualizada;
            this.maquina.OnParesConsumidos += Maquina_OnParesConsumidos;
        }

        private void Maquina_OnParesConsumidos(object sender, EventArgs e)
        {
            try
            {
                CargarInformacion();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void Maquina_OnColaTrabajoActualizada(object sender, EventArgs e)
        {
            try
            {
                CargarInformacion();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void CargarInformacion()
        {
            Informacion.Clear();
            Informacion.Add(new ClaveValor<string, string>("MÁQUINA", this.maquina.Nombre));
            Informacion.Add(new ClaveValor<string, string>("OF", this.maquina.CodigoOrden));
            Informacion.Add(new ClaveValor<string, string>("PENDIENTE", this.maquina.TrabajoEjecucion.ParesPendientes.ToString()));
            Informacion.Add(new ClaveValor<string, string>("CLIENTE", this.maquina.Cliente));
            Informacion.Add(new ClaveValor<string, string>("MODELO", this.maquina.Modelo));
            Informacion.Add(new ClaveValor<string, string>("UTILLAJE", this.maquina.Utillaje));
            Informacion.Add(new ClaveValor<string, string>("TALLA_MOLDE", this.maquina.TallaUtillaje));
            Informacion.Add(new ClaveValor<string, string>("TALLA_CAJA", this.maquina.TrabajoEjecucion.TallaEtiquetaFichada));
            Informacion.Add(new ClaveValor<string, string>("CAJA", string.Format("{0}/{1}",this.maquina.CantidadCajaRealizada,this.maquina.CantidadCaja)));
        }  

        private void Salir()
        {
            if (OnSalir != null)
            {
                OnSalir(this,null);
            }
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            Salir();
        }
    }
}
