using Almacenamiento.Implementaciones;
using Entidades.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace MqttServicio
{
    public static class ClienteMqtt
    {
        private static List<Topic> Topics { get; set; } = new List<Topic>();
        private static MqttClient client;
        private static bool cierreForzado = false;
        private static bool iniciando = false;
        private static string clientId = Guid.NewGuid() + "";
        private static string ipAddress = "192.168.0.104";

        static ClienteMqtt()
        {
            Iniciar();
        }
        private static void Iniciar()
        {
            client = new MqttClient(ipAddress);
            clientId = string.Format("{0}-{1}", "arn-monitor-flex", Guid.NewGuid().ToString());
            client.Connect(clientId, "", "", true, 10);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived; ;
            client.ConnectionClosed += Client_ConnectionClosed;
        }

        /// <summary>
        /// Trata de suscribir a un topic que recibe como parámetro
        /// </summary>
        /// <param name="topic"></param>
        public static void Suscribir(Topic topic)
        {
            try
            {
                Desuscribir(topic);
                client.Subscribe(new string[] { topic.Nombre }, new byte[] { topic.QOS });
                Topics.Add(topic);
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
                client.Unsubscribe(new string[] { topic.Nombre });
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
                client.Disconnect();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }

        private static async Task TryReconnectAsync(CancellationToken cancellationToken)
        {
            if (!cierreForzado)
            {
                var connected = client.IsConnected;
                while (!connected && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        client.Connect(clientId);
                    }
                    catch
                    {
                        new Log().Escribir(string.Format("No connection to...{0}", ipAddress));
                    }
                    connected = client.IsConnected;
                    if (connected)
                    {
                        SuscribirTodo();
                    }
                    await Task.Delay(10000, cancellationToken);
                }
            }
          
        }
        private static void Client_ConnectionClosed(object sender, EventArgs e)
        {
            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                Task T = Task.Run(() => TryReconnectAsync(source.Token));
                T.Wait();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }
        }

        private static void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            foreach (Topic topic in Topics)
            {
                if (topic.Nombre == e.Topic)
                {
                    topic.MensajeRecibido(System.Text.Encoding.UTF8.GetString(e.Message));
                }
            }
        }

        public static void Publicar(string topic, string msg, int qos)
        {
            if (client != null && client.IsConnected)
            {
                client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(msg), (byte)qos, false);
            }
        }
    }
}
