using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Formula;
using NPOI.SS.Util;
using NPOI.XSSF.Util;

namespace ManejadorArchivos
{

    public class TiposExcel
    {
        public const int XLS = 1;
        public const int XLSX = 2;
    }      
  
    public class ArchivoExcel
    {
        private string ruta;
        private int tipo;
        private HSSFWorkbook hssfwb;
        private XSSFWorkbook xssfwb;
        private ISheet sheet;

        /// <summary>
        /// Constructor que carga el archivo Excel para su lectura y modificación.
        /// </summary>
        /// <param name="ruta">Ubicación fisica del archivo Excel.</param>
        public ArchivoExcel(string ruta)
        {
            this.ruta = ruta;
            if(Path.GetExtension(ruta).ToLower()==".xls")
            {
                tipo = TiposExcel.XLS;
            }
            else
            {
                tipo = TiposExcel.XLSX;
            }                 
            using (FileStream file = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            {
                if(tipo == TiposExcel.XLS)
                {
                    hssfwb = new HSSFWorkbook(file);
                    sheet = hssfwb.GetSheetAt(0);
                }                    
                else
                {
                    xssfwb = new XSSFWorkbook(file);
                    sheet = xssfwb.GetSheetAt(0);
                }                           
                file.Close();
            }            
        }

        /// <summary>
        /// Cambia la hoja abierta del archivo.
        /// </summary>
        /// <param name="numero">Número de la hoja a seleccionar (inicia en 0).</param>
        public void CambiarHoja(int numero)
        {
            if (tipo == TiposExcel.XLS)
                sheet = hssfwb.GetSheetAt(numero);
            else
                sheet = xssfwb.GetSheetAt(numero);
        }

        /// <summary>
        /// Cambia la hoja abierta del archivo.
        /// </summary>
        /// <param name="nombre">Nombre de la hoja a seleccionar.</param>
        public void CambiarHoja(string nombre)
        {
            if (tipo == TiposExcel.XLS)
                sheet = hssfwb.GetSheet(nombre);
            else
                sheet = xssfwb.GetSheet(nombre);
        }

        /// <summary>
        /// Guarda los cambios hechos en el archivo.
        /// </summary>       
        public void GuardarCambios()
        {
            using (FileStream file = new FileStream(ruta, FileMode.Open, FileAccess.Write))
            {              
                if (tipo == TiposExcel.XLS)
                    hssfwb.Write(file);
                else
                    xssfwb.Write(file);                
                file.Close();
            }
        }

        /// <summary>
        /// Obtiene el valor de la celda con las coordenadas indicadas.
        /// </summary>     
        /// <param name="coordenadas">Coordenadas completas en forma de texto.</param>     
        public string ObtenerValorCoordenadas(string coordenadas)
        {
            CellReference cr = new CellReference(coordenadas);
            var row = sheet.GetRow(cr.Row);
            var cell = row.GetCell(cr.Col);           

            if (tipo == TiposExcel.XLS)
            {
                HSSFFormulaEvaluator  evaluator = new HSSFFormulaEvaluator(hssfwb);
                CellValue cellValue = evaluator.Evaluate(cell);

                if (cellValue.CellType == CellType.Numeric)
                    return cellValue.NumberValue.ToString();
                else
                    return cellValue.StringValue;               
            }                      
            else
            {
                XSSFFormulaEvaluator evaluator = new XSSFFormulaEvaluator(xssfwb);
                CellValue cellValue = evaluator.Evaluate(cell);
                if (cellValue.CellType == CellType.Numeric)      
                    return cellValue.NumberValue.ToString();    
                else               
                    return cellValue.StringValue;                
            }                            
        }

        /// <summary>
        /// Obtiene el valor de la celda con las coordenadas indicadas.
        /// </summary>     
        /// <param name="x">Letra correspondiente a la coordenada horizontal.</param>
        /// <param name="y">Número correspondiente a la coordenada vertical.</param>
        public string ObtenerValorCoordenadas(string x, int y)
        {
            string coordenadas = x + y.ToString();
            return this.ObtenerValorCoordenadas(coordenadas);         
        }

        /// <summary>
        /// Guarda el valor númerico en la celda especificada
        /// </summary>     
        /// <param name="coordenadas">Letra correspondiente a la coordenada horizontal.</param>
        /// <param name="valor">Valor a guardar.</param>
        public void GuardarValorNumerico(string coordenadas, double valor)
        {
            CellReference cr = new CellReference(coordenadas);
            var row = sheet.GetRow(cr.Row);
            var cell = row.GetCell(cr.Col);
            cell.SetCellValue(valor);
            cell.SetCellType(CellType.Numeric);    
        }

        /// <summary>
        /// Guarda el valor númerico en la celda especificada
        /// </summary>     
        /// <param name="x">Letra correspondiente a la coordenada horizontal.</param>
        /// <param name="y">Número correspondiente a la coordenada vertical.</param>
        /// <param name="valor">Valor a guardar.</param>
        public void GuardarValorNumerico(string x, int y, double valor)
        {
            string coordenadas = x + y.ToString();
            this.GuardarValorNumerico(coordenadas,valor);
        }

    }
}
