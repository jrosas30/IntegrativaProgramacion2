﻿namespace PROG2EVA1juanrosas
{
    partial class FormAcciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnTraspasarTabla = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(52, 159);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(719, 279);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnTraspasarTabla
            // 
            this.btnTraspasarTabla.Location = new System.Drawing.Point(52, 22);
            this.btnTraspasarTabla.Name = "btnTraspasarTabla";
            this.btnTraspasarTabla.Size = new System.Drawing.Size(93, 43);
            this.btnTraspasarTabla.TabIndex = 14;
            this.btnTraspasarTabla.Text = "TRASPASAR A TABLA";
            this.btnTraspasarTabla.UseVisualStyleBackColor = true;
            this.btnTraspasarTabla.Click += new System.EventHandler(this.btnTraspasarTabla_Click);
            // 
            // FormAcciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTraspasarTabla);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormAcciones";
            this.Text = "FormAcciones";
            this.Load += new System.EventHandler(this.FormAcciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnTraspasarTabla;
    }
}