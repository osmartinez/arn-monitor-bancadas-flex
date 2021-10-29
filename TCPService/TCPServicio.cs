using Almacenamiento.Implementaciones;
using Entidades.DTO;
using Entidades.Eventos;
using Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TCPService
{
    public class Cliente
    {
        public TcpClient Socket { get; private set; } = new TcpClient();
        public string IP { get; private set; } = "";
        public int Puerto { get; private set; } = 9875;
        public Cliente(int port, string iP)
        {

            this.Puerto = port;
            IP = iP;
            try
            {
                Socket = new TcpClient();
                Socket.Connect(this.IP, this.Puerto);
            }
            catch (Exception)
            {

                Console.WriteLine(" >> " + "Error al conectar con cliente " + this.IP);
            }


        }

        public void Desconectar()
        {
            try
            {
                if (Socket != null && Socket.Client != null)
                {
                    Socket.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(" >> " + "Error en la desconexion del cliente " + this.IP);
            }
        }
    }
    public class TCPCliente
    {
        List<Cliente> Clientes = new List<Cliente>();
        private int port = 9875;
        private Queue<MensajeTCPPrensa> mensajes = new Queue<MensajeTCPPrensa>();
        private DispatcherTimer timerEnvio = new DispatcherTimer { Interval = new TimeSpan(0, 0, 15) };

        public TCPCliente()
        {
            timerEnvio.Tick += TimerEnvio_Tick;
            timerEnvio.Start();
        }

        private void TimerEnvio_Tick(object sender, EventArgs e)
        {
            Enviar();
        }

        public void EnviarMensaje(MensajeTCPPrensa mensaje)
        {

            mensajes.Enqueue(mensaje);
        }

        private void Enviar()
        {
            try
            {
                if (mensajes.Count == 0) return;

                List<MensajeTCPPrensa> mensajesProcesar = new List<MensajeTCPPrensa>();
                while (mensajes.Count > 0)
                {
                    var mensaje = this.mensajes.Dequeue();

                    mensajesProcesar.RemoveAll(x => x.NombrePrensa == mensaje.NombrePrensa);

                    mensajesProcesar.Add(mensaje);
                }


                foreach (var cliente in Clientes)
                {

                    try
                    {
                        StreamWriter sw = new StreamWriter(cliente.Socket.GetStream());
                        foreach (var mensaje in mensajesProcesar)
                        {
                            sw.WriteLine(mensaje.ToTexto());
                        }
                        sw.Flush();

                        Console.WriteLine(" >> " + "Enviados " + mensajesProcesar.Count + " mensajes a servidor " + cliente.IP);


                    }
                    catch (Exception exInterna)
                    {
                        Console.WriteLine(" >> " + "Error local al enviar a servidor " + cliente.IP + ", desconectando...");
                        try
                        {
                            Desconectar(cliente);
                        }
                        catch (Exception)
                        {

                            Console.WriteLine(" >> " + "Error local al desconectar de servidor " + cliente.IP);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" >> " + "Error global en envío a servidores");
            }
        }

        public void Desconectar(Cliente cli)
        {

            foreach (var cliente in this.Clientes.Where(x => x.IP == cli.IP))
            {
                try
                {
                    cliente.Desconectar();
                    Clientes.RemoveAll(x => x.IP == cli.IP);

                }
                catch (Exception)
                {
                    Console.WriteLine(" >> " + "Error desconectando de servidor " + cliente.IP);


                }
            }

        }
        public void Desconectar()
        {
            try
            {
                foreach (var cliente in this.Clientes)
                {
                    try
                    {
                        cliente.Desconectar();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(" >> " + "Error desconectando de servidor " + cliente.IP);


                    }
                }

                this.Clientes.Clear();
            }
            catch (Exception)
            {


            }
        }
        public void Conectar(string ip)
        {
            try
            {
                foreach (var cliente in Clientes.Where(x => x.IP == ip))
                {
                    cliente.Desconectar();
                }

                Clientes.RemoveAll(x => x.IP == ip);

                Clientes.Add(new Cliente(port, ip));

            }
            catch (Exception ex)
            {
                Console.WriteLine(" >> " + "Conectado a servidor " + ip);



            }
        }

    }
    public class TCPServicio
    {
        public bool Working { get; set; } = true;
        private TcpListener serverSocket = new TcpListener(9876);
        private TcpClient clientSocket = default(TcpClient);

        public event EventHandler<MensajeTCPRecibidoEventArgs> OnMensajeRecibido;

        public TCPServicio()
        {
            try
            {
                serverSocket.Start();
                Console.WriteLine(" << " + "Server Started");
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }

        public void Desconectar()
        {
            try
            {
                Working = false;
                serverSocket.Stop();
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" << " + "Error al desconectar servidor escucha");

            }
        }

        public void Run()
        {
            try
            {
                while (Working)
                {
                    clientSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine(" << " + "Client started!");
                    handleClinet client = new handleClinet();
                    client.startClient(clientSocket, MensajeRecibido);
                }

                clientSocket.Close();
                serverSocket.Stop();
                Console.WriteLine(" << " + "exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                new Log().Escribir(ex);
            }

        }

        private void MensajeRecibido(TcpClient cli, MensajeTCPRecibidoEventArgs ev)
        {
            if (OnMensajeRecibido != null)
            {
                OnMensajeRecibido(cli, ev);
            }
        }

    }


    //Class to handle each client request separatly
    public class handleClinet
    {
        TcpClient clientSocket;
        Action<TcpClient, MensajeTCPRecibidoEventArgs> publisher;
        private bool working = true;
        public void startClient(TcpClient inClientSocket, Action<TcpClient, MensajeTCPRecibidoEventArgs> publisher)
        {
            this.publisher = publisher;
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        private void doChat()
        {
            while ((working))
            {
                try
                {
                    if (clientSocket.Connected)
                    {
                        NetworkStream networkStream = clientSocket.GetStream();

                        using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                publisher(this.clientSocket, new MensajeTCPRecibidoEventArgs(line));
                                Console.WriteLine(" << " + "From client " + line + " (" + (clientSocket.Client.RemoteEndPoint as IPEndPoint).Address);
                                //writer.WriteLine("ok;" + line);
                                //writer.Flush();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    working = false;

                    try { clientSocket.Close(); } catch (Exception ex2) { }

                }
            }

        }


    }
}

