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
using System.Threading;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        static class global
        {
            public static string invitados;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            
        }
        private void atenderServidor()
        {
            while (true)
            {
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string [] trozos = Encoding.ASCII.GetString(msg2).Split('-');
                int codigo = Convert.ToInt32(trozos[0]);
                int nConectados = Convert.ToInt32(trozos[1]);
                string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                switch (codigo)
                {
                    case 0:
                        if (mensaje == "0-0")
                        {
                            Invoke(new Action(() =>
                            {

                               MessageBox.Show("Has accedido a la cuenta");
                               this.BackColor = Color.Green;
                               lConectados.BackColor = Color.Green;
                               menuStrip1.BackColor = Color.Green;
                               label3.Visible = true;
                               label4.Visible = true;
                               label5.Visible = true;
                               NAME.Visible = true;
                               DATE.Visible = true;
                               QUERY1.Visible = true;
                               QUERY2.Visible = true;
                               QUERY3.Visible = true;
                               lConectados.Visible = true;
                               USERNAME.Enabled = false;
                               PASSWORD.Enabled = false;
                               SIGNIN.Enabled = false;
                               LOGIN.Enabled = false;
                            }));
                        }
                        if (mensaje == "0-Error")
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos");
                        }
                        break;
                    case 1:
                        if (mensaje != "1-Error")
                        {
                            int i = 1;
                            string jugadores = "Los jugadores que jugaron la partida mas larga son : " + mensaje.Split('-')[i];
                            i++;
                            while (i < mensaje.Split('-').Length)
                            {
                                jugadores = jugadores + " ," + mensaje.Split('-')[i];
                                i++;
                            }
                            MessageBox.Show(jugadores);
                        }
                        if (mensaje == "1-Error")
                        {
                            MessageBox.Show("No hay datos coincidentes");
                        }
                        break;
                    case 2:

                        if (mensaje != "2-Error")
                        {
                            string mensaje2 = mensaje.Split('-')[1];
                            MessageBox.Show("El jugador " + mensaje2 + " fue el jugador con mas partidas el dia " + DATE.Text);
                        }
                        if (mensaje == "2-Error")
                        {
                            MessageBox.Show("No hay datos coincidentes");
                        }
                        break;
                    case 3:
                        if (mensaje != "3-Error")
                        {
                            double Wins = Convert.ToDouble(mensaje.Split('-')[1]);
                            double Played = Convert.ToDouble(mensaje.Split('-')[2]);
                            double wr;
                            wr = (Wins / Played) * 100;
                            MessageBox.Show("El jugador " + NAME.Text + " tuvo un winratio de " + wr + "% el dia " + DATE.Text);
                        }
                        if (mensaje == "3-Error")
                        {
                            MessageBox.Show("No hay datos coincidentes");
                        }
                        break;
                    case 4:
                        if (mensaje == "4-0")
                        {
                            Invoke(new Action(() =>
                            {
                                    MessageBox.Show("Registrado correctamente");
                                MessageBox.Show("Has accedido a la cuenta");
                                this.BackColor = Color.Green;
                                lConectados.BackColor = Color.Green;
                                menuStrip1.BackColor = Color.Green;
                                label3.Visible = true;
                                label4.Visible = true;
                                label5.Visible = true;
                                NAME.Visible = true;
                                DATE.Visible = true;
                                QUERY1.Visible = true;
                                QUERY2.Visible = true;
                                QUERY3.Visible = true;
                                lConectados.Visible = true;
                                USERNAME.Enabled = false;
                                PASSWORD.Enabled = false;
                                SIGNIN.Enabled = false;
                                LOGIN.Enabled = false;
                            }));
                        }
                        if (mensaje == "4-ERROR")
                        {
                            MessageBox.Show("Ha habido un problema con la creación de cuenta");
                        }
                        break;
                    case 5:
                        
                        break;
                    case 6:
                        int id = 0;
                        lConectados.DropDownItems.Clear();
                        foreach (String items in dameListaConectados(mensaje, nConectados))
                        {
                            ToolStripMenuItem item = new ToolStripMenuItem(items);
                            item.Tag = id;
                            id++;
                            lConectados.DropDownItems.Add(item);
                            item.Click += new EventHandler(item_Click);
                        }
                        break;
                    case 7:
                        if (mensaje != "7-ERROR")
                        {
                            Convert.ToString(mensaje);
                            string invitador = mensaje.Split('-')[2];
                            // Create a new instance of the Form2 class
                            Form2 settingsForm = new Form2();

                            // Show the settings form
                            settingsForm.Show();
                            
                        }
                            


                        //Recivir una peticion que te permita aceptar o denegar partida y envie una peticion 7-Aceptada o 7-Rechazada
                        break;
                }
            }
        }
        private void LOGIN_Click(object sender, EventArgs e)//iniciar sesion 
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 5060);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                

                string login = "0" + "-" + USERNAME.Text + "-" + PASSWORD.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(login);

                server.Send(mensaje);

                ThreadStart ts = delegate { atenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return; 
            }
        }

        private void SIGNIN_Click(object sender, EventArgs e)//registrarse
        {

            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 5060);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;

                string SIGNIN1 = "4" + "-" + USERNAME.Text + "-" + PASSWORD.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(SIGNIN1);

                server.Send(mensaje);

                ThreadStart ts = delegate { atenderServidor(); };
                atender = new Thread(ts);
                atender.Start();

            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void QUERY1_Click(object sender, EventArgs e)//jugadores de la partida mas larga
        {
            try
            {
                this.BackColor = Color.Green;

                string QUERY1 = "1" + "-" + DATE.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(QUERY1);

                server.Send(mensaje);
            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void QUERY2_Click(object sender, EventArgs e)// nombre del jugador con mas partidas 
        {
            try
            {
                this.BackColor = Color.Green;

                string QUERY2 = "2" + "-" + DATE.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(QUERY2);

                server.Send(mensaje);  
            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void QUERY3_Click(object sender, EventArgs e)//winratio de un jugador un dia
        {
            try
            {
                this.BackColor = Color.Green;

                string QUERY3 = "3" + "-" + NAME.Text + "-" + DATE.Text;//probar con 03/10/2022
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(QUERY3);

                server.Send(mensaje);
            }
            catch (SocketException)
            {
                MessageBox.Show("NO HA SIDO POSIBLE CONECTARSE AL SERVIDOR");
                return;
            }
        }

        private void DESCONECTAR_Click(object sender, EventArgs e)//desconectar
        {
            try
            {
                this.BackColor = Color.Green;

                string CERRAR = "5" + "-" + USERNAME.Text;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(CERRAR);

                server.Send(mensaje);

                atender.Abort();

                server.Shutdown(SocketShutdown.Both);
                this.BackColor = Color.Gray;
                lConectados.BackColor = Color.Gray;
                menuStrip1.BackColor = Color.Gray;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                NAME.Visible = false;
                DATE.Visible = false;
                QUERY1.Visible = false;
                QUERY2.Visible = false;
                QUERY3.Visible = false;
                lConectados.Visible = false;
                USERNAME.Enabled = true;
                PASSWORD.Enabled = true;
                SIGNIN.Enabled = true;
                LOGIN.Enabled = true;
                server.Close();
                MessageBox.Show("DESCONECTADO DEL SERVIDOR");
            }
            catch (SocketException)
            {
                MessageBox.Show("DESCONECTADO DEL SERVIDOR");
                return;
            }
 
        }
        private List<String> dameListaConectados(string lista, int nConectados)
        {
            List<String> conectados = new List<String>();
            string [] lconectados = lista.Split('-');

            int numeroConectados = Convert.ToInt32(lconectados[1]);
            int i = 0;
            while (i < nConectados)
            {

                string nombre = lconectados[i+2];
                string[] Nnombre;
                Nnombre = nombre.Split('6');
                conectados.Add(Nnombre[0]);
                i++;
            }
            return conectados;
        }
        public void item_Click(object sender, EventArgs e)
        {
          
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            MessageBox.Show("Invitacion enviada a" + item.Text);
            //cada vez que cliques en una persona del desplegable añada a una persona a un listado de 4 maximo
            int i = 0;
            i++;
            string[]lista = new string[2];
            lista[i] = item.Text;
            if (lista.Length == 1)
            {
                global.invitados = lista[0];
                string invite = "6-1-"+global.invitados;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(invite);

                server.Send(mensaje);
            }
            if (lista.Length == 2)
            {
                global.invitados = lista[0] + "-" + lista[1];
                string invite = "6-2-" + global.invitados;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(invite);

                server.Send(mensaje);
            }
            if (lista.Length == 3)
            {
                global.invitados = lista[0] + "-" + lista[1] + "-" + lista[2];
                string invite = "6-3-" + global.invitados;
                byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(invite);

                server.Send(mensaje);
            }


           
        }

        private void Invitar_Click(object sender, EventArgs e)//no hace nada
        {
            string invite = "6" + "-" + global.invitados;
            byte[] mensaje = System.Text.Encoding.ASCII.GetBytes(invite);

            server.Send(mensaje);
            //enciar peticion 6-Nombre-Nombre-Nombre
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }    
}
