using Almacenamiento.Implementaciones;
using Entidades.Util;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MqttServicio
{
    public static class ClienteMqtt
    {
        public static event EventHandler<EventArgs> OnConectado;
        private static List<Topic> Topics { get; set; } = new List<Topic>();
        private static IManagedMqttClient client;
        private static bool cierreForzado = false;
        private static bool iniciando = false;
        public static bool Conectado { get; set; }

        public static string clientId { get; private set; } = string.Format("{0}-{1}", NetworkUtils.GetLocalIPAddress(), Guid.NewGuid().ToString());
        public static string clientIp
        {
            get
            {
                return clientId.Split('-')[0];
            }
        }
        private static string ipAddress = "192.168.0.104";

        static ClienteMqtt()
        {
            Iniciar();
        }

        private  static void Iniciado()
        {
            if (OnConectado != null)
            {
                OnConectado(null, null);
            }
        }
        private static void Iniciar()
        {

                try
                {
                    //clientId = string.Format("{0}-{1}", NetworkUtils.GetLocalIPAddress(), Guid.NewGuid().ToString());
                    MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                        .WithClientId(clientId)
                                        .WithTcpServer(ipAddress, 1883);

                    // Create client options objects
                    ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                                            .WithAutoReconnectDelay(TimeSpan.FromSeconds(10))
                                            .WithClientOptions(builder.Build())
                                            .Build();

                    // Creates the client object
                    client= new MqttFactory().CreateManagedMqttClient();

                    // Set up handlers
                    client.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
                    client.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
                    client.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);
                    client.ApplicationMessageProcessedHandler = new ApplicationMessageProcessedHandlerDelegate(OnMensajeProcesado);
                    client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnMensajeRecibido);

                    client.StartAsync(options).GetAwaiter().GetResult();


                }
                catch (Exception ex)
                {

                }


            



            /*client = new MqttClient(ipAddress);
            client.Connect(clientId, "", "", true, 10);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived; ;
            client.ConnectionClosed += Client_ConnectionClosed;*/
        }


        public static void OnMensajeRecibido(MqttApplicationMessageReceivedEventArgs obj)
        {
            for (int i = 0; i < Topics.Count; i++)
            {
                var topic = Topics[i];
                if (topic.Nombre == obj.ApplicationMessage.Topic)
                {
                    topic.MensajeRecibido(System.Text.Encoding.UTF8.GetString(obj.ApplicationMessage.Payload));
                }
            }
        }

        public static void OnMensajeProcesado(ApplicationMessageProcessedEventArgs obj)
        {
            new Log().Escribir("hola");
        }

        public static void OnConnected(MqttClientConnectedEventArgs obj)
        {
            Conectado = true;
            Iniciado();
            SuscribirTodo();
        }

        public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
        {
            Conectado = false;
        }

        public async static Task OnDisconnected(MqttClientDisconnectedEventArgs obj)
        {
            Conectado = false;
        }

        /// <summary>
        /// Trata de suscribir a un topic que recibe como parámetro
        /// </summary>
        /// <param name="topic"></param>
        public static void Suscribir(Topic topic)
        {
            try
            {
                client.SubscribeAsync(topic.Nombre, (MQTTnet.Protocol.MqttQualityOfServiceLevel)topic.QOS);
                //client.Subscribe(new string[] { topic.Nombre }, new byte[] { topic.QOS });

                //Desuscribir(topic);
                if (Topics.FirstOrDefault(x=>x.Nombre == topic.Nombre && x.IdHandler == topic.IdHandler) !=null)
                { 

                }
                else
                {
                    Topics.Add(topic);
                }

                
                
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private static void SuscribirTodo()
        {
            foreach (var topic in Topics)
            {
                Suscribir(topic);
            }
        }

        public static void Desuscribir(Topic topic)
        {
            try
            {
                //client.Unsubscribe(new string[] { topic.Nombre });
                Topics.Remove(topic);
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        public static void Cerrar()
        {
            try
            {
                cierreForzado = true;
                client.Dispose();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }

       
        private static void Client_ConnectionClosed(object sender, EventArgs e)
        {
            if (!cierreForzado)
            {
                Iniciar();
            }
            else
            {
                cierreForzado = false;
            }
        }


        public static void Publicar(string topic, string msg, int qos)
        {
            if (client != null && client.IsConnected)
            {
                var message = new MqttApplicationMessageBuilder()
                   .WithTopic(topic)
                   .WithPayload(System.Text.Encoding.UTF8.GetBytes(msg))
                   .WithExactlyOnceQoS()
                   .WithRetainFlag()
                   .Build();

                client.PublishAsync(message);
               // client.Publish();
            }
        }
    }
}
