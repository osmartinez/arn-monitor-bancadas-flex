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
    /// Lógica de interacción para PrimaTiempoRealControl.xaml
    /// </summary>
    public partial class PrimaTiempoRealControl : UserControl
    {
        public double Prima { get; set; } = 1.4;
        public PrimaTiempoRealControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
