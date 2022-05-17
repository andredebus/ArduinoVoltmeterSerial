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

        }

        SerialPort port;
        private void textBox1_Enter(Object sender, System.EventArgs e)
        {
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                port = new SerialPort("COM6", 115200);
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
                string timeNow = DateTime.Now.ToString();
                string tmpSerial = sp.ReadExisting();
                Thread.Sleep(500);
                this.textBox1.SelectionStart = 0;
                //textBox1.AppendText($"{timeNow}\t{tmpSerial.Replace(",","\t")}");
                textBox1.Text = $"{timeNow}\t{tmpSerial.Replace(",", "\t")}" + textBox1.Text;
                txb1.Text = tmpSerial.Substring(0, 6);
            }
                    );
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            try
            {
                port.Close();
                txb1.Text = "--.--V";

                Btn1.Enabled = true;
                Btn3.Enabled = false;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }


        private void button2_Click(object sender, EventArgs e)
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



