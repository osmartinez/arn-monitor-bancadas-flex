using Almacenamiento.Implementaciones;
using Almacenamiento.Interfaces;
using Entidades.DB;
using Entidades.DTO;
using Entidades.Enum;
using Entidades.Util;
using Horario;
using MqttServicio;
using Newtonsoft.Json;
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
using System.Windows.Threading;

namespace MonitorWPF.Controles
{
    /// <summary>
    /// Lógica de interacción para PrensaGenericaControl.xaml
    /// </summary>
    public partial class PrensaGenericaControl : UserControl, INotifyPropertyChanged
    {
        private IDaoPuesto daoPuesto = new DaoPuesto();
        private IDaoTarea daoTarea = new DaoTarea();

        public Maquinas Maquina { get; set; }
        private DispatcherTimer timerCalentamiento;
        private DispatcherTimer timerInactividad;
        private DispatcherTimer timerLimiteCaja;
        private bool inactiva = false;
        private Topic topicNormal;
        private Topic topicCalentar;
        private Topic topicAsociarTarea;


        public event PropertyChangedEventHandler PropertyChanged;

        public string NombrePrensa
        {
            get
            {
                return (Maquina == null) ? "" : (
                    string.Format("PRENSA {0} - {1}<{2}>",
                    Maquina.Nombre
                    .Replace("MOLDE ESPUMA ", "")
                    .Replace("MOLDE PEGADO ", ""), Maquina.Utillaje, Maquina.TallaUtillaje)
                    ); ;
            }
        }

        public PrensaGenericaControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public PrensaGenericaControl(Maquinas maquina,Operarios op)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Maquina = maquina;
            this.Maquina.OperarioACargo = op;
            this.MinHeight = 30;
            Grid.SetRow(this, (int)maquina.Top);
            Grid.SetColumn(this, (int)maquina.Left);
            Maquina.OnParesConsumidos += Maquina_OnParesConsumidos;
            Maquina.OnInfoEjecucionActualizada += Maquina_OnInfoEjecucionActualizada;
            Maquina.OnModoCambiado += Maquina_OnModoCambiado;
            this.PreviewMouseUp += PrensaLayout_PreviewMouseUp;

            this.timerCalentamiento = new DispatcherTimer();
            this.timerCalentamiento.Interval = new TimeSpan(0, 0, 0, 0, 400);
            this.timerCalentamiento.Tick += TimerCalentamiento_Tick; ;
            this.timerInactividad = new DispatcherTimer();
            this.timerInactividad.Interval = new TimeSpan(0, 10, 0);
            this.timerInactividad.Tick += TimerInactividad_Tick;
            this.timerLimiteCaja = new DispatcherTimer();
            this.timerLimiteCaja.Interval = new TimeSpan(0, 0, 10);
            this.timerLimiteCaja.Tick += TimerLimiteCaja_Tick;

            this.timerLimiteCaja.Start();
            this.timerInactividad.Start();

            this.topicNormal = new Topic(string.Format("/{0}/plc/{1}/normal",maquina.Tipo,maquina.IpAutomata),(byte)2);
            this.topicNormal.OnMensajeRecibido += TopicNormal_OnMensajeRecibido;
            this.topicCalentar = new Topic(string.Format("/{0}/plc/{1}/calentar", maquina.Tipo, maquina.IpAutomata), (byte)1);
            this.topicCalentar.OnMensajeRecibido += TopicCalentar_OnMensajeRecibido;
            this.topicAsociarTarea = new Topic(string.Format("/{0}/plc/{1}/asociarTarea", maquina.Tipo, maquina.IpAutomata), (byte)1);
            this.topicAsociarTarea.OnMensajeRecibido += TopicAsociarTarea_OnMensajeRecibido;
            
