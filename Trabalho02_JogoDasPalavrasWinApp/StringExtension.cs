using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho02_JogoDasPalavrasWinApp
{
    public static class StringExtension
    {
        public static bool ContainsSemConsiderarAcento(this string str, char letra)
        {
            foreach (char c in str)
            {
                if (string.Compare(c.ToString(), letra.ToString(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                    return true;
            }

            return false;
        }
    }
}
