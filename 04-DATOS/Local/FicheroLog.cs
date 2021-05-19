using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local
{
    public static class FicheroLog
    {
        private static string ruta = Path.Combine(Environment.GetFolderPath(
Environment.SpecialFolder.ApplicationData), "log_monitor_flex.json");

        public static void EscribirMensaje(string msg)
        {
            File.AppendAllText(ruta, msg);
        }
        
        public static string LeerTodo()
        {
            return File.ReadAllText(ruta);
        }
    }
}