            try
            {
                BackgroundWorker bwActualizarCola = new BackgroundWorker();
                List<MaquinasColasTrabajo> cola = new List<MaquinasColasTrabajo>();
                Maquinas maquinaDb = null;
                List<MaquinasRegistrosDatos> paquetesHistorico = new List<MaquinasRegistrosDatos>();
                DateTime ahora = DateTime.Now;
                Turno turno = HorarioTurnos.CalcularTurnoAFecha(ahora);
                DateTime fechaInicio;
                DateTime fechaFin;
                HorarioTurnos.CalcularHorarioTurno(turno, ahora, out fechaInicio, out fechaFin);
                
                maquina.Pulsos.Clear();

                bwActualizarCola.DoWork += (se, ev) =>
                {
                    maquinaDb = daoPuesto.ObtenerMaquinaConColaTrabajo(maquina.ID);
                    paquetesHistorico = daoTarea.ObtenerHistoricoParesOperario(op.Id, maquina.IpAutomata, maquina.Posicion, fechaInicio, fechaFin);
                };
                bwActualizarCola.RunWorkerCompleted += (se, ev) =>
                {
                    cola = maquinaDb.MaquinasColasTrabajo.ToList();
                    this.Maquina.IpAutomata = maquinaDb.IpAutomata;
                    this.Maquina.Posicion = maquinaDb.Posicion;
                    this.Maquina.PosicionGlobal = maquinaDb.PosicionGlobal;
                    maquina.AsignarColaTrabajo(cola);

                    ClienteMqtt.Suscribir(this.topicNormal);
                    ClienteMqtt.Suscribir(this.topicCalentar);
                    ClienteMqtt.Suscribir(this.topicAsociarTarea);

                    foreach(var paquete in paquetesHistorico.Where(x=>x.PiezaIntroducida))
                    {
                        Maquina.Pulsos.Add(new PulsoMaquina
                        {
                            CodigoEtiqueta = paquete.CodigoEtiqueta,
                            Control = daoTarea.BuscarControlGuardado(paquete.IdOperacion, Maquina.IdTipo??0),
                            Ciclo = paquete.Ciclo,
                            Fecha = paquete.FechaCreacion,
                            Pares = paquete.Pares,
                            PosicionGlobal = Maquina.PosicionGlobal ?? 0,
                            IdOperario = paquete.IdOperario,
                        });
                    }
                    Notifica("Maquina");
                };

                bwActualizarCola.RunWorkerAsync();

               
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }

