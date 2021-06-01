using Entidades.DB;
using MonitorWPF.Paginas;
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
using System.Windows.Shapes;

namespace MonitorWPF.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VerDetallesPrensa.xaml
    /// </summary>
    public partial class VerDetallesPrensa : Window
    {
        public VerDetallesPrensa(Maquinas maquina)
        {
            InitializeComponent();
            var pagina = new DetallesPrensaPagina(maquina);
            pagina.OnSalir += Pagina_OnSalir;
            this.Frame.Navigate(pagina);
        }

        private void Pagina_OnSalir(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
