using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CursoOnline.Dominio._Base
{
    public static class ValidadorDeDataNasc
    {
        public static bool ValidarData(this Entidade nomeQualquer, string data)
        {
            Regex r = new Regex(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$");
            
            if (r.IsMatch(data))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
