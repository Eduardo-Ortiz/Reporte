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
    class InventarioPlanchon
    {
        ArchivoExcel reporteNovedades;
        ArchivoPdf datosInventarioPlanchon;
        double tiraProg;
        DatosSalida salida;

        /// <summary>
        /// Constructor que carga el reporte donde se guardaran los datos y el archivo PDF del cual se obtendran.
        /// </summary>
        /// <param name="reporteNovedades">Archivo Excel donde se guardaran los datos.</param>
        /// <param name="datosInventarioPlanchon">Archivo PDF del cual se obtendran los datos.</param>
        public InventarioPlanchon(ArchivoExcel reporteNovedades, ArchivoPdf datosInventarioPlanchon, double tiraProg, DatosSalida salida)
        {
            this.reporteNovedades = reporteNovedades;
            this.datosInventarioPlanchon = datosInventarioPlanchon;
            this.tiraProg = tiraProg;
            this.salida = salida;
        }

        /// <summary>
        /// Copia los datos correspondientes del archivo PDF a su posicion en el archivo Excel del reporte.
        /// </summary>
        public void Procesar()
        {

            //Se obtiene la pagina número 1 del reporte
            string total = datosInventarioPlanchon.ObtenerPaginaTabla(1);
            //Guarda cada linea del archivo en una lista.
            List<string> tabla = total.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            tabla = tabla[tabla.Count - 3].Split(' ').ToList();

            var cantidadTotal = tabla[tabla.Count - 1];
            cantidadTotal = cantidadTotal.Replace(",", "");

            Console.WriteLine("El total es: " + cantidadTotal);

            //Se obtiene la pagina número 6 del reporte
            string texto = datosInventarioPlanchon.ObtenerPagina(6);         



            //Guarda cada linea del archivo en una lista.
            List<string> datos = texto.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //Recorre todas las lineas del archivo.
            for(int i = 0; i<datos.Count;i++)
            {
                //Cuando encuentra una linea con el texto "INSP Y ESC" si la siguiente linea no es "COL CONT", inserta una linea con este dato en valor 0.
                if (datos[i].Contains("INSP Y ESC"))
                {
                    if (!datos[i + 1].Contains("COL CONT"))
                    {
                        datos.Insert(i + 1, "COL CONT 0");                        
                    }                   
                }                
            }


            var resultadoResta = Textos.ExtraerNumeroComaDecimal(datos[1]) - tiraProg;
            //Se abre la hoja de datos del archivo Excel.
            reporteNovedades.CambiarHoja("DATOS");

            //Se mapean los datos con su respectiva celda.
            reporteNovedades.GuardarValorNumericoDia("126", tiraProg);
            reporteNovedades.GuardarValorNumericoDia("127", Textos.ExtraerNumeroComaDecimal(datos[2]));
            reporteNovedades.GuardarValorNumericoDia("128", resultadoResta);
            reporteNovedades.GuardarValorNumericoDia("129", Textos.ExtraerNumeroComaDecimal(datos[3]));
            reporteNovedades.GuardarValorNumericoDia("130", Textos.ExtraerNumeroComaDecimal(datos[4]));
            reporteNovedades.GuardarValorNumericoDia("131", Textos.ExtraerNumeroComaDecimal(datos[5]));
            reporteNovedades.GuardarValorNumericoDia("132", Textos.ExtraerNumeroComaDecimal(datos[6]));
            reporteNovedades.GuardarValorNumericoDia("134", Textos.ExtraerNumeroComaDecimal(datos[8]));
            reporteNovedades.GuardarValorNumericoDia("135", Textos.ExtraerNumeroComaDecimal(datos[9]));
            reporteNovedades.GuardarValorNumericoDia("136", Textos.ExtraerNumeroComaDecimal(datos[10]));
            reporteNovedades.GuardarValorNumericoDia("137", Textos.ExtraerNumeroComaDecimal(datos[11]));
            reporteNovedades.GuardarValorNumericoDia("138", Textos.ExtraerNumeroComaDecimal(datos[12]));
            reporteNovedades.GuardarValorNumericoDia("139", Textos.ExtraerNumeroComaDecimal(datos[13]));
            reporteNovedades.GuardarValorNumericoDia("140", Textos.ExtraerNumeroComaDecimal(datos[15]));
            reporteNovedades.GuardarValorNumericoDia("141", Textos.ExtraerNumeroComaDecimal(datos[16]));
            reporteNovedades.GuardarValorNumericoDia("142", Textos.ExtraerNumeroComaDecimal(datos[17]));
            reporteNovedades.GuardarValorNumericoDia("143", Textos.ExtraerNumeroComaDecimal(datos[18]));
            reporteNovedades.GuardarValorNumericoDia("144", Textos.ExtraerNumeroComaDecimal(datos[19]));

            //duda sobre ajuste
            reporteNovedades.GuardarValorNumericoDia("145", Textos.ExtraerNumeroComaDecimal(datos[20]));
            reporteNovedades.GuardarValorNumericoDia("147", Textos.ExtraerNumeroComaDecimal(datos[25]));

            //Evalua todas las formulas.
            reporteNovedades.EvaluarFormulas();
            double totalExcelNum = reporteNovedades.ObtenerValorRenglonDia(148);
            double totalInvplanchonNum = Textos.ConvertirANumero(cantidadTotal);
            double ajuste = totalInvplanchonNum - totalExcelNum;


            System.Console.WriteLine("Total Excel: " + totalExcelNum);
            System.Console.WriteLine("Total Excel String: " + reporteNovedades.ObtenerValorRenglonDia(148));
            System.Console.WriteLine("Total: " + totalInvplanchonNum);
            System.Console.WriteLine("Ajuste: " + ajuste);


            if (ajuste > 0)
            {
                double viasEscAjustada = Textos.ExtraerNumeroComaDecimal(datos[20]) + ajuste;
                reporteNovedades.GuardarValorNumericoDia("145", viasEscAjustada);
                reporteNovedades.EvaluarFormulas();                
            }

            salida.AjustePlanchon = ajuste;

            //Se confirman los cambios en el archivo.
            reporteNovedades.GuardarCambios();
        }


    }
}
