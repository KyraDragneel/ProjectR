﻿using compiladorR.Analisis.Gramaticas;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class comprobacionConcatenacion
    {
        public static List<string> comprobarConcatenacion(List<elementoVariable> variables)
        {
            List<string> errores = new List<string>();
            string errorDeclaracion = "Error de Cadena: El valor de la variable asignada no ha sido declarado previamente. Línea: ";
            string errorAnalisis = "Error de Cadena: El valor de la variable asignada no ha podido ser analizado. Línea: ";
            string errorAlmacenado = "Error de Cadena: La variable asignada no ha sido inicializada previamente. Línea: ";

            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].getValor().Contains("\"") && variables[i].getValor().Contains("+"))
                {
                    List<elementoToken> lista = new List<elementoToken>();

                    string codigo = variables[i].getValor();
                    var gramatica = new gramaticaConcatenacion();

                    var parser = new Parser(gramatica);
                    var arbol = parser.Parse(codigo);

                    if (arbol.Root == null)
                    {
                        for (int j = 0; j < arbol.ParserMessages.Count; j++)
                        {
                            errores.Add(errorAnalisis + variables[i].getLinea());
                        }
                    }
                    else
                    {
                        elementoToken auxiliar;

                        for (int j = 0; j < arbol.Tokens.Count - 1; j++)
                        {
                            auxiliar = new elementoToken();
                            auxiliar.setNombre(arbol.Tokens[j].Text);
                            auxiliar.setTipo(arbol.Tokens[j].ToString().Split('(')[1].Replace("(", "").Replace(")", "").Replace("}", ""));
                            auxiliar.setLinea(arbol.Tokens[j].Location.Line + 1);
                            lista.Add(auxiliar);
                        }

                        for (int j = 0; j < lista.Count; j++)
                        {

                            if (lista[j].getTipo().Equals("id"))
                            {
                                string tipo = "";
                                string valorAlmacenado = "";

                                for (int k = i - 1; k >= 0; k--)
                                {
                                    if (lista[j].getNombre().Equals(variables[k].getNombre()))
                                    {

                                        tipo = variables[k].getTipo();

                                        if (!variables[k].getValor().Equals(""))
                                        {
                                            valorAlmacenado = variables[k].getValor();
                                        }

                                    }
                                }

                                if (tipo.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }

                                if (valorAlmacenado.Equals(""))
                                {
                                    errores.Add(errorAlmacenado + variables[i].getLinea());
                                }
                            }
                        }

                    }
                }
            }

            return errores;
        }
    }
}
