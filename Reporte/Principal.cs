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
        InventarioSteckel inventarioSteckel;
        InventarioCoordinacion inventarioCoordinacion;


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
                label1.Font = new Font(label1.Font, FontStyle.Regular);

                reporteNovedades = new ArchivoExcel(ruta, false);
                salida = new DatosSalida();

                txtTiraProg.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;               
                                         
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtTiraProg.Text))
            {
                DialogResult resultado = openFileDialog2.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    string ruta = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                    label2.Text = ruta;
                    label2.Font = new Font(label2.Font, FontStyle.Regular);

                    ArchivoPdf reportePlanchon;
                    reportePlanchon = new ArchivoPdf(ruta);

                    inventarioPlanchon = new InventarioPlanchon(reporteNovedades, reportePlanchon, double.Parse(txtTiraProg.Text.Replace(".", ",")), salida);

                    if (!inventarioPlanchon.FechaValida())
                    {
                        DialogResult alertaFecha = MessageBox.Show("La fecha del reporte de Inventario Planchon seleccionado no es la correspondiente al día actual ¿Desea continuar?", "Inconsistencia en la fecha", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (alertaFecha == DialogResult.No)
                        {
                            inventarioPlanchon = null;
                            label2.Text = null;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese el valor necesario en el campo 'Tira Prog' antes de seleccionar el archivo de inventario de planchon", "Acción Requerida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult resultado = openFileDialog2.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                string ruta = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                label5.Text = ruta;
                label5.Font = new Font(label5.Font, FontStyle.Regular);

                ArchivoPdf reporteSteckel;
                reporteSteckel = new ArchivoPdf(ruta);

                inventarioSteckel = new InventarioSteckel(reporteNovedades, reporteSteckel);           
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult resultado = openFileDialog2.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                string ruta = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                label6.Text = ruta;
                label6.Font = new Font(label6.Font, FontStyle.Regular);

                ArchivoExcel reporteCoordinacion;
                reporteCoordinacion = new ArchivoExcel(ruta, true);

                inventarioCoordinacion = new InventarioCoordinacion(reporteNovedades, reporteCoordinacion);
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            //Inicializa el switch de error y el mensaje.
            bool error = false;
            string mensaje = "";

            //Verifica que se haya cargado un archivo de reporte de novedades.
            if (reporteNovedades == null)
            {
                error = true;
                mensaje += "   -Seleccione el archivo de reporte de novedades." + Environment.NewLine;
            }
            //Verifica que se haya cargado un archivo de inventario de plachon.
            if (inventarioPlanchon == null)
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

            //Verifica que se haya cargado un archivo de inventario de steckel.
            if (inventarioSteckel == null)
            {
                error = true;
                mensaje += "   -Seleccione el archivo de inventario de steckel." + Environment.NewLine;
            }

            //Verifica que se haya cargado un archivo de inventario de coordinación.
            if (inventarioCoordinacion == null)
            {
                error = true;
                mensaje += "   -Seleccione el archivo de inventario de coordinacion." + Environment.NewLine;
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
                inventarioCoordinacion.Procesar();
                inventarioSteckel.Procesar();
                ReporteSalida reporteSalida = new ReporteSalida(salida);
                reporteSalida.ShowDialog();
            }
        }

    }
}
