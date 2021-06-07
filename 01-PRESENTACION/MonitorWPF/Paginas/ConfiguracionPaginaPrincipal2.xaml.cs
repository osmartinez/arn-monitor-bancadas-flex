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
    /// Lógica de interacción para ConfiguracionPaginaPrincipal2.xaml
    /// </summary>
    public partial class ConfiguracionPaginaPrincipal2 : Page
    {
        private IDaoPuesto daoPuesto = new DaoPuesto();
        private IGuiConfiguracion guiConfig = new GuiConfiguracion();

        private int numPantallas = 1;
        public event EventHandler<ConfiguracionTerminadaEventArgs> OnConfiguracionTerminada;
        public event PropertyChangedEventHandler PropertyChanged;

        public ConfiguracionGlobal Configuracion { get; set; } = ConfiguracionGlobal.Default;
        public List<string> Modos { get; set; } = new List<string> { "pegado", "moldeado" };
        public string AliasLayout { get; set; } = "";
        public ObservableCollection<Maquinas> Maquinas { get; set; } = new ObservableCollection<Maquinas>();
        public Pantalla PantallaSeleccionada
        {
            get
            {
                try
                {
                    int indice = CmbPantallas.SelectedIndex;

                    return Configuracion.ConfiguracionLayoutActiva.Pantallas[indice];
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
                return new ObservableCollection<Pantalla>(Configuracion.ConfiguracionLayoutActiva.Pantallas);
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
                //this.ActualizarPantallas();
            }
        }

        public ConfiguracionPaginaPrincipal2()
        {
            InitializeComponent();
            this.DataContext = this;
            try
            {
                this.Maquinas.AddRange(daoPuesto.ObtenerMaquinasEnSecciones(new List<string> { "120", "110" }));
                Configuracion = guiConfig.LeerConfiguracion();
                if(Configuracion.Configuraciones.FirstOrDefault(x=>x.Alias == "- NUEVA -") == null)
                {
                    Configuracion.Configuraciones.Add(new ConfiguracionLayout());

                }
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

                var cfgLayout = Configuracion.Configuraciones[indice];

                if(cfgLayout.Alias!="- NUEVA -")
                {
                    Configuracion.Configuraciones.RemoveAll(x => x.Alias == "- NUEVA -");
                }

                if (cfgLayout.Alias != "- NUEVA - " && string.IsNullOrEmpty(AliasLayout))
                {

                }
                else if(cfgLayout.Alias == "- NUEVA -" && string.IsNullOrEmpty(AliasLayout))
                {
                    cfgLayout.Alias = indice.ToString();
                }
                else
                {
                    cfgLayout.Alias = AliasLayout;
                }


                Configuracion.IdConfiguracionActiva = cfgLayout.Id;


                if(Maquinas.Any(maquina=> maquina.PosicionLayout > 0 && maquina.IndicePantalla > 0))
                {
                    Configuracion.ConfiguracionLayoutActiva.Pantallas.Clear();

                    for (int i = 0; i < NumPantallas; i++)
                    {
                        Configuracion.ConfiguracionLayoutActiva.Pantallas.Add(new Pantalla());
                    }

                    foreach (var maquina in Maquinas)
                    {
                        if (maquina.PosicionLayout > 0 && maquina.IndicePantalla > 0)
                        {
                            Configuracion.ConfiguracionLayoutActiva.Pantallas[maquina.IndicePantalla - 1].Maquinas.Add(maquina);
                        }
                    }
                }

               

                Configuracion.Modo = Modos[CmbModos.SelectedIndex];
                guiConfig.GuardarConfiguracion(Configuracion);
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

            ConfiguracionTerminada();
        }
        public void Notifica(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CmbPantallas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Notifica();
        }
    }
}
