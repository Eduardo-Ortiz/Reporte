using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporte.Salida
{ 

    public class DatosSalida
    {
        private string fecha;
        private double ajustePlanchon;

        public DatosSalida()
        {
            DateTime fechaActual = DateTime.Now;
            this.fecha = "El " + fechaActual.Day + " de " + fechaActual.Month + " del " + fechaActual.Year + " a las " + fechaActual.Hour + ":" + fechaActual.Minute;
        }

        public double AjustePlanchon
        {
            get
            {
                return ajustePlanchon;
            }
            set
            {
                ajustePlanchon = value;
            }
        }

        public string Fecha
        {
            get
            {
                return fecha;
            }
        }
    }
}
