using Entidades.DTO;
using Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPService
{
    public static class AnalizadorMensajes
    {
        public static MensajeTCP ObtenerMensaje(string txt)
        {
            MensajeTCP result = null;
            string[] partes = txt.Split(';');
            TipoMensajeTCP tipo = TipoMensajeTCP.cambiarIpPda;
            switch (partes[0])
            {
                case "-1":
                    {
                        // ack
                        break;
                    }
                case "0":
                    {
                        tipo = TipoMensajeTCP.cambiarIpPda;
                        result = new MensajeTCPCambioIpPda {Tipo = tipo, Ip = partes[1] };
                        break;
                    }
                case "1":
                    {
                        tipo = TipoMensajeTCP.fichaje;
                        result = new MensajeTCPFichaje { Tipo = tipo, CodigoMaquina = partes[1].Trim(), CodigoBarquilla = partes[2].Trim() };
                        break;
                    }
                case "2":
                    {
                        tipo = TipoMensajeTCP.errorPrensa;
                        result = new MensajeTCPErrorPrensa { Tipo = tipo, NombrePrensa = partes[1].Trim() };
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return result;
        }
    }
}
