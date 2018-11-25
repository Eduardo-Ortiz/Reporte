using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManejadorArchivos;
using ManejadorArchivos.Utilidades;
using Reporte.Salida;

namespace Reporte.Procesadores
{
    class InventarioSteckel
    {
        ArchivoExcel reporteNovedades;
        ArchivoPdf datosInventarioSteckel;
        double tiraProg;
        DatosSalida salida;

        /// <summary>
        /// Constructor que carga el reporte donde se guardaran los datos y el archivo PDF del cual se obtendran.
        /// </summary>
        /// <param name="reporteNovedades">Archivo Excel donde se guardaran los datos.</param>
        /// <param name="datosInventarioSteckel">Archivo PDF del cual se obtendran los datos.</param>
        public InventarioSteckel(ArchivoExcel reporteNovedades, ArchivoPdf datosInventarioSteckel, DatosSalida salida)
        {
            this.reporteNovedades = reporteNovedades;
            this.datosInventarioSteckel = datosInventarioSteckel;
            this.salida = salida;
        }

        /// <summary>
        /// Copia los datos correspondientes del archivo PDF a su posicion en el archivo Excel del reporte.
        /// </summary>
        public void Procesar()
        {

        }
    }
}
