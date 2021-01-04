using compiladorR.Analisis.Semantica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion
{
    class compiladorInstrucciones
    {

        public static void compilarInstrucciones(List<elementoToken> tokens)
        {
            List<elementoToken> lineaEjecutada = new List<elementoToken>();
            int posicionTermino;

            for (int i = ignorarInicio(tokens); i < tokens.Count; i++)
            {
                if (tokens[i].getNombre().Equals("System"))
                {
                    posicionTermino = i;

                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals(";"))
                        {
                            posicionTermino = j;
                            j = tokens.Count;
                        }
                    }

                    lineaEjecutada = obtenerTokensLinea(tokens, i, posicionTermino);

                    for (int j = 0; j < lineaEjecutada.Count; j++)
                    {
                        Console.WriteLine(lineaEjecutada[j].getNombre());
                    }
                }
            }

        }

        private static int ignorarInicio(List<elementoToken> tokens)
        {
            int numeroToken = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].getNombre().Equals("main"))
                {
                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals(")"))
                        {
                            numeroToken = j + 1;
                            j = tokens.Count;
                        }
                    }
                }
            }

            return numeroToken;
        }

        private static List<elementoToken> obtenerTokensLinea(List<elementoToken> tokens, int inicio, int final)
        {
            List<elementoToken> lista = new List<elementoToken>();

            for (int i = inicio; i <= final; i++)
            {
                lista.Add(tokens[i]);
            }

            return lista;
        }
    }
}