        private void TopicCalentar_OnMensajeRecibido(object sender, Entidades.Eventos.MqttMensajeRecibidoEventArgs e)
        {
            try
            {
                ConsumoPrensa consumo = JsonConvert.DeserializeObject<ConsumoPrensa>(e.Cuerpo);

                if (consumo.Prensa == this.Maquina.Posicion)
                {
                    timerCalentamiento.Start();
                }
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void TopicAsociarTarea_OnMensajeRecibido(object sender, Entidades.Eventos.MqttMensajeRecibidoEventArgs e)
        {
            try
            {

            }catch(Exception ex)
            {
                new Log().Escribir(ex);

            }
        }

        private void TopicNormal_OnMensajeRecibido(object sender, Entidades.Eventos.MqttMensajeRecibidoEventArgs e)
        {
            timerCalentamiento.Stop();
            try
            {
                ConsumoPrensa consumo = JsonConvert.DeserializeObject<ConsumoPrensa>(e.Cuerpo);
                if (consumo == null)
                {
                    new Log().Escribir("consumo nulo");
                }

                if(consumo.Prensa != this.Maquina.Posicion)
                {
                    return;
                }

                if (this.Maquina.Modo != Entidades.Enum.ModoMaquina.Normal)
                {
                    this.Maquina.CambiarModo(Entidades.Enum.ModoMaquina.Normal);
                }

                this.Maquina.CargarInformacion(consumo);

                if (this.Maquina.TrabajoEjecucion != null)
                {
                    this.Maquina.Pulsos.Add(new PulsoMaquina
                    {
                        PiezaIntroducida = consumo.PiezaIntroducida==1,
                        CodigoEtiqueta = this.Maquina.TrabajoEjecucion.CodigoEtiquetaFichada,
                        Control = new OperacionesControles
                        {
                            IdTipoMaquina = 1,
                            TiempoBaseEjecucion = 0.01,
                            TiempoCambioBarquilla = 0.3,
                            TiempoInicio = 0,
                            TiempoUtillajeEjecucion = 0.1,
                        },
                        Ciclo=consumo.SgCiclo,
                        Fecha = DateTime.Now,
                        IdOperario = Maquina.OperarioACargo.Id,
                        PosicionGlobal = Maquina.PosicionGlobal??0,
                        Pares = (consumo.ParesUtillaje==0?1:consumo.ParesUtillaje) * consumo.NumMoldes,
                    }) ;

                    if(consumo.PiezaIntroducida == 1)
                    {
                        this.Maquina.InsertarPares(this.Maquina.TrabajoEjecucion, consumo.NumMoldes * (consumo.ParesUtillaje == 0 ? 1 : consumo.ParesUtillaje));
                    }

                    Notifica("Maquina");
                }
                else
                {
                    BackgroundWorker bw = new BackgroundWorker();
                    List<MaquinasColasTrabajo> colaTrabajo = new List<MaquinasColasTrabajo>();
                    bw.DoWork += (se, ev) =>
                    {
                        colaTrabajo = daoPuesto.ObtenerColaTrabajoMaquina(this.Maquina.ID);
                    };
                    bw.RunWorkerCompleted += (se, ev) =>
                    {
                        if (colaTrabajo.Count == 0)
                        {
                            ClienteMqtt.Publicar(string.Format("/{0}/fallo/{1}", Maquina.Tipo, this.Maquina.IpAutomata.PadLeft(3)),
                                string.Format("El automata {0} con maquina {1} no tiene cola de trabajo", Maquina.IpAutomata, Maquina.Posicion), 1);
                        }
                        Maquina.AsignarColaTrabajo(colaTrabajo);

                    };
                    bw.RunWorkerAsync();
                }
               

            }catch(Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void Maquina_OnParesConsumidos(object sender, EventArgs e)
        {
            this.Notifica();
            this.inactiva = false;
            this.timerInactividad.Stop(); this.timerInactividad.Start();
            if (this.Maquina.Modo != Entidades.Enum.ModoMaquina.Calentamiento)
            {
                PonerColorFrio();
            }
        }

        private void TimerLimiteCaja_Tick(object sender, EventArgs e)
        {
            if (this.Maquina.Modo == Entidades.Enum.ModoMaquina.Normal &&
                this.Maquina.CantidadCaja <= this.Maquina.CantidadCajaRealizada && !inactiva)
            {
                PonerColorError();
            }
        }

        private void TimerInactividad_Tick(object sender, EventArgs e)
        {
            if (this.Maquina.Modo != Entidades.Enum.ModoMaquina.Calentamiento)
            {
                PonerColorInactivo();
                inactiva = true;
            }
        }

        private void PrensaLayout_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //(((this.Parent as Grid).Parent as ScrollViewer).Parent as LayoutPrensas).PrensaSeleccionada = this;
        }

        private void TimerCalentamiento_Tick(object sender, EventArgs e)
        {
            /**
             * la idea es que si esta calentando:
             *  - si no ha alcanzado la tepmeratura -> naranja fijo
             *  - si ha alcanzado la temperatura -> naranja parpadeo
             */
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    if (this.Maquina.TemperaturaOK)
                    {
                        if (this.Border.Background == Brushes.Black)
                        {
                            PonerColorCaliente();
                        }
                        else
                        {
                            PonerColorFrio();
                        }
                    }
                    else
                    {
                        PonerColorCaliente();
                    }

                }));
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void PonerColorCaliente()
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    this.Border.Background = Brushes.Orange;

                }));
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }

        private void PonerColorFrio()
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    this.Border.Background = Brushes.Black;
                }));
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }

        private void PonerColorInactivo()
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    this.Border.Background = Brushes.Gray;
                }));
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private void PonerColorError()
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    this.Border.Background = Brushes.Red;
                }));
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }



        private void Maquina_OnModoCambiado(object sender, Entidades.Eventos.ModoMaquinaCambioEventArgs e)
        {
            if (e.Modo == Entidades.Enum.ModoMaquina.Calentamiento)
            {
                timerCalentamiento.Start();
            }
            else
            {
                timerCalentamiento.Stop();
                PonerColorFrio();
            }
        }

        public void Notifica(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Maquina_OnInfoEjecucionActualizada(object sender, EventArgs e)
        {
            this.Notifica();
            this.inactiva = false; ;
            this.timerInactividad.Stop(); this.timerInactividad.Start();
            if (this.Maquina.Modo != Entidades.Enum.ModoMaquina.Calentamiento)
            {
                PonerColorFrio();
            }
        }
    }
}
