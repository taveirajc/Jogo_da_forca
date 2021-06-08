using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOVOFORCA
{
    static class acentos
    {
        public static char[] VerificarVetor(this string texto)
        {
            char[] novaArray = new char[6];
            char[] acentosA = { 'A', 'Á', 'Ã', 'Â', 'À' };
            char[] acentosE = { 'E', 'É', 'Ê', 'È' };
            char[] acentosI = { 'I', 'Í' };
            char[] acentosO = { 'O', 'Ó', 'Õ', 'Ô', 'Ò' };
            char[] acentosU = { 'U', 'Ú' };

            switch (texto)
            {
                case "A": novaArray = acentosA.ToArray(); break;
                case "E": novaArray = acentosE.ToArray(); break;
                case "I": novaArray = acentosI.ToArray(); break;
                case "O": novaArray = acentosO.ToArray(); break;
                case "U": novaArray = acentosU.ToArray(); break;
            }
            return novaArray;
        }
        public static char VerificarLetra(this char texto, char[] acentos)
        {
            var letra = acentos.Where(a => a == texto);
            return letra.FirstOrDefault();
        }
    }
}
