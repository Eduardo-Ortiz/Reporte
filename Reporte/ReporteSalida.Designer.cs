namespace Reporte
{
    partial class ReporteSalida
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAjustePlanchon = new System.Windows.Forms.Label();
            this.txtNotaInventarioPlanchon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Procesado:";
            // 
            // txtFecha
            // 
            this.txtFecha.AutoSize = true;
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(79, 17);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(48, 13);
            this.txtFecha.TabIndex = 1;
            this.txtFecha.Text = "txtFecha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Datos Cargados Exitosamente";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ajuste de Inventario Planchon:";
            // 
            // txtAjustePlanchon
            // 
            this.txtAjustePlanchon.AutoSize = true;
            this.txtAjustePlanchon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAjustePlanchon.Location = new System.Drawing.Point(187, 99);
            this.txtAjustePlanchon.Name = "txtAjustePlanchon";
            this.txtAjustePlanchon.Size = new System.Drawing.Size(21, 13);
            this.txtAjustePlanchon.TabIndex = 4;
            this.txtAjustePlanchon.Text = "10";
            // 
            // txtNotaInventarioPlanchon
            // 
            this.txtNotaInventarioPlanchon.AutoSize = true;
            this.txtNotaInventarioPlanchon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotaInventarioPlanchon.Location = new System.Drawing.Point(13, 117);
            this.txtNotaInventarioPlanchon.Name = "txtNotaInventarioPlanchon";
            this.txtNotaInventarioPlanchon.Size = new System.Drawing.Size(133, 13);
            this.txtNotaInventarioPlanchon.TabIndex = 5;
            this.txtNotaInventarioPlanchon.Text = "txtNotaInventarioPlanchon";
            // 
            // ReporteSalida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 365);
            this.Controls.Add(this.txtNotaInventarioPlanchon);
            this.Controls.Add(this.txtAjustePlanchon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.label1);
            this.Name = "ReporteSalida";
            this.Text = "ReporteSalida";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txtAjustePlanchon;
        private System.Windows.Forms.Label txtNotaInventarioPlanchon;
    }
}