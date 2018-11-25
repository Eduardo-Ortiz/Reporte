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
using Reporte.Salida;

namespace Reporte
{
    public partial class Principal : Form
    {
        DatosSalida salida;
        ArchivoExcel reporteNovedades;
        InventarioPlanchon inventarioPlanchon;           

        public Principal()
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

                reporteNovedades = new ArchivoExcel(ruta,false);
                salida = new DatosSalida();         
                                         
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
              
                inventarioPlanchon = new InventarioPlanchon(reporteNovedades, reportePlanchon, double.Parse(txtTiraProg.Text.Replace(".",",")),salida);
                inventarioPlanchon.Procesar();
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            //Inicializa el switch de error y el mensaje.
            bool error = false;
            string mensaje = "";

            //Verifica que se haya cargado un archivo de reporte de novedades.
            if(reporteNovedades==null)
            {
                error = true;
                mensaje += "   -Seleccione el archivo de reporte de novedades." + Environment.NewLine;
            }
            //Verifica que se haya cargado un archivo de inventario de plachon.
            if (inventarioPlanchon==null)
            {
                error = true;
                mensaje += "   -Seleccione el archivo de inventario de planchon." + Environment.NewLine;
            }
            //Verifica que se haya ingresado una cantidad en el campo Tira Prog.
            if (string.IsNullOrWhiteSpace(txtTiraProg.Text))
            {
                error = true;
                mensaje += "   -El campo \"Tira Prog\" no puede estar vacio." + Environment.NewLine;
            }            

            //Si existe un error se muestra al usuario y no se procesa el reporte.
            if (error)
            {
                mensaje = "Solucione los errores para continuar:" + Environment.NewLine + mensaje;
                MessageBox.Show(mensaje, "Error al procesar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Si no hay errores se procesan todos los datos y se genera el archivo de salida.
            else
            {
                inventarioPlanchon.Procesar();
                ReporteSalida reporteSalida = new ReporteSalida(salida);
                reporteSalida.ShowDialog();
            }          
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
