using Almacenamiento.Implementaciones;
using Entidades.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPService;

namespace MonitorWPF
{
    public static class Store
    {
        //public static TCPServicio TCPServidor { get; private set; } = new TCPServicio();
        //public static TCPCliente TCPCliente { get; private set; } = new TCPCliente();
        public static Operarios Operario { get; set; }

        static Store()
        {
            /*try
            {
                Thread serverHilo = new Thread(TCPServidor.Run);
                serverHilo.Start();
            }catch(Exception ex)
            {
                new Log().Escribir(ex);
            }*/

        }
    }
}
