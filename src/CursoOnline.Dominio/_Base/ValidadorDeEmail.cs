using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CursoOnline.Dominio._Base
{
    public static class ValidadorDeEmail
    {
        public static bool ValidaEmail(this Entidade nomeQualquer, string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
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
