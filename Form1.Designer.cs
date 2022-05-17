﻿using System.IO.Ports;
using System.Threading;

namespace Arduino_Serial
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Btn1 = new System.Windows.Forms.Button();
            this.txb1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn2 = new System.Windows.Forms.Button();
            this.Btn3 = new System.Windows.Forms.Button();
            this.Btn4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Btn5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn1
            // 
            this.Btn1.Location = new System.Drawing.Point(33, 12);
            this.Btn1.Name = "Btn1";
            this.Btn1.Size = new System.Drawing.Size(167, 59);
            this.Btn1.TabIndex = 0;
            this.Btn1.Text = "Verbinden";
            this.Btn1.UseVisualStyleBackColor = true;
            this.Btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // txb1
            // 
            this.txb1.BackColor = System.Drawing.Color.Black;
            this.txb1.Cursor = System.Windows.Forms.Cursors.No;
            this.txb1.Font = new System.Drawing.Font("LCD", 48F);
            this.txb1.ForeColor = System.Drawing.Color.Lime;
            this.txb1.Location = new System.Drawing.Point(33, 52);
            this.txb1.MaxLength = 6;
            this.txb1.Name = "txb1";
            this.txb1.ReadOnly = true;
            this.txb1.ShortcutsEnabled = false;
            this.txb1.Size = new System.Drawing.Size(294, 157);
            this.txb1.TabIndex = 1;
            this.txb1.Text = "--.--";
            this.txb1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb1.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txb1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(78, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 244);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arduino Sensor A0";
            // 
            // Btn2
            // 
            this.Btn2.Location = new System.Drawing.Point(33, 449);
            this.Btn2.Name = "Btn2";
            this.Btn2.Size = new System.Drawing.Size(227, 46);
            this.Btn2.TabIndex = 3;
            this.Btn2.Text = "Exit";
            this.Btn2.UseVisualStyleBackColor = true;
            this.Btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // Btn3
            // 
            this.Btn3.Location = new System.Drawing.Point(33, 94);
            this.Btn3.Name = "Btn3";
            this.Btn3.Size = new System.Drawing.Size(167, 59);
            this.Btn3.TabIndex = 4;
            this.Btn3.Text = "Trennen";
            this.Btn3.UseVisualStyleBackColor = true;
            this.Btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // Btn4
            // 
            this.Btn4.Location = new System.Drawing.Point(344, 449);
            this.Btn4.Name = "Btn4";
            this.Btn4.Size = new System.Drawing.Size(227, 46);
            this.Btn4.TabIndex = 5;
            this.Btn4.Text = "Reset";
            this.Btn4.UseVisualStyleBackColor = true;
            this.Btn4.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(615, 64);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(470, 582);
            this.textBox1.TabIndex = 0;
            // 
            // Btn5
            // 
            this.Btn5.Location = new System.Drawing.Point(267, 12);
            this.Btn5.Name = "Btn5";
            this.Btn5.Size = new System.Drawing.Size(214, 60);
            this.Btn5.TabIndex = 6;
            this.Btn5.Text = "Messung Starten";
            this.Btn5.UseVisualStyleBackColor = true;
            this.Btn5.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1107, 658);
            this.Controls.Add(this.Btn5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Btn4);
            this.Controls.Add(this.Btn3);
            this.Controls.Add(this.Btn2);
            this.Controls.Add(this.Btn1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Arduino RS232 Komunikation";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button Btn1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn2;
        public System.Windows.Forms.Button Btn3;
        private System.Windows.Forms.Button Btn4;
        public System.Windows.Forms.TextBox txb1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Btn5;
    }

}
