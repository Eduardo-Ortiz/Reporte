using ManejadorArchivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporte.Procesadores
{
    class InventarioCoordinacion
    {

        ArchivoExcel reporteNovedades;
        ArchivoExcel datosInventarioCoordinación;
        /// <summary>
        /// Constructor que carga el reporte donde se guardaran los datos y el archivo excel del cual se obtendran.
        /// </summary>
        /// <param name="reporteNovedades">Archivo Excel donde se guardaran los datos.</param>
        /// <param name="datosInventarioPlanchon">Archivo PDF del cual se obtendran los datos.</param>
        public InventarioCoordinacion(ArchivoExcel reporteNovedades, ArchivoExcel datosInventarioCoordinación)
        {
            this.reporteNovedades = reporteNovedades;
            this.datosInventarioCoordinación = datosInventarioCoordinación;
          
        }




        /// <summary>
        /// Copia los datos correspondientes del archivo Excel a su posicion en el archivo Excel del reporteNovedades.
        /// </summary>
        public void Procesar()
        {

            string hoja = ObtenerHojaDia();
            datosInventarioCoordinación.CambiarHoja(hoja);

            double altohorno4 = datosInventarioCoordinación.ObtenerValorCoordenadas("G", 7);
            double altohorno5 = datosInventarioCoordinación.ObtenerValorCoordenadas("G", 8);
            double altohorno6 = datosInventarioCoordinación.ObtenerValorCoordenadas("G", 9);
            double BOF1 = datosInventarioCoordinación.ObtenerValorCoordenadas("E", 14);
            double BOF2 = datosInventarioCoordinación.ObtenerValorCoordenadas("E", 17);
            double hornoElectrico = datosInventarioCoordinación.ObtenerValorCoordenadas("E", 18);
            double transporte = datosInventarioCoordinación.ObtenerValorCoordenadas("E", 19);


            //Se mapean los datos con su respectiva celda.
            reporteNovedades.GuardarValorNumericoDia("11", altohorno4);
            reporteNovedades.GuardarValorNumericoDia("12", altohorno5);
            reporteNovedades.GuardarValorNumericoDia("13", altohorno6);
            reporteNovedades.GuardarValorNumericoDia("23", BOF1);
            reporteNovedades.GuardarValorNumericoDia("24", BOF2);
            reporteNovedades.GuardarValorNumericoDia("25", hornoElectrico);
            reporteNovedades.GuardarValorNumericoDia("32", transporte);


            //Evalua todas las formulas.
            reporteNovedades.EvaluarFormulas();
 
            //Se confirman los cambios en el archivo.
            reporteNovedades.GuardarCambios();
        }


        //Funcion para obtener la hoja del día del inventario de coordinacion
        private string ObtenerHojaDia()
        {
            int diaSemana = (int)DateTime.Now.DayOfWeek;
            int numeroDia = DateTime.Now.AddDays(-1).Day;
            string nombreHoja = "";

            switch (diaSemana)
            {
                case 0:
                    nombreHoja = "SAB-" + numeroDia;
                    break;
                case 1:
                    nombreHoja = "DOM-" + numeroDia;
                    break;
                case 2:
                    nombreHoja = "LUN-" + numeroDia;
                    break;
                case 3:
                    nombreHoja = "MAR-" + numeroDia;
                    break;
                case 4:
                    nombreHoja = "MIE-" + numeroDia;
                    break;
                case 5:
                    nombreHoja = "JUE-" + numeroDia;
                    break;
                case 6:
                    nombreHoja = "VIE-" + numeroDia;
                    break;


            }

            return nombreHoja;

        }


    }

}
