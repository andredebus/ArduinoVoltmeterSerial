using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Arduino_Serial
{
    
public partial class Form1 : Form
    {
        bool Messung = false;
       
        public Form1()
        {
            InitializeComponent();

            Btn3.Enabled = false;
            Btn5.Enabled = false;
            //if (Messung == true)
            //{
            //    Btn5.Text = "Messung beenden";
            //}

        }
        
    SerialPort port;
        //private void TextBox1_Enter(Object sender, System.EventArgs e)
        //{
        //    textBox1.SelectionStart = 0;
        //    textBox1.SelectionLength = 0;
        //}
        private void Btn1_Click(object sender, EventArgs e)
        {
            try
            {
                port = new SerialPort("COM3", 115200);
                port.DataReceived += new SerialDataReceivedEventHandler(ReceivedSerialHandler);
                port.Open();

                Btn1.Enabled = false;
                Btn3.Enabled = true;
                Btn5.Enabled = true;

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
                //Thread.Sleep(500);
                string ZeitAktuell = DateTime.Now.ToString();
                string TmpSerial = sp.ReadExisting();
                string WertVolt = TmpSerial.Substring(0, 7);
                //textBox1.AppendText($"{timeNow}\t{tmpSerial.Replace(",","\t")}");
                textBox1.Text = $"{Messung} {ZeitAktuell}\t{TmpSerial.Replace(",", " \t")}" + textBox1.Text;
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
                if (Messung)
                {
                    Btn5.Text = "Messung starten";
                    port.Write("1");
                    Messung = !Messung;
                    port.Close();
                    txb1.Text = "--,-- V";
                    textBox1.Clear();
                    Btn1.Enabled = !Btn1.Enabled;
                    Btn3.Enabled = !Btn3.Enabled;
                    Btn5.Enabled = false;
                }
            else
                {
                    port.Close();
                    txb1.Text = "--,-- V";
                    textBox1.Clear();
                    Btn1.Enabled = !Btn1.Enabled;
                    Btn3.Enabled = !Btn3.Enabled;
                    Btn5.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Messung)
            {
                Btn5.Text = "Messung starten";
                port.Write("1");
                Messung = !Messung;
                port.Close();
                Application.Restart();
            }
            else
            {
                Application.Restart();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Messung)
                {
                    Btn5.Text = "Messung beenden";
                }
                else
                {
                    Btn5.Text = "Messung starten";
                }
                
                port.Write("1");
                Messung = !Messung;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
       
    }

}



