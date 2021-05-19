using Almacenamiento.Implementaciones;
using Almacenamiento.Interfaces;
using Entidades.DB;
using Entidades.Eventos;
using Entidades.Local;
using Extensiones;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MonitorWPF.Paginas
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionPaginaPrincipal.xaml
    /// </summary>
    public partial class ConfiguracionPaginaPrincipal : Page, INotifyPropertyChanged
    {
        private IDaoPuesto daoPuesto = new DaoPuesto();
        private IGuiConfiguracion guiConfig = new GuiConfiguracion();

        private int numPantallas = 1;
        public event EventHandler<ConfiguracionTerminadaEventArgs> OnConfiguracionTerminada;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ConfiguracionGlobal> Configuraciones { get; set; } = new ObservableCollection<ConfiguracionGlobal> { ConfiguracionGlobal.Default };
        public List<string> Modos { get; set; } = new List<string> { "pegado", "moldeado" };
        public ObservableCollection<Maquinas> Maquinas { get; set; } = new ObservableCollection<Maquinas>();
        public ObservableCollection<Maquinas> MaquinasSeleccionadas
        {
            get
            {
                return new ObservableCollection<Maquinas>(PantallaSeleccionada.Maquinas);
            }
        }
        public Pantalla PantallaSeleccionada
        {
            get
            {
                try
                {
                    int indice = CmbPantallas.SelectedIndex;

                    return Configuraciones.First().ConfiguracionLayoutActiva.Pantallas[indice];
                }
                catch (Exception ex)
                {
                    new Log().Escribir(ex);
                    return new Pantalla { Maquinas = new List<Maquinas>() };
                }
            }
        }
        public ObservableCollection<Pantalla> Pantallas
        {
            get
            {
                return new ObservableCollection<Pantalla>(Configuraciones.First().ConfiguracionLayoutActiva.Pantallas);
            }
        }

        public int NumPantallas
        {
            get
            {
                return numPantallas;
            }
            set
            {
                this.numPantallas = value;
                this.ActualizarPantallas();
            }
        }

        public ConfiguracionPaginaPrincipal()
        {
            InitializeComponent();
            this.DataContext = this;
            try
            {
                this.Maquinas.AddRange(daoPuesto.ObtenerMaquinasEnSecciones(new List<string> { "120" }));
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        public void ConfiguracionTerminada()
        {
            if (OnConfiguracionTerminada != null)
            {
                OnConfiguracionTerminada(this, new ConfiguracionTerminadaEventArgs());
            }
        }

        private void BtSalir_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionTerminada();
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int indice = CmbConfig.SelectedIndex;
                var cfg = Configuraciones[indice];

                if (cfg.Modo == "- NUEVA -")
                {
                    cfg.Modo = Modos[CmbModos.SelectedIndex];
                    guiConfig.GuardarConfiguracion(cfg);
                }
            }
            catch(Exception ex)
            {
                new Log().Escribir(ex);
            }
           
            ConfiguracionTerminada();
        }
        public void Notifica(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ActualizarPantallas()
        {
            int indice = CmbConfig.SelectedIndex;
            var cfg = Configuraciones[indice];

            if (cfg.Modo == "- NUEVA -")
            {
                Guid id = Guid.NewGuid();
                cfg.Configuraciones.Clear();
                cfg.Configuraciones.Add(new ConfiguracionLayout
                {
                    Id = id,
                    Pantallas = new List<Pantalla>(),
                });
                cfg.IdConfiguracionActiva = id;
                for (int i = 0; i < numPantallas; i++)
                {
                    cfg.ConfiguracionLayoutActiva.Pantallas.Add(new Pantalla
                    {
                        Id = Guid.NewGuid(),
                        Maquinas = new List<Maquinas>(),
                    });
                }

                Notifica();
            }
        }

        private void BtAddMaquina_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int indice = CmbMaquinas.SelectedIndex;
                Maquinas maq = Maquinas[indice];
                PantallaSeleccionada.Maquinas.Add(maq);
                Notifica();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void CmbPantallas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Notifica();
        }
    }
}
