using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impresion
{
    public class ImprimirTicketTrabajo : Imprime
    {
        private List<MaquinasRegistrosDatos> pares = new List<MaquinasRegistrosDatos>();
        private Operarios operario;
        List<OperacionesControles> storeControles = new List<OperacionesControles>();
        public ImprimirTicketTrabajo(List<OperacionesControles> storeControles, Operarios operario, List<MaquinasRegistrosDatos> pares,  string printername = "POS-80C") : base(printername)//Microsoft Print To PDF  - EPSON-TICKETS
        {
            this.pares = pares;
            this.operario = operario;
            this.storeControles = storeControles;
        }

        public override void Imprimir()
        {
            PrintDocument doc = new PrintDocument();
            doc.DocumentName = string.Format("Ticket Hoja Trabajo");
            doc.PrintPage += Doc_PrintPage;
            doc.PrintController = new StandardPrintController();
            PrinterSettings ps = new PrinterSettings
            {
                PrinterName = base.PrinterName,
            };
            doc.PrinterSettings = ps;
            doc.Print();
            doc.Dispose();
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float saltoLinea = 10;
            this.Y = saltoLinea;

            var ahora = DateTime.Now;

            this.Text = ahora.ToShortDateString();
            this.DibujarBloque(e, base.Arial12, alineacion: System.Drawing.StringAlignment.Near, saltoLinea: false);

            this.Text = ahora.ToShortTimeString();
            this.DibujarBloque(e, base.Arial12, alineacion: System.Drawing.StringAlignment.Far);
            Y += saltoLinea;

            this.Text = this.operario.CodigoObrero;
            this.DibujarBloque(e, base.Arial12);

            this.Text = this.operario.Nombre + " " + this.operario.Apellidos;
            this.DibujarBloque(e, base.Arial12);
            Y += saltoLinea;

            this.Text = string.Format("{0} PARES", this.pares.Sum(x => x.Pares));
            this.DibujarBloque(e, base.Arial12negrita);
            Y += saltoLinea - 5;

            var groupPorMaquina = pares.OrderBy(x => x.FechaCreacion).GroupBy(x => x.IdMaquina);
            var vueltas = groupPorMaquina.Max(x => x.Count());

            int cambiosUtillaje = 0;
            foreach (var grupo in groupPorMaquina)
            {
                string anterior = grupo.First().OrdenesFabricacionOperacionesTallasCantidad.OrdenesFabricacionOperacionesTallas.OrdenesFabricacionOperaciones.CodUtillaje + " " + grupo.First().OrdenesFabricacionOperacionesTallasCantidad.OrdenesFabricacionOperacionesTallas.IdUtillajeTalla;
                foreach (var pulso in grupo)
                {
                    string actual = pulso.OrdenesFabricacionOperacionesTallasCantidad.OrdenesFabricacionOperacionesTallas.OrdenesFabricacionOperaciones.CodUtillaje + " " + pulso.OrdenesFabricacionOperacionesTallasCantidad.OrdenesFabricacionOperacionesTallas.IdUtillajeTalla;
                    if (actual != anterior)
                    {
                        cambiosUtillaje++;
                    }
                    anterior = actual;
                }
            }

            this.Text = string.Format("{0} VUELTAS", vueltas);
            this.DibujarBloque(e, base.Arial10, alineacion: System.Drawing.StringAlignment.Center, saltoLinea: false);
            Y += 14;

            this.Text = string.Format("{0} CAMBIOS DE MOLDE", cambiosUtillaje);
            this.DibujarBloque(e, base.Arial10, alineacion: System.Drawing.StringAlignment.Center, saltoLinea: false);
            Y += 16;


            // enumerar las operaciones junto a los pares realizados de cada una

            var agrupadasPorOperacion = pares.GroupBy(x => x.OrdenesFabricacionOperacionesTallasCantidad.OrdenesFabricacionOperacionesTallas.OrdenesFabricacionOperaciones.IdOperacionMaestra); // operacion maestra



            foreach (var grupo in agrupadasPorOperacion)
            {
                var control = storeControles.FirstOrDefault(x => x.IdOperacion == grupo.Key);
                if (control != null)
                {
                    this.Text = string.Format(" * {0}p. {1}", grupo.Sum(x => x.Pares).ToString().PadRight(4, ' '), control.Operaciones.Descripcion);
                }
                else
                {
                    this.Text = string.Format(" * {0}p. {1}", grupo.Sum(x => x.Pares).ToString().PadRight(4, ' '), "~DESCONOCIDO~");
                }
                this.DibujarBloque(e, base.Lucida9, alineacion: System.Drawing.StringAlignment.Near, saltoLinea: false);
                Y += 12;
            }
        }
    }

}
