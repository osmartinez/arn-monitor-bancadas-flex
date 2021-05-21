using Entidades.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Eventos
{
    public class MqttMensajeRecibidoEventArgs:EventArgs
    {
        public string NombreTopic { get; set; }
        public string Cuerpo { get; set; }
        public Topic Topic { get; set; }

        public MqttMensajeRecibidoEventArgs(string nombreTopic, string cuerpo, Topic topic)
        {
            NombreTopic = nombreTopic;
            Cuerpo = cuerpo;
            Topic = topic;
        }
    }
}
