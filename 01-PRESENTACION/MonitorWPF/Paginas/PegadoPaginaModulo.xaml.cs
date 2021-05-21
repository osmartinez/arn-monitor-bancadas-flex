using Almacenamiento.Implementaciones;
using Almacenamiento.Interfaces;
using Entidades.DB;
using Entidades.Eventos;
using Entidades.Local;
using MonitorWPF.Controles;
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
    /// Lógica de interacción para PegadoPaginaModulo.xaml
    /// </summary>
    public partial class PegadoPaginaModulo : Page
    {
        private IGuiOperario guiOperario = new GuiOperario();

        public event EventHandler<OperarioSaleEventArgs> OnOperarioSale;

        public Operarios Operario { get; set; } = new Operarios { Nombre = "- SIN OPERARIO -" };
        public Pantalla Pantalla { get; set; } = null;
        public List<Maquinas> Maquinas { get; set; } = new List<Maquinas>();
       
        public PegadoPaginaModulo(Operarios op,Pantalla p)
        {
            InitializeComponent();
  
            this.DataContext = this;
            this.BtLogout.OnPulsado += (s, e) => {
                OperarioSale();
            };

            this.Operario = op;
            this.Pantalla = p;

            this.Maquinas = guiOperario.ObtenerMisMaquinas(p);

            for(int i = 0;i<Maquinas.Count;i++)
            {
                GridMaquinas.ColumnDefinitions.Add(new ColumnDefinition());
                Maquinas maquina = this.Maquinas[i];
                PrensaGenericaControl pgc = new PrensaGenericaControl(maquina,op);
                Grid.SetColumn(pgc, i);
                GridMaquinas.Children.Add(pgc);
            }

            this.RitmoControl.SetMaquinas(this.Operario, this.Maquinas);
        }

        public void OperarioSale()
        {
            if (OnOperarioSale != null)
            {
                OnOperarioSale(this, new OperarioSaleEventArgs(this.Operario));
            }
        }
    }
}
