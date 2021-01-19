using compiladorR.Analisis.Gramaticas;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class comprobacionExpresion
    {
        public static List<string> comprobarExpresion(List<elementoVariable> variables)
        {
            List<string> errores = new List<string>();
            string errorDeclaracion = "Error de Expresión: El valor de la variable asignada no ha sido declarado previamente. Línea: ";
            string errorAsignacion = "Error de Expresión: El valor asignado no pertenece al tipo esperado. Línea: ";
            string errorAnalisis = "Error de Expresión: El valor de la variable asignada no ha podido ser analizado. Línea: ";
            string errorAlmacenado = "Error de Expresión: La variable asignada no ha sido inicializada. Línea: ";

            for (int i = 0; i < variables.Count; i++)
            {
                if (!variables[i].getNombre().Contains("[") && !variables[i].getValor().Contains("[") && !variables[i].getValor().Contains("\"") && (variables[i].getValor().Contains("+") || variables[i].getValor().Contains("-") || variables[i].getValor().Contains("*") || variables[i].getValor().Contains("/") || variables[i].getValor().Contains("%")))
                {
                    List<elementoToken> lista = new List<elementoToken>();

                    string codigo = variables[i].getValor();
                    var gramatica = new gramaticaExpresion();

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
                                string tipoActual = variables[i].getTipo();
                                string valorAlmacenado = "";

                                for (int k = i - 1; k >= 0; k--)
                                {
                                    if (variables[i].getNombre().Equals(variables[k].getNombre()))
                                    {
                                        if (!variables[k].getTipo().Equals(""))
                                        {
                                            tipoActual = variables[k].getTipo();
                                        }
                                    }
                                }

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
                                else if (!tipoActual.Replace("[", "").Replace("]", "").Equals(tipo.Replace("[", "").Replace("]", "")))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
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
