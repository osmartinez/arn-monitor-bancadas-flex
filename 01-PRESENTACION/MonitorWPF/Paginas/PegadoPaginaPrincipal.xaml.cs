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
    /// Lógica de interacción para PegadoPaginaPrincipal.xaml
    /// </summary>
    public partial class PegadoPaginaPrincipal : Page
    {
        private IGuiConfiguracion guiConfig = new GuiConfiguracion();

        public PegadoPaginaPrincipal()
        {
            InitializeComponent();

            var pantallas = guiConfig.ObtenerPantallas();

            for (int i = 0; i < pantallas.Count; i++)
            {
                this.Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Frame f = new Frame();
                f.Margin = new Thickness(1);
                Grid.SetColumn(f, i);
                this.Grid.Children.Add(f);
                LoginPaginaPrincipal lpp = new LoginPaginaPrincipal(pantallas[i]);
                lpp.OnOperarioEntra += (s, e) => {
                    PegadoPaginaModulo ppm = new PegadoPaginaModulo(e.Operario);
                    ppm.OnOperarioSale += (s2, e2) => {
                        f.NavigationService.GoBack();
                    };
                    f.NavigationService.Navigate(ppm);
                };

                f.NavigationService.Navigate(lpp);
            }
        }
    }
}
