using Almacenamiento.Implementaciones;
using Almacenamiento.Interfaces;
using Entidades.DB;
using Entidades.DTO;
using Entidades.Eventos;
using Fichajes;
using MqttServicio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;
using TCPService;

namespace MonitorWPF.Paginas
{
    /// <summary>
    /// Lógica de interacción para MoldeadoPaginaPrincipal.xaml
    /// </summary>
    public partial class MoldeadoPaginaPrincipal : Page
    {

        private IGuiConfiguracion guiConfig = new GuiConfiguracion();
        private IDaoTarea daoTarea = new DaoTarea();
        private IDaoPuesto daoPuesto = new DaoPuesto();
        private IDaoBarquilla daoBarquilla = new DaoBarquilla();
        private IDaoUtillajeTallaColeccion daoUtc = new DaoUtillajeTallaColeccion();

        private DispatcherTimer timerFocus = new DispatcherTimer { Interval = new TimeSpan(0, 0, 2) };
        private DispatcherTimer timerEventoFichaje = new DispatcherTimer { Interval = new TimeSpan(0, 0, 5) };

        private Queue<FichajeAsociacionEventArgs> colaEventosFichaje = new Queue<FichajeAsociacionEventArgs>();
        private List<MoldeadoPaginaModulo> paginasModulos = new List<MoldeadoPaginaModulo>();
        List<Frame> frames = new List<Frame>();

        private Dictionary<string, string> maquinasEmparejadas = new Dictionary<string, string>() {
            { "0200000007676", "0200000007690" },
            { "0200000007713","0200000007737"},
            { "0200000007751","0200000007775"},
            { "0200000007799","0200000007812"},
            { "0200000007805","0200000007782"},
            { "0200000007768","0200000007744"},
            { "0200000007720","0200000007706"},
            { "0200000007683","0200000007669"}, };

        public event EventHandler<EventArgs> OnAbrirConfiguracionUsuario;


        public MoldeadoPaginaPrincipal()
        {
            this.InitializeComponent();

            timerFocus.Tick += TimerFocus_Tick;
            timerEventoFichaje.Tick += TimerEventoFichaje_Tick;
            timerFocus.Start();
            timerEventoFichaje.Start();

            SuscribirTopicLecturaPDA();

            ClienteMqtt.OnConectado += ClienteMqtt_OnConectado;

            FichajeAgente.OnFichajeAsociacion += FichajeAgente_OnFichajeAsociacion;
            FichajeAgente.OnFichajeUbicacionUtillaje += FichajeAgente_OnFichajeUbicacionUtillaje;
            var pantallas = guiConfig.ObtenerPantallas();


            for (int i = 0; i < pantallas.Count; i++)
            {
                this.Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Frame f = new Frame();

                f.JournalOwnership = JournalOwnership.OwnsJournal;
                f.NavigationUIVisibility = NavigationUIVisibility.Hidden;

                f.Margin = new Thickness(1);
                Grid.SetColumn(f, i);
                this.Grid.Children.Add(f);
                frames.Add(f);
                int locali = i;

                LoginPaginaPrincipal lpp = new LoginPaginaPrincipal(pantallas[locali]);
                lpp.OnOperarioEntra += (s, e) =>
                        {

                            MoldeadoPaginaModulo mpm = new MoldeadoPaginaModulo(e.Operario, e.Pantalla);
                            paginasModulos.Add(mpm);
                            mpm.OnOperarioSale += (s2, e2) =>
                            {
                                frames[locali].GoBack();
                            };
                            frames[locali].Navigate(mpm);
                        };

                frames[locali].Navigate(lpp);
            }
        }

