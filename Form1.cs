using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace Arduino_Serial
{

    public partial class Form1 : Form
    {
        static SerialPort port;
        bool Messung = false;
        bool MessungAufzeichnen = false;
        UInt16 zaehler = 1;
        static readonly int ersteZeile = 0;
        public Form1()
        {
            
            InitializeComponent();

            Btn3.Enabled = false;
            Btn5.Enabled = false;
            Btn5.ForeColor = System.Drawing.Color.White;
                        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
                
                string[] ports = SerialPort.GetPortNames();
                comboBoxComPort.Items.AddRange(ports);
                
            if (comboBoxComPort.Items.Count != 0)
            
            {
                
                comboBoxComPort.Text = comboBoxComPort.Items[0].ToString();
            
            }
                

           
        }
       
        private void cBx1_CheckedChanged(Object sender, EventArgs e)
            
        {
            
            if (cBx1.Checked)

            {
                dataGridView1.Sort(dataGridView1.Columns["messNr"], ListSortDirection.Descending);
                dataGridView1.FirstDisplayedScrollingRowIndex = ersteZeile;
            }

            else

            {
                dataGridView1.Sort(dataGridView1.Columns["messNr"], ListSortDirection.Ascending);
                dataGridView1.FirstDisplayedScrollingRowIndex = ersteZeile;
            }


        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            if (comboBoxComPort.Items.Count > 0)
            {
                try
                {
                    port = new SerialPort(comboBoxComPort.Text, Convert.ToInt32(comboBoxbaudRate.Text));
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
                    //port.Close();

                }
            }
            else
            {
            Form1_Load(this, null); 
            }
            

            //try
            //{
            //    port = new SerialPort(comboBoxComPort.Text, Convert.ToInt32(comboBoxbaudRate.Text));
            //    port.DataReceived += new SerialDataReceivedEventHandler(ReceivedSerialHandler);
            //    port.ReadTimeout = 500;
            //    port.WriteTimeout = 500;
            //    port.Open();
            //    port.DiscardOutBuffer();
            //    port.WriteLine("1");
            //    Btn1.Enabled = false;
            //    Btn3.Enabled = true;
            //    Btn5.Enabled = true;
                
            //    string DatumAktuell = DateTime.Now.ToString("d");
            //    txb2.Text = DatumAktuell;
            //    Messung = !Messung;

            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    //port.Close();
                
            //}
            
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
                //textBox1.Text = $" {ZeitAktuell}\t{TmpSerial.Replace(",", " \t")}" + textBox1.Text;
                if (WertVolt.Length > 0)
                
                {
                    progressBar1.Value = 1;
                }


                if (MessungAufzeichnen)
                {
                    int letzteZeile = dataGridView1.RowCount;
                    //int ersteZeile = 0;
                    int zeileId = dataGridView1.Rows.Add();
                    DataGridViewRow zeile = dataGridView1.Rows[zeileId];
                    zeile.Cells["messZeit"].Value = ZeitAktuell;
                    zeile.Cells["messWert"].Value = WertVolt.Replace(".", ",");
                    zeile.Cells["messNr"].Value = zaehler;
                    zaehler += 1;
                    
                    
                    if (cBx1.Checked)
                    
                    {
                        dataGridView1.Sort(dataGridView1.Columns["messNr"], ListSortDirection.Descending);
                        dataGridView1.FirstDisplayedScrollingRowIndex = ersteZeile;
                    }
                    
                    else
                    
                    {
                        dataGridView1.Sort(dataGridView1.Columns["messNr"], ListSortDirection.Ascending);
                        dataGridView1.FirstDisplayedScrollingRowIndex = letzteZeile;
                    }
                            
                    
                }
                txb1.Text = WertVolt.Replace(".", ",");
               
            }
                    );
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
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
                    progressBar1.Value = 0;
                }
            else
                {
                    progressBar1.Value = 0;
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
            if (MessungAufzeichnen)
            {
                Btn5.Text = "Messung starten";
                port.Write("1");
                MessungAufzeichnen = !MessungAufzeichnen;
                //port.Close();
                Form1_Load(this, null);
            }
            else
            {
                Form1_Load(this, null);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Excel._Application app = new Excel.Application();
                Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Excel._Worksheet arbeitsmappe = null;
                app.Visible = true;
                arbeitsmappe = workbook.Sheets[1];
                arbeitsmappe = workbook.ActiveSheet;
                int spalte;
                int zeile;

                try
                {
                    // Spaltenüberschriften von gridview auf Excel Arbeitsmappe übertragen ACHTUNG Index Gridview startet bei 0
                    // Index bei Excel startet bei 1 >> daher "arbeitsmappe.Cells[1, spalte + 1]"
                    
                    for (spalte = 0; spalte < dataGridView1.Columns.Count; ++spalte)
                    {
                        arbeitsmappe.Cells[1, spalte + 1] = dataGridView1.Columns[spalte].HeaderText;
                    }
                    for (zeile = 0; zeile < dataGridView1.Rows.Count; ++zeile)
                    {
                        for (spalte = 0; spalte < dataGridView1.Columns.Count; ++spalte)
                        {
                            if (dataGridView1.Rows[zeile].Cells[spalte].Value != null)
                            {
                                arbeitsmappe.Cells[zeile + 2, spalte + 1] = dataGridView1.Rows[zeile].Cells[spalte].Value.ToString();
                            }
                            else
                            {
                                arbeitsmappe.Cells[zeile + 2, spalte + 1] = "";
                            }
                        }
                    for (spalte = 0; spalte < dataGridView1.Columns.Count; ++spalte)
                    {
                    arbeitsmappe.Columns[spalte + 1].AutoFit();
                    }
                    }

                    //Getting the location and file name of the excel to save from user.
                    //SaveFileDialog saveDialog = new SaveFileDialog();
                    //saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    //saveDialog.FilterIndex = 2;

                    //if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    //    workbook.SaveAs(saveDialog.FileName);
                    //    MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    
                    app.Quit();
                    workbook = null;
                    arbeitsmappe = null;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
    }
}





