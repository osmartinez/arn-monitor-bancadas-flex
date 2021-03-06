using Almacenamiento.Interfaces;
using Local;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class Log : ILog
    {
        private void EscribirConEstilo(string msg)
        {
            try
            {
                FicheroLog.EscribirMensaje(string.Format("[{0} {1}] \t{2}\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), msg));
            }
            catch (Exception) { }
        }
        public void Escribir(string msg)
        {
            this.EscribirConEstilo(msg);
        }

        public void Escribir(Exception ex)
        {
            
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);
            int line = frame.GetFileLineNumber();
            string filename = frame.GetFileName();
            this.EscribirConEstilo(string.Format("{0}@{1}\n{2}\n{3}",filename,line,ex.Message,ex.StackTrace));
        }


        public string Leer()
        {
            return FicheroLog.LeerTodo();
        }
    }
}
