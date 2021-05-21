﻿using Almacenamiento.Interfaces;
using Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class Log : ILog
    {
        private void EscribirConEstilo(string msg)
        {
            FicheroLog.EscribirMensaje(string.Format("[{0} {1}] \t{2}\n", DateTime.Now.ToShortDateString(),DateTime.Now.ToShortTimeString(), msg));
        }
        public void Escribir(string msg)
        {
            this.EscribirConEstilo(msg);
        }

        public void Escribir(Exception ex)
        {
            this.EscribirConEstilo(ex.Message);
        }

        public string Leer()
        {
            return FicheroLog.LeerTodo();
        }
    }
}
