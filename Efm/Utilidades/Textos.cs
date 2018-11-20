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
            //Extra el valor decimal del String.
            var valor = Regex.Match(cadena, @"[-+]?(?<![0-9]\.)\b[0-9]+(?:[,\s][0-9]+)*\.[0-9]+(?:[eE][-+]?[0-9]+)?\b(?!\.[0-9])").Value;

            //Si no se encontro un decimal busca un entero.
            if(valor=="")
            {
                valor = Regex.Match(cadena, @"\d+").Value;
            }

            //Se da formato al valor.
            valor = valor.Replace(",", "");
            valor = valor.Replace(".", ",");

            //Se regresa el valor encontrado
            return double.Parse(valor);           
        }
    }
}
