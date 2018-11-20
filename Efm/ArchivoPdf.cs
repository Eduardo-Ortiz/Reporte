using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.IO;

namespace ManejadorArchivos
{
    public class ArchivoPdf
    {
        string ruta;

        /// <summary>
        /// Constructor que carga el archivo PDF para su lectura.
        /// </summary>
        /// <param name="ruta">Ubicación fisica del archivo PDF.</param>
        public ArchivoPdf(string ruta)
        {
            this.ruta = ruta;
        }

        /// <summary>
        /// Obtiene la pagina solicitada del archivo PDF en formato de string.
        /// </summary>
        /// <param name="pagina">Número de la pagina a obtener.</param>
        public string ObtenerPagina(int pagina)
        {
            if (!File.Exists(ruta))
                throw new FileNotFoundException("ruta");

            using (PdfReader reader = new PdfReader(ruta))
            {
                StringBuilder sb = new StringBuilder();

                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                string text = PdfTextExtractor.GetTextFromPage(reader, pagina, strategy);
                sb.Append(Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text))));

                return sb.ToString();
            }
        }


        /// <summary>
        /// Obtiene la pagina tabla solicitada del archivo PDF en formato de string.
        /// </summary>
        /// <param name="pagina">Número de la pagina a obtener.</param>
        public string ObtenerPaginaTabla(int pagina)
        {
            if (!File.Exists(ruta))
                throw new FileNotFoundException("ruta");

            using (PdfReader reader = new PdfReader(ruta))
            {
                StringBuilder sb = new StringBuilder();                

                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                string text = PdfTextExtractor.GetTextFromPage(reader, pagina, its);


                sb.Append(Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text))));

                return sb.ToString();
            }
        }
    }
}

