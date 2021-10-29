using Almacenamiento.Implementaciones;
using MqttServicio;
using QRCoder;
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
    /// Lógica de interacción para QRLocal.xaml
    /// </summary>
    public partial class QRLocal : UserControl
    {

        private string clienteIp = "";
        public QRLocal()
        {
            InitializeComponent();
            clienteIp = ClienteMqtt.clientIp;
            ClienteMqtt.OnConectado += ClienteMqtt_OnConectado;
            GenerarQR();
        }

        private void ClienteMqtt_OnConectado(object sender, EventArgs e)
        {
            try
            {
                clienteIp = ClienteMqtt.clientIp;
                GenerarQR();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void GenerarQR()
        {
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(clienteIp.PadRight(15, 'x'), QRCodeGenerator.ECCLevel.H);
                XamlQRCode qrCode = new XamlQRCode(qrCodeData);
                DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20);
                qrImage.Source = qrCodeAsXaml;
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }
    }
}
