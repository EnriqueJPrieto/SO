using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket server;
        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

        private void LOGIN_Click(object sender, EventArgs e)//iniciar sesion 
        {
            IPAddress direc = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;

                

                string login = "0" + "-" + USERNAME.Text + "-" + PASSWORD.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(login);

                server.Send(mensaje);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                login = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                if (login == "0-0")
                {
                    MessageBox.Show("Has accedido a la cuenta");
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    NAME.Visible = true;
                    DATE.Visible = true;
                    QUERY1.Visible = true;
                    QUERY2.Visible = true;
                    QUERY3.Visible = true;
                }
                MessageBox.Show("mensaje de log in");
                if (login == "Error")
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }

            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return; 
            }
        }

        private void SIGNIN_Click(object sender, EventArgs e)//registrarse
        {

            IPAddress direc = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;

                string SIGNIN = "4" + "-" + USERNAME.Text + "-" + PASSWORD.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(SIGNIN);

                server.Send(mensaje);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                SIGNIN = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                if (SIGNIN == "4-0")
                {
                    MessageBox.Show("Registrado correctamente");
                }
                MessageBox.Show("mensaje de sign in");
                if (SIGNIN == "ERROR")
                {
                    MessageBox.Show("Ha habido un problema con la creación de cuenta");
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void QUERY1_Click(object sender, EventArgs e)//jugadores de la partida mas larga
        {
            IPAddress direc = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;

                string QUERY1 = "1" + "-" + DATE.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(QUERY1);

                server.Send(mensaje);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                QUERY1 = Encoding.ASCII.GetString(msg2).Split('\0')[0];//2 o 4 nombres separados por -
                if (QUERY1 != "Error") 
                {
                    int i = 1;
                    string jugadores = "Los jugadores que jugaron la partida mas larga son : " + QUERY1.Split('-')[i];
                    i++;
                    while (QUERY1.Split('-')[i] != null)
                    {
                        jugadores = jugadores + " ," + QUERY1.Split('-')[i];
                        i++;
                    }
                    MessageBox.Show(jugadores);
                }
                if (QUERY1 == "Error")
                {
                    MessageBox.Show("No hay datos coincidentes");
                }


            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void QUERY2_Click(object sender, EventArgs e)// nombre del jugador con mas partidas 
        {
            IPAddress direc = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;

                string QUERY2 = "2" + "-" + DATE.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(QUERY2);

                server.Send(mensaje);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                QUERY2 = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string mensaje2 = QUERY2.Split('-')[1];
                if (QUERY2 != "Error")
                {
                    MessageBox.Show("El jugador " + mensaje2 + "fue el jugador con mas partidas el dia " + DATE.Text);
                }
                if (QUERY2 == "Error")
                {
                    MessageBox.Show("No hay datos coincidentes");
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void QUERY3_Click(object sender, EventArgs e)//winratio de un jugador un dia
        {
            IPAddress direc = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;

                string QUERY3 = "3" + "-" + NAME.Text + "-" + DATE.Text;//probar con 03/10/2022
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(QUERY3);

                server.Send(mensaje);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                QUERY3 = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                if (QUERY3 != "Error")
                {
                    int Wins = Convert.ToInt32(QUERY3.Split('-')[1]);
                    int Played = Convert.ToInt32(QUERY3.Split('-')[2]);
                    int wr = Wins / Played;
                    MessageBox.Show("El jugador " + NAME.Text + "tuvo un winratio de " + wr + "el dia " + DATE.Text);
                }
                if (QUERY3 == "Error")
                {
                    MessageBox.Show("No hay datos coincidentes");
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void DESCONECTAR_Click(object sender, EventArgs e)//desconectar
        {
            IPAddress direc = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;

                string CERRAR = "5" + "-";
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(CERRAR);

                server.Send(mensaje);
                server.Shutdown(SocketShutdown.Both);
                this.BackColor = Color.Gray;
                server.Close();
                MessageBox.Show("DESCONECTADO DEL SERVIDOR");
            }
            catch (SocketException)
            {
                MessageBox.Show("DESCONECTADO DEL SERVIDOR");
                return;
            }
        }
    }    
}
