using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Arduino_Serial
{
    
public partial class Form1 : Form
    {
        SerialPort port;
        bool Messung = false;
        bool MessungAufzeichnen = false;
        UInt16 zaehler = 1;
        public Form1()
        {
            InitializeComponent();

            Btn3.Enabled = false;
            Btn5.Enabled = false;
            Btn5.ForeColor = System.Drawing.Color.White;
                        
        }
        
    
        
        private void Btn1_Click(object sender, EventArgs e)
        {
            try
            {
                port = new SerialPort("COM3", 115200);
                port.DataReceived += new SerialDataReceivedEventHandler(ReceivedSerialHandler);
                port.ReadTimeout = 500;
                port.WriteTimeout = 500;
                port.Open();
                port.DiscardOutBuffer();
                port.WriteLine("1");
                Btn1.Enabled = false;
                Btn3.Enabled = true;
                Btn5.Enabled = true;
                string DatumAktuell = DateTime.Now.ToString("d");
                txb2.Text = DatumAktuell;
                Messung = !Messung;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                port.Close();
                
            }
            
        }
        public void ReceivedSerialHandler(object sender, SerialDataReceivedEventArgs e)
       
        {
            SerialPort sp = (SerialPort)sender;
            Invoke(
                (MethodInvoker)delegate
            {
                Thread.Sleep(100);
                string ZeitAktuell = DateTime.Now.ToString("T");
                string TmpSerial = sp.ReadExisting();
                string WertVolt = TmpSerial.Substring(0, 7);
                //textBox1.AppendText($"{timeNow}\t{tmpSerial.Replace(",","\t")}");
                
                if (MessungAufzeichnen)
                {
                    int letzteZeile = dataGridView1.RowCount;
                    int rowId = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowId];
                    row.Cells["Column1"].Value = ZeitAktuell;
                    row.Cells["Column2"].Value = WertVolt.Replace(".", ",");
                    row.Cells["Column3"].Value = zaehler;
                    zaehler += 1;
                    
                    
                    if (cBx1.Checked)
                    {
                        dataGridView1.Sort(dataGridView1.Columns["Column3"], ListSortDirection.Descending);
                        dataGridView1.CurrentCell = dataGridView1.Rows[letzteZeile].Cells[2];
                    }
                    else
                    {
                        dataGridView1.Sort(dataGridView1.Columns["Column3"], ListSortDirection.Ascending);
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                    }
                            
                    //textBox1.Text = $" {ZeitAktuell}\t{TmpSerial.Replace(",", " \t")}" + textBox1.Text;
                }
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
                    Btn1.Enabled = !Btn1.Enabled;
                    Btn3.Enabled = !Btn3.Enabled;
                    Btn5.Enabled = false;
                }
            else
                {
                    port.Close();
                    txb1.Text = "--,-- V";
                    //textBox1.Clear();
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
                if (!MessungAufzeichnen)
                {
                    Btn5.ForeColor = System.Drawing.Color.Red;
                    Btn5.Text = "Messung beenden";
                    MessungAufzeichnen = !MessungAufzeichnen;
                    
                }
                else
                {
                    Btn5.ForeColor = System.Drawing.Color.White;
                    Btn5.Text = "Messung starten";
                    MessungAufzeichnen = !MessungAufzeichnen;
                }
                
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            zaehler = 1;
        }
    }

}



