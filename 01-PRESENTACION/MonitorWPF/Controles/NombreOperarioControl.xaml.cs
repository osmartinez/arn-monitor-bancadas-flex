using Entidades.DB;
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

namespace MonitorWPF.Controles
{
    /// <summary>
    /// Lógica de interacción para NombreOperarioControl.xaml
    /// </summary>
    public partial class NombreOperarioControl : UserControl,INotifyPropertyChanged
    {
        public Operarios Operario { get; private set; }
        public NombreOperarioControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notifica(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SetOperario (Operarios op)
        {
            Operario = op;
            Store.Operario = op;
            Notifica("Operario");
        }
    }
}
