using Almacenamiento.Implementaciones;
using Entidades.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static void Iniciar()
        {
            if (!iniciando)
            {
                iniciando = true;
                client = new MqttClient("192.168.0.104");
                string clientId = string.Format("{0}-{1}", "arn-monitor-flex", Guid.NewGuid().ToString());
                client.Connect(clientId, "", "", true, 10);
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived; ;
                client.ConnectionClosed += Client_ConnectionClosed;
                iniciando = false;
            }

        }

        public static void Suscribir(Topic topic)
        {
            if (client != null && client.IsConnected)
            {
                client.Subscribe(new string[] { topic.Nombre }, new byte[] { topic.QOS });
                Topics.Add(topic);
            }
            else
            {
                Iniciar();
                client.Subscribe(new string[] { topic.Nombre }, new byte[] { topic.QOS });
                Topics.Add(topic);
            }
        }

        public static void Desuscribir(Topic topic)
        {
            if (client != null && client.IsConnected)
            {
                client.Unsubscribe(new string[] { topic.Nombre });
            }
        }

        public static void Cerrar()
        {
            cierreForzado = true;
            client.Disconnect();
        }

        private static void Client_ConnectionClosed(object sender, EventArgs e)
        {
            try
            {
                if (!cierreForzado)
                {
                    Iniciar();
                }
                else
                {
                    cierreForzado = false;
                }
            }catch(Exception ex)
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
