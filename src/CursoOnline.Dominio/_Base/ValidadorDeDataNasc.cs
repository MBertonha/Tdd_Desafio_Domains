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
            Regex r = new Regex(@"(\d{2}\/\d{2}\/\d{4} \d{2}:\d{2})");
            return r.Match(data).Success;
        }
    }
}
