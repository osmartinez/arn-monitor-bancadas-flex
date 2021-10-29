using Almacenamiento.Implementaciones;
using MonitorWPF.Paginas;
using MqttServicio;
using System;
using System.Collections.Generic;
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

namespace MonitorWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GuiConfiguracion guiConfig = new GuiConfiguracion();
        private DispatcherTimer timerLoading = new DispatcherTimer { Interval = new TimeSpan(0, 0, 5), };

        public MainWindow()
        {
            InitializeComponent();

            if (!System.Security.Principal.WindowsIdentity.GetCurrent().Name.Contains("omartinez"))
            {
                //KillExplorer();
                this.Topmost = true;
                this.WindowStyle = WindowStyle.None;
            }

            this.Frame.Navigate(new BienvenidaAplicacionPaginaPrincipal());
            timerLoading.Tick += (s, e) => { this.timerLoading.Stop(); CargarAplicacion(); };
            timerLoading.Start();

            this.PreviewKeyUp += MainWindow_PreviewKeyUp;
        }

        private void LimpiarPaginas()
        {
            while (Frame.NavigationService.CanGoBack)
            {
                try
                {
                    Frame.NavigationService.RemoveBackEntry();
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        private void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F1)
            {
                LimpiarPaginas();
                ConfiguracionPaginaPrincipal2 cfg = new ConfiguracionPaginaPrincipal2();
                cfg.OnConfiguracionTerminada += (s, ev) => {
                    LimpiarPaginas();
                    this.Frame.Navigate(new BienvenidaAplicacionPaginaPrincipal());
                    timerLoading.Start();
                };
                Frame.Navigate(cfg);
            }
        }

        private void CargarAplicacion()
        {
            string modo = guiConfig.ObtenerModo();
            if (modo == "pegado")
            {
                PegadoPaginaPrincipal ppp = new PegadoPaginaPrincipal();
                ppp.OnAbrirConfiguracionUsuario += (s, e) => {
                    var cup = new ConfiguracionUsuarioPrincipal();
                    cup.OnCerrar += (s2, e2) => { LimpiarPaginas();CargarAplicacion(); };
                    Frame.Navigate(cup);
                };
                Frame.Navigate(ppp);
            }
            else
            {
                MoldeadoPaginaPrincipal mpp = new MoldeadoPaginaPrincipal();
                mpp.OnAbrirConfiguracionUsuario += (s, e) =>
                {
                    var cup = new ConfiguracionUsuarioPrincipal();
                    cup.OnCerrar += (s2, e2) => { LimpiarPaginas(); CargarAplicacion(); };
                    Frame.Navigate(cup);
                };
                Frame.Navigate(mpp);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ClienteMqtt.Cerrar();
               // Store.TCPCliente.Desconectar();
               // Store.TCPServidor.Desconectar();
            }
            catch(Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //ClienteMqtt.Iniciar();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }
    }
}
