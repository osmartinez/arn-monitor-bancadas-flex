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
        public int IndiceIdTopic { get; set; }
        public int IndiceTipoBancada { get; set; }
        public event EventHandler<MqttMensajeRecibidoEventArgs> OnMensajeRecibido;
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

        public void MensajeRecibido(string cuerpo)
        {
            if (OnMensajeRecibido != null)
            {
                OnMensajeRecibido(this, new MqttMensajeRecibidoEventArgs(this.Nombre, cuerpo, this));
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Topic topic &&
                   Id.Equals(topic.Id);
        }
    }
}
