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
       
        /// <summary>
        /// Constructor que carga el reporte donde se guardaran los datos y el archivo PDF del cual se obtendran.
        /// </summary>
        /// <param name="reporteNovedades">Archivo Excel donde se guardaran los datos.</param>
        /// <param name="datosInventarioSteckel">Archivo PDF del cual se obtendran los datos.</param>
        public InventarioSteckel(ArchivoExcel reporteNovedades, ArchivoPdf datosInventarioSteckel)
        {
            this.reporteNovedades = reporteNovedades;
            this.datosInventarioSteckel = datosInventarioSteckel;     
        }

        /// <summary>
        /// Copia los datos correspondientes del archivo PDF a su posicion en el archivo Excel del reporte.
        /// </summary>
        public void Procesar()
        {        
            //Se obtiene la pagina número 1 del reporte
            string texto = datosInventarioSteckel.ObtenerPagina(1);

            //Guarda cada linea del archivo en una lista.
            List<string> datos = texto.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //Varaibles para detectar cuando se encuentran los valores buscados.
            bool rollo = false;
            bool placa = false;
            bool normalizado = false;

            //Variables para guardar los datos.
            double valorRollo = 0;
            double valorPlaca = 0;
            double valorNormalizado = 0;

            foreach (string linea in datos)
            {
                //Se obtiene la primera palabra de la linea.
                string inicioLinea = linea.Split(' ')[0];

                //Si la linea tiene el dato de rollo y aun no se encuentra ese valor.
                if(inicioLinea=="ROLLO"&&!rollo)
                {
                    //Notifica que se encontro el valor de Rollo.
                    rollo = true;
                    //Convierte el valor a double y lo guarda en la variable correspondiente.
                    valorRollo = Textos.ConvertirANumero(linea.Split(' ')[1]);
                }

                //Si la linea contiene el dato de placa y aun no se encuentra ese valor, se descarta el renglon 'PLACA + ROLLO'.
                if (inicioLinea=="PLACA"&& !placa && !linea.Contains("ROLLO"))
                {
                    //Notifica que se encontro el valor de Placa.
                    placa = true;
                    //Convierte el valor a double y lo guarda en la variable correspondiente.
                    valorPlaca = Textos.ConvertirANumero(linea.Split(' ')[1]);
                }

                //Si la linea contiene el dato de normalizado y aun no se encuentra ese valor.
                if (inicioLinea == "NORMALIZADO" && !normalizado)
                {
                    //Notifica que se encontro el valor de Normalizado.
                    normalizado = true;
                    //Convierte el valor a double y lo guarda en la variable correspondiente.
                    valorNormalizado = Textos.ConvertirANumero(linea.Split(' ')[1]);
                }
            }


            //Se mapean los datos con su respectiva celda.
            reporteNovedades.GuardarValorNumericoDia("61", valorRollo);
            reporteNovedades.GuardarValorNumericoDia("60", valorPlaca);
            reporteNovedades.GuardarValorNumericoDia("62", valorNormalizado);

            //Se confirman los cambios en el archivo.
            reporteNovedades.GuardarCambios();
        }
    }
}
