﻿using System;
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
        private string fecha;    

        /// <summary>
        /// Constructor que carga el archivo Excel para su lectura y modificación.
        /// </summary>
        /// <param name="ruta">Ubicación fisica del archivo Excel.</param>
        public ArchivoExcel(string ruta, bool sobreescribir)
        {
            this.ruta = ruta;
            CalcularFecha();
            if (Path.GetExtension(ruta).ToLower() == ".xls")
            {
                tipo = TiposExcel.XLS;
            }
            else
            {
                tipo = TiposExcel.XLSX;
            }
            using (FileStream file = new FileStream(ruta, FileMode.Open, FileAccess.Read))
            {
                if (tipo == TiposExcel.XLS)
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

            if(!sobreescribir)
            {
                string carpeta = System.IO.Path.GetDirectoryName(ruta);
                string nombre = System.IO.Path.GetFileNameWithoutExtension(ruta);
                string extension = System.IO.Path.GetExtension(ruta);
                string nuevoNombre = nombre + "-Actualizado-" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;

                Console.WriteLine(Path.Combine(carpeta, nuevoNombre));

                using (FileStream file = new FileStream(Path.Combine(carpeta, nuevoNombre), FileMode.Create, System.IO.FileAccess.Write))
                {
                    if (tipo == TiposExcel.XLS)
                        hssfwb.Write(file);
                    else
                        xssfwb.Write(file);
                    file.Close();
                }

                this.ruta = Path.Combine(carpeta, nuevoNombre);
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
                HSSFFormulaEvaluator evaluator = new HSSFFormulaEvaluator(hssfwb);
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
        public double ObtenerValorCoordenadas(string x, int y)
        {
            string coordenadas = x + y.ToString();
            return double.Parse(this.ObtenerValorCoordenadas(coordenadas));
        }


        /// <summary>
        /// Obtiene el valor de la celda del renglon especificado con la columna correspondiente al día actual.
        /// </summary>       
        /// <param name="y">Número correspondiente a la coordenada vertical.</param>
        public double ObtenerValorRenglonDia(int y)
        {
            string coordenadas = fecha + y.ToString();
            return double.Parse(this.ObtenerValorCoordenadas(coordenadas));
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
            this.GuardarValorNumerico(coordenadas, valor);
        }

        /// <summary>
        /// Guarda el valor númerico en la celda especificada en el dia anterior.
        /// </summary>     
        /// <param name="y">Letra correspondiente a la coordenada vertical.</param>        
        /// <param name="valor">Valor a guardar.</param>
        public void GuardarValorNumericoDia(string y, double valor)
        {
            string coordenadas = fecha + y;
            this.GuardarValorNumerico(coordenadas, valor);
        }

        /// <summary>
        /// Evalua el valor de todas las formulas del documento.
        /// </summary>    
        public void EvaluarFormulas()
        {
            if (tipo == TiposExcel.XLS)
                HSSFFormulaEvaluator.EvaluateAllFormulaCells(hssfwb);
            else
                XSSFFormulaEvaluator.EvaluateAllFormulaCells(xssfwb);
        }

        private void CalcularFecha()
        {
            int numeroDia = DateTime.Now.AddDays(-1).Day;

            switch (numeroDia)
            {
                case 1:
                    fecha = "B";
                    break;
                case 2:
                    fecha = "C";
                    break;
                case 3:
                    fecha = "D";
                    break;
                case 4:
                    fecha = "E";
                    break;
                case 5:
                    fecha = "F";
                    break;
                case 6:
                    fecha = "G";
                    break;
                case 7:
                    fecha = "H";
                    break;
                case 8:
                    fecha = "I";
                    break;
                case 9:
                    fecha = "J";
                    break;
                case 10:
                    fecha = "K";
                    break;
                case 11:
                    fecha = "L";
                    break;
                case 12:
                    fecha = "M";
                    break;
                case 13:
                    fecha = "N";
                    break;
                case 14:
                    fecha = "O";
                    break;
                case 15:
                    fecha = "P";
                    break;
                case 16:
                    fecha = "Q";
                    break;
                case 17:
                    fecha = "R";
                    break;
                case 18:
                    fecha = "S";
                    break;
                case 19:
                    fecha = "T";
                    break;
                case 20:
                    fecha = "U";
                    break;
                case 21:
                    fecha = "V";
                    break;
                case 22:
                    fecha = "W";
                    break;
                case 23:
                    fecha = "X";
                    break;
                case 24:
                    fecha = "Y";
                    break;
                case 25:
                    fecha = "Z";
                    break;
                case 26:
                    fecha = "AA";
                    break;
                case 27:
                    fecha = "AB";
                    break;
                case 28:
                    fecha = "AC";
                    break;
                case 29:
                    fecha = "AD";
                    break;
                case 30:
                    fecha = "AE";
                    break;
                case 31:
                    fecha = "AF";
                    break;
            }

        }
    }
 }

