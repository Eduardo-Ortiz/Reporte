using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reporte.Salida;

namespace Reporte
{
    public partial class ReporteSalida : Form
    {
        DatosSalida datosSalida;
        public ReporteSalida(DatosSalida datosSalida)
        {
            InitializeComponent();
            this.datosSalida = datosSalida;

            txtFecha.Text = datosSalida.Fecha;
            txtAjustePlanchon.Text = datosSalida.AjustePlanchon.ToString();

            if(datosSalida.AjustePlanchon<30)
            {
                txtNotaInventarioPlanchon.Text = "El ajuste se encuentra dentro del rango normal.";
            }

        }
    }
}
