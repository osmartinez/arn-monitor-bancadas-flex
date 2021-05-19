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

namespace MonitorWPF.Controles
{
    /// <summary>
    /// Lógica de interacción para BotonLogoutControl.xaml
    /// </summary>
    public partial class BotonLogoutControl : UserControl
    {
        public event EventHandler<EventArgs> OnPulsado;

        public BotonLogoutControl()
        {
            InitializeComponent();
        }

        public void Pulsado()
        {
            if (OnPulsado != null)
            {
                OnPulsado(this, new EventArgs());
            }
        }

        private void BtBorrarLogin_Click(object sender, RoutedEventArgs e)
        {
            Pulsado();
        }
    }
}
