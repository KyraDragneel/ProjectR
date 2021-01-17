using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class comprobacionNombres
    {
        public static List<string> comprobarNombres(List<elementoVariable> variables)
        {
            List<string> errores = new List<string>();
            string errorNombre = "Error: El nombre asignado a la variable no es valido. Linea: ";
            string[] arregloPalabras = { 
                "void",
                "return",
                "null",
                "true",
                "false",
                "System",
                "out",
                "print",
                "println",
                "break",
                "main",
                "static",
                "public",
                "private",
                "class",
                "Scanner",
                "new",
                "in",
                "import",
                "if",
                "else",
                "default",
                "while",
                "do",
                "for",
                "switch",
                "case",
                "int",
                "float",
                "double",
                "boolean",
                "String",
                "char",
                "next",
                "nextInt",
                "nextDouble",
                "nextFloat",
                "nextChar",
                "nextBoolean"
            };

            for(int i = 0; i < variables.Count; i++)
            {
                for(int j = 0; j < arregloPalabras.Length; j++)
                {
                    if(variables[i].getNombre().Equals(arregloPalabras[j]))
                    {
                        errores.Add(errorNombre + variables[i].getLinea());
                        j = arregloPalabras.Length;
                    }
                }
            }

            return errores;
        }
    }
}
