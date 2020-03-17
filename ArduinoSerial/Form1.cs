using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace ArduinoSerial
{
    public partial class Form1 : Form
    {
        public MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=compServer;database=arduino");
        SerialPort myPort;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            myPort.Write("U");
            lblValor.Text = myPort.ReadLine();
            myPort.Write("T");
            lbltemp.Text = myPort.ReadLine();
            /*if (Convert.ToInt32(lblValor.Text) > 5)
                lblValor.ForeColor = Color.Red;
            else
                lblValor.ForeColor = Color.Blue;*/
            // myPort.Write("B");
            // lbltemp.Text = myPort.ReadLine();

        }

        void salvaBanco()
        {
            MySqlCommand cmd = new MySqlCommand("insere", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("valor", lblValor.Text);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Salvo com sucesso";

            }
            catch (MySqlException erro)
            {
                lblmsg.Text = "erro:" + erro;
            }
            conexao.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            salvaBanco();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            myPort.Close();
        }

        private void lblValor_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            salvaBanco();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            //comboBox1.SelectedIndex = 0;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            
            String porta = comboBox1.SelectedItem.ToString();
            myPort = new SerialPort(porta, 9600);
            myPort.Open();
            timer1.Enabled = true;
            timer1.Start();
            btnOpen.Enabled = false;
            timer2.Enabled = true;
            timer2.Start();
        }
    }
}
