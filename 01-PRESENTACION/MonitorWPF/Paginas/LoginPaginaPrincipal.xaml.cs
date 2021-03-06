using Almacenamiento.Implementaciones;
using Almacenamiento.Interfaces;
using Entidades.DB;
using Entidades.Eventos;
using Entidades.Local;
using MonitorWPF.Ventanas;
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

namespace MonitorWPF.Paginas
{
    /// <summary>
    /// Lógica de interacción para LoginPaginaPrincipal.xaml
    /// </summary>
    public partial class LoginPaginaPrincipal : Page, INotifyPropertyChanged
    {
        private IDaoOperario daoOperario = new DaoOperario();
        private IGuiOperario guiOperario = new GuiOperario();
        private Pantalla pantalla;

        public event EventHandler<OperarioEntraEventArgs> OnOperarioEntra;

        public string CodigoOperario { get; set; } = "";

        public LoginPaginaPrincipal(Pantalla p)
        {
            InitializeComponent();
            this.pantalla = p;
            this.DataContext = this;
            this.Loaded += (s, e) => { btOk.IsEnabled = true; };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notificar(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OperarioEntra(Operarios op)
        {
            if (OnOperarioEntra != null)
            {
                OnOperarioEntra(this, new OperarioEntraEventArgs(op, this.pantalla));
            }
        }


        private void BtNumeroClick(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            string text = bt.Name.Replace("Bt", "");
            this.CodigoOperario += text;
            Notificar("CodigoOperario");
        }

        private void BtBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (this.CodigoOperario.Length > 0)
            {
                this.CodigoOperario = this.CodigoOperario.Substring(0, this.CodigoOperario.Length - 1);
                Notificar("CodigoOperario");
            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            Loader loader = new Loader();
            this.IsEnabled = false;
            loader.Show();

            try
            {
                if (string.IsNullOrEmpty(CodigoOperario))
                {
                    error = true;
                    loader.Close();
                    this.IsEnabled = true;

                }
                else
                {
                    Operarios operario = null;
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += (se, ev) =>
                    {
                        operario = daoOperario.BuscarPorCodigo(CodigoOperario);
                    };
                    bw.RunWorkerCompleted += (se, ev) =>
                    {
                        if (operario != null)
                        {
                            guiOperario.Entrar(this.pantalla, operario);
                            OperarioEntra(operario);
                        }
                        else
                        {
                            error = true;
                        }
                        loader.Close();
                        this.IsEnabled = true;
                        this.CodigoOperario = "";
                        Notificar("CodigoOperario");

                    };
                    bw.RunWorkerAsync();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                error = true;
                loader.Close();
                this.IsEnabled = true;
                this.CodigoOperario = "";
                Notificar("CodigoOperario");

            }


        }
    }
}
