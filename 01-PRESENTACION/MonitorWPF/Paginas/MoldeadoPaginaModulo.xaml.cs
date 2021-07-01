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
    /// Lógica de interacción para MoldeadoPaginaModulo.xaml
    /// </summary>
    public partial class MoldeadoPaginaModulo : Page
    {
        private IGuiOperario guiOperario = new GuiOperario();

        public event EventHandler<OperarioSaleEventArgs> OnOperarioSale;

        public Operarios Operario { get; set; } = new Operarios { Nombre = "- SIN OPERARIO -" };
        public Pantalla Pantalla { get; set; } = null;
        public List<Maquinas> Maquinas { get; set; } = new List<Maquinas>();
        public MoldeadoPaginaModulo(Operarios op, Pantalla p)
        {
            InitializeComponent();

            this.DataContext = this;
            this.BtLogout.OnPulsado += (s, e) =>
            {
                OperarioSale();
            };

            this.Operario = op;
            this.Pantalla = p;
            GridMaquinas.ColumnDefinitions.Add(new ColumnDefinition());
            GridMaquinas.ColumnDefinitions.Add(new ColumnDefinition());

            this.Maquinas = guiOperario.ObtenerMisMaquinas(p).OrderBy(x=>x.PosicionLayout).ToList();

            for (int i = 0; i < Maquinas.Count / 2; i++)
            {
                GridMaquinas.RowDefinitions.Add(new RowDefinition());
            }

            for (double i = 0, j = 0 ; i < Maquinas.Count; i++,j+=0.5)
            {
                Maquinas maquina = this.Maquinas[(int)i];

                PrensaGenericaControl pgc = new PrensaGenericaControl(maquina, op);

                Grid.SetColumn(pgc, i % 2 == 0?1:0);
                Grid.SetRow(pgc, (int)(j));

                GridMaquinas.Children.Add(pgc);
            }

            ControlOperario.SetOperario(op);
            ControlVueltas.SetMaquinas(this.Maquinas, op);

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
