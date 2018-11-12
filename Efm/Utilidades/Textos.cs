using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ManejadorArchivos.Utilidades
{
    public class Textos
    {
        /// <summary>
        /// Obtiene los numeros contenidos en una cadena de texto.
        /// </summary>     
        /// <param name="cadena">Texto del cual se va a extraer el número.</param>     
        public static double ExtraerNumeroComaDecimal(string cadena)
        {
            var valor = Regex.Match(cadena, @"[-+]?(?<![0-9]\.)\b[0-9]+(?:[,\s][0-9]+)*\.[0-9]+(?:[eE][-+]?[0-9]+)?\b(?!\.[0-9])").Value;
            valor = valor.Replace(",", "");
            valor = valor.Replace(".", ",");
            return double.Parse(valor);           
        }
    }
}
