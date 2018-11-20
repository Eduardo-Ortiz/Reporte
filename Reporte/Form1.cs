using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManejadorArchivos;
using Reporte.Procesadores;

namespace Reporte
{
    public partial class Form1 : Form
    {
        ArchivoExcel reporteNovedades;
        //Hola
      
        public Form1()
        {
            InitializeComponent();
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult resultado = openFileDialog1.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                string ruta = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                label1.Text = ruta;

                reporteNovedades = new ArchivoExcel(ruta);             
                             
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = openFileDialog2.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                string ruta = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                label2.Text = ruta;

                ArchivoPdf reportePlanchon;
                reportePlanchon = new ArchivoPdf(ruta);

               

               
                InventarioPlanchon inventarioPlanchon = new InventarioPlanchon(reporteNovedades, reportePlanchon, double.Parse(textBox1.Text));
                inventarioPlanchon.Procesar();
            }
        }

      
    }
}
