using Almacenamiento.Implementaciones;
using Almacenamiento.Interfaces;
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

namespace MonitorWPF.Paginas
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionUsuarioPrincipal.xaml
    /// </summary>
    public partial class ConfiguracionUsuarioPrincipal : Page
    {
        private IGuiConfiguracion guiConfig = new GuiConfiguracion();
        public event EventHandler<EventArgs> OnCerrar;

        public ConfiguracionUsuarioPrincipal()
        {
            InitializeComponent();

            int cols = 3;
            int rows = 2;
            for(int i = 0; i < cols; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for(int j = 0;j<rows; j++)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
            }

            var cfg = guiConfig.LeerConfiguracion();

            for(int i = 0; i < cfg.Configuraciones.Count; i++)
            {

                Button bt = new Button();
                bt.Content = cfg.Configuraciones[i].Alias;
                bt.Style = FindResource("BotonNumeroLogin") as Style;
                Grid.SetColumn(bt, i % cols);
                Grid.SetRow(bt, i<cols?0:1);
                Grid.Children.Add(bt);
                Guid id = cfg.Configuraciones[i].Id;
                bt.Click += (s, e) =>
                {
                    cfg.IdConfiguracionActiva = id;
                    guiConfig.GuardarConfiguracion(cfg);
                    Cerrar();
                };
            }
        }

        private void Cerrar()
        {
            if (OnCerrar != null)
            {
                OnCerrar(this, new EventArgs());
            }
        }
    }
}
