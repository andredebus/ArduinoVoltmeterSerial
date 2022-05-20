using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Arduino_Serial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Btn3.Enabled = false;
            Btn3.Enabled = false;

        }

        SerialPort port;
        private void TextBox1_Enter(Object sender, System.EventArgs e)
        {
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }
        private void Btn1_Click(object sender, EventArgs e)
        {
            try
            {
                port = new SerialPort("COM3", 115200);
                port.DataReceived += new SerialDataReceivedEventHandler(ReceivedSerialHandler);
                port.Open();

                Btn1.Enabled = false;
                Btn3.Enabled = true;

                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                port.Close();
                
            }

        }
        private void ReceivedSerialHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);
            
            SerialPort sp = (SerialPort)sender;
            Invoke(
                (MethodInvoker)delegate
            {
                Thread.Sleep(500);
                string ZeitAktuell = DateTime.Now.ToString();
                string tmpSerial = sp.ReadExisting();
                string WertVolt = tmpSerial.Substring(0, 7);
                //textBox1.AppendText($"{timeNow}\t{tmpSerial.Replace(",","\t")}");
                textBox1.Text = $" {ZeitAktuell}\t{tmpSerial.Replace(",", " \t")}" + textBox1.Text;
                txb1.Text = WertVolt.Replace(".",",");
            }
                    );
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            try
            {
                port.Close();
                txb1.Text = "--,-- V";

                Btn1.Enabled = !Btn1.Enabled;
                Btn3.Enabled = !Btn3.Enabled;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                port.Write("1");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
       
    }

}



