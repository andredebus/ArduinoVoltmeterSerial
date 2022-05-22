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
            Btn5.ForeColor = System.Drawing.Color.White;
            
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
                int rowId = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowId];
                row.Cells["Column1"].Value = ZeitAktuell;
                row.Cells["Column2"].Value = WertVolt.Replace(".", ",");
                row.Cells["Column3"].Value = TmpSerial.Substring(11);
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1; 
                //dataGridView1.CurrentCell = dataGridView1.Rows[rowId].Cells[0];
                textBox1.Text = $" {ZeitAktuell}\t{TmpSerial.Replace(",", " \t")}" + textBox1.Text;
                txb1.Text = WertVolt.Replace(".", ",");
               
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
                    Btn5.ForeColor = System.Drawing.Color.White;
                    port.Write("1");
                    Messung = !Messung;
                    port.Close();
                    txb1.Text = "--,-- V";
                    dataGridView1.Rows.Clear();
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
                    dataGridView1.Rows.Clear();
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
                    Btn5.ForeColor = System.Drawing.Color.Red;
                    Btn5.Text = "Messung beenden";
                }
                else
                {
                    Btn5.ForeColor = System.Drawing.Color.White;
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