        private void SuscribirTopicLecturaPDA()
        {
            try
            {


                /*Store.TCPServidor.OnMensajeRecibido += (s, ev) =>
                {
                    MensajeTCP mensaje = AnalizadorMensajes.ObtenerMensaje(ev.Mensaje);
                    if(mensaje != null)
                    {
                        switch (mensaje.Tipo)
                        {
                            case Entidades.Enum.TipoMensajeTCP.cambiarIpPda:
                                {
                                    Store.TCPCliente.Conectar((mensaje as MensajeTCPCambioIpPda).Ip);
                                    break;
                                }

                            case Entidades.Enum.TipoMensajeTCP.fichaje:
                                {
                                    MensajeTCPFichaje mensajeFichaje = mensaje as MensajeTCPFichaje;
                                    FichajeAgente.EtiquetaFichada(mensajeFichaje.CodigoMaquina);
                                    FichajeAgente.EtiquetaFichada(mensajeFichaje.CodigoBarquilla);
                                    break;
                                }
                            default: { break; }
                        }
                    }
                    
                };*/

                Entidades.Util.Topic topicLectura = new Entidades.Util.Topic("/moldeado/pantalla/" + ClienteMqtt.clientIp + "/lectura", (byte)1);
                topicLectura.OnMensajeRecibido += (se, ev) =>
                {
                    /* LecturaPDA lectura = JsonConvert.DeserializeObject<LecturaPDA>(ev.Cuerpo);
                     FichajeAgente.EtiquetaFichada(lectura.CodigoMaquina);
                     FichajeAgente.EtiquetaFichada(lectura.CodigoBarquilla);*/
                    string[] partes = ev.Cuerpo.Split(';');
                    if(partes.Length == 3)
                    {
                        FichajeAgente.EtiquetaFichada(partes[1].Trim());
                        FichajeAgente.EtiquetaFichada(partes[2].Trim());
                        
                    }

                };

                Entidades.Util.Topic topicHojaProduccion = new Entidades.Util.Topic("/moldeado/pantalla/" + ClienteMqtt.clientIp + "/hojaProduccion", (byte)1);
                topicHojaProduccion.OnMensajeRecibido += (se, ev) =>
                {
                    if (Store.Operario != null)
                        ClienteMqtt.Publicar(string.Format("/operario/imprimir/moldeado/hojaProduccion"), "{\"IdOperario\": " + Store.Operario.Id + "}", 2);

                };
                ClienteMqtt.Suscribir(topicLectura);
                ClienteMqtt.Suscribir(topicHojaProduccion);

            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void ClienteMqtt_OnConectado(object sender, EventArgs e)
        {
            SuscribirTopicLecturaPDA();
        }

        private void FichajeAgente_OnFichajeUbicacionUtillaje(object sender, FichajeUbicacionUtillajeEventArgs e)
        {
            try
            {
                daoUtc.Ubicar(e.CodigoEtiqueta, e.CodigoUbicacion);
                MqttInformarUbicacion(new CambioUbicacion { CodigoEntidad = e.CodigoEtiqueta, CodigoUbicacion = e.CodigoUbicacion });
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void TimerEventoFichaje_Tick(object sender, EventArgs e)
        {
            FichajeAsociacionEventArgs evento = null;

            try
            {
                if (colaEventosFichaje.Count == 0) return;
                evento = colaEventosFichaje.Dequeue();
                if (evento != null)
                {

                    //asociar
                    Maquinas maquina = null;
                    foreach (var pantalla in this.paginasModulos)
                    {
                        foreach (var m in pantalla.Maquinas)
                        {
                            if (m.CodigoEtiqueta == evento.CodigoMaquina)
                            {
                                maquina = m;
                                break;
                            }
                        }
                    }
                    if (maquina != null)
                    {
                        maquina.FichajeMaquina(evento);
                        var infoBarquillaSeccion = daoTarea.BuscarInformacionBarquilla(evento.CodigoBarquilla, maquina.CodSeccion);
                        if (infoBarquillaSeccion.Any())
                        {
                            var idsOrden = infoBarquillaSeccion.Select(x => x.IdOrden);
                            var idsOrdenDistinto = idsOrden.Distinct();
                            if (idsOrden.Count() != idsOrdenDistinto.Count())
                            {
                                // multiOperacion
                            }
                            else
                            {
                                var idsTareas = infoBarquillaSeccion.Select(x => x.IdTarea.Value).Distinct().ToList();

                                evento.Control = daoTarea.BuscarControlGuardado(infoBarquillaSeccion.First().IdOperacion, maquina.IdTipo ?? 0);

                                // bd
                                BackgroundWorker bwActualizarCola = new BackgroundWorker();
                                List<MaquinasColasTrabajo> cola = new List<MaquinasColasTrabajo>();
                                bwActualizarCola.DoWork += (se, ev) =>
                                {
                                    cola = daoPuesto.ActualizarColaTrabajo(evento.CodigoBarquilla, idsTareas, infoBarquillaSeccion.First().Agrupacion ?? 0, maquina.ID, maquina.OperarioACargo.Id, infoBarquillaSeccion.Sum(x => x.Cantidad));
                                    Dictionary<int, int> idOrdenesOperaciones = new Dictionary<int, int>();
                                    foreach (var info in infoBarquillaSeccion)
                                    {
                                        idOrdenesOperaciones.Add(info.IdOrden, info.IdOperacion);
                                    }
                                    daoBarquilla.Ubicar(idOrdenesOperaciones, maquina.CodSeccion, evento.CodigoBarquilla, maquina.CodUbicacion);
                                };
                                bwActualizarCola.RunWorkerCompleted += (se, ev) =>
                                {
                                    maquina.AsignarColaTrabajo(cola);
                                };
                                bwActualizarCola.RunWorkerAsync();

                                // mqtt
                                MqttAsociarBarquilla(infoBarquillaSeccion, maquina);
                                MqttInformarUbicacion(new CambioUbicacion { CodigoEntidad = evento.CodigoBarquilla, CodigoUbicacion = maquina.CodUbicacion });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                colaEventosFichaje.Enqueue(evento);
                new Log().Escribir(ex);
            }
        }

        private void MqttInformarUbicacion(CambioUbicacion cambio)
        {
            try
            {
                ClienteMqtt.Publicar("/ubicacion", JsonConvert.SerializeObject(cambio), 1);
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }
        private void MqttAsociarBarquilla(List<SP_BarquillaBuscarInformacionEnSeccion_Result> prepaquete, Maquinas maquina, bool asociacion = true)
        {
            try
            {
                string nombreCliente = prepaquete.First().NOMBRECLI ?? "ARNEPLANT S.L.";
                nombreCliente = new Regex("[^A-Za-z0-9 ]").Replace(nombreCliente, " ");
                if (nombreCliente.Length > 25)
                {
                    nombreCliente = nombreCliente.Substring(0, 24);
                }

                string mensaje = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};",
                    maquina.Posicion
                    , prepaquete.First().IdTarea.ToString().PadLeft(10, '0')
                    , prepaquete.First().CantidadFabricar.ToString().PadLeft(10, '0')
                    , prepaquete.First().Codigo.PadLeft(13)
                    , prepaquete.First().CodUtillaje.PadLeft(25)
                    , prepaquete.First().IdUtillajeTalla.PadLeft(10)
                    , prepaquete.First().Talla.PadLeft(10)
                    , prepaquete.First().CodigoEtiqueta.PadLeft(13)
                    , prepaquete.First().IdOrden.ToString().PadLeft(10, '0')
                    , prepaquete.First().IdOperacion.ToString().PadLeft(10, '0')
                    , nombreCliente.PadLeft(25)
                    , prepaquete.First().CodigoArticulo.PadLeft(20)
                    , prepaquete.First().Productividad.ToString().PadLeft(3)
                    , maquina.OperarioACargo.Id.ToString().PadLeft(5));

                ClienteMqtt.Publicar(string.Format("/{0}/plc/{1}/asociarTarea", maquina.Tipo, maquina.IpAutomata.PadLeft(3)), mensaje, 1);


                maquina.CargarInformacion(new AsociacionTarea
                {
                    Cliente = prepaquete.First().NOMBRECLI,
                    CodigoArticulo = prepaquete.First().CodigoArticulo,
                    CodigoOrden = prepaquete.First().Codigo,
                    IdOperacion = prepaquete.First().IdOperacion,
                    IdOrden = prepaquete.First().IdOrden,
                    Utillaje = prepaquete.First().CodUtillaje,
                    TallaUtillaje = prepaquete.First().IdUtillajeTalla,
                    TallasArticulo = prepaquete.First().Tallas,
                    IdTarea = prepaquete.First().IdTarea ?? 0,
                    Pares = prepaquete.First().CantidadFabricar.HasValue ? (int)prepaquete.First().CantidadFabricar : 0,
                    CodigoEtiqueta = prepaquete.First().CodigoEtiqueta,
                    IdOperario = maquina.OperarioACargo.Id,
                    ParesUtillaje = (int)prepaquete.First().Productividad,
                    Prensa = maquina.Posicion,
                    CantidadCaja = prepaquete.Sum(x => x.Cantidad),
                });


            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }


        private void FichajeAgente_OnFichajeAsociacion(object sender, FichajeAsociacionEventArgs e)
        {
            try
            {
                this.colaEventosFichaje.Enqueue(e);
                if (maquinasEmparejadas.ContainsKey(e.CodigoMaquina))
                {
                    this.colaEventosFichaje.Enqueue(new FichajeAsociacionEventArgs(maquinasEmparejadas[e.CodigoMaquina], e.CodigoBarquilla));
                }
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void TimerFocus_Tick(object sender, EventArgs e)
        {
            TbCodigo.Focus();
            TbCodigo.CaretIndex = TbCodigo.Text.Length;
        }

        private void TbCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            timerFocus.Stop();
            timerFocus.Start();


        }

        private void TbCodigo_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string codigo = TbCodigo.Text;
                TbCodigo.Clear();
                FichajeAgente.EtiquetaFichada(codigo);
            }
        }
        private void AbrirConfiguracionUsuario()
        {
            if (OnAbrirConfiguracionUsuario != null)
            {
                OnAbrirConfiguracionUsuario(this, new EventArgs());
            }
        }
        private void BtConfigUsuario_Click(object sender, RoutedEventArgs e)
        {
            AbrirConfiguracionUsuario();
        }
    }
}
