using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Utils
{
    public static class Helper
    {
        public static decimal StringToDecimal(string valorEmString)
        {
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            valorEmString = valorEmString.Replace(".", ",");

            decimal valorDecimal = decimal.Parse(valorEmString, cultureInfo);

            return valorDecimal;
        }

        public static decimal CalcularImposto(string preco)
        {
            var valor = StringToDecimal(preco);

            decimal percentual = 0.15m;


            decimal imposto = valor * percentual;

            decimal impostoArredondado = Math.Round(imposto, 2);

            return impostoArredondado;
        }
    }

}
