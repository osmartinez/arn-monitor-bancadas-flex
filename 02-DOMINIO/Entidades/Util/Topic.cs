using Entidades.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Util
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string IdHandler { get; set; } = "default";
        public int IndiceIdTopic { get; set; }
        public int IndiceTipoBancada { get; set; }
        private event EventHandler<MqttMensajeRecibidoEventArgs> onMensajeRecibido;
        public event EventHandler<MqttMensajeRecibidoEventArgs> OnMensajeRecibido
        {
            add
            {
                if (onMensajeRecibido == null || !onMensajeRecibido.GetInvocationList().Contains(value))
                {
                    onMensajeRecibido += value;
                }
            }
            remove
            {
                onMensajeRecibido -= value;
            }
        }
        public byte QOS { get; set; }
        public Topic(string nombre)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            QOS = (byte)2;
        }
        public Topic(string nombre, byte qos)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            this.QOS = qos;
        }

        public Topic(string nombre, byte qos, string handler)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            this.QOS = qos;
            this.IdHandler = handler;
        }

        public void MensajeRecibido(string cuerpo)
        {
            if (onMensajeRecibido != null)
            {
                onMensajeRecibido(this, new MqttMensajeRecibidoEventArgs(this.Nombre, cuerpo, this));
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Topic topic &&
                   Nombre.Equals(topic.Nombre) && IdHandler.Equals(topic.IdHandler);
        }
    }
}
