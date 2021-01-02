using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class validarValoresVariables
    {
        public static List<string> validarValores(List<elementoVariable> variables)
        {
            List<string> errores = new List<string>();
            string auxiliarTipo;
            string errorAsignacion = "Error: El valor asignado no pertenece al tipo esperado. Linea: ";
            string errorDeclaracion = "Error: El valor de la variable asignada no ha sido declarado previamente. Linea: ";
            int residuo = 0;
            float residuoF = 0;
            double residuoD = 0;

            for (int i = 0; i < variables.Count; i++)
            {
                if (!variables[i].getValor().Equals(""))
                {
                    auxiliarTipo = variables[i].getTipo();

                    if (auxiliarTipo.Equals(""))
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (variables[i].getNombre().Equals(variables[j].getNombre()))
                            {
                                auxiliarTipo = variables[j].getTipo();
                            }
                        }
                    }

                    switch (auxiliarTipo)
                    {
                        case "int":
                            //Pendiente expresion con variables
                            #region Int

                            if (!Int32.TryParse(variables[i].getValor(), out residuo) && !Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\""))
                            {
                                if (float.TryParse(variables[i].getValor(), out residuoF) || double.TryParse(variables[i].getValor(), out residuoD))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                                else if (!Int32.TryParse(evaluarExpresion(variables[i].getValor().Replace(" ", "")), out residuo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else if (!Int32.TryParse(variables[i].getValor(), out residuo) && Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !Regex.IsMatch(variables[i].getValor(), "\'[^\']\'") && !variables[i].getValor().Equals("true") && !variables[i].getValor().Equals("false"))
                            {

                                if (variables[i].getValor().Contains("(") || variables[i].getValor().Contains(")") || variables[i].getValor().Contains("+") || variables[i].getValor().Contains("-") || variables[i].getValor().Contains("*") || variables[i].getValor().Contains("/"))
                                {
                                    //Console.WriteLine("Es una expresion con variables: " + variables[i].getLinea());
                                }
                                else
                                {

                                    string tipoBusqueda = "";

                                    for (int j = i - 1; j >= 0; j--)
                                    {
                                        if (variables[i].getValor().Equals(variables[j].getNombre()))
                                        {
                                            tipoBusqueda = variables[j].getTipo();
                                        }
                                    }

                                    if (tipoBusqueda.Equals(""))
                                    {
                                        errores.Add(errorDeclaracion + variables[i].getLinea());
                                    }
                                    else if (!tipoBusqueda.Equals(auxiliarTipo))
                                    {
                                        errores.Add(errorAsignacion + variables[i].getLinea());
                                    }
                                }

                            }
                            else if (!Int32.TryParse(variables[i].getValor(), out residuo) && variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !variables[i].getValor().Contains("+") && !variables[i].getValor().Contains("-") && !variables[i].getValor().Contains("*") && !variables[i].getValor().Contains("/") && !variables[i].getValor().Contains("%"))
                            {
                                
                                Console.WriteLine("Lo envie desde aqui: "+variables[i].getLinea());
                                string auxiliarValor = variables[i].getValor().Split('[')[0].Replace(" ", "");

                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (auxiliarValor.Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else if (!Int32.TryParse(variables[i].getValor(), out residuo) && (Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") || Regex.IsMatch(variables[i].getValor(), "\'[^\']\'")))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            else if (variables[i].getValor().Equals("true") || variables[i].getValor().Equals("false"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }

                            #endregion

                            break;

                        case "String":
                            //Queda pendiente si contiene expresiones aritmeticas
                            //Pendiente concatenacion de dos variables
                            #region String
                            if (!Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("["))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            else if (Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !variables[i].getValor().Contains("["))
                            {
                                if (variables[i].getValor().Equals("true") || variables[i].getValor().Equals("false"))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                                else
                                {
                                    string tipoBusqueda = "";

                                    for (int j = i - 1; j >= 0; j--)
                                    {
                                        if (variables[i].getValor().Equals(variables[j].getNombre()))
                                        {
                                            tipoBusqueda = variables[j].getTipo();
                                        }
                                    }

                                    if (tipoBusqueda.Equals(""))
                                    {
                                        errores.Add(errorDeclaracion + variables[i].getLinea());
                                    }
                                    else if (!tipoBusqueda.Equals(auxiliarTipo))
                                    {
                                        errores.Add(errorAsignacion + variables[i].getLinea());
                                    }
                                }
                            }
                            else if (variables[i].getValor().Contains("[") && !variables[i].getValor().Contains("\""))
                            {
                                Console.WriteLine("entre aqui");
                                //Queda pendiente si el espacio existe en el arreglo
                                string auxiliarValor = variables[i].getValor().Split('[')[0].Replace(" ", "");

                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (auxiliarValor.Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else
                            {
                                /*
                                Console.WriteLine("Entre en este caso");
                                string cadena = variables[i].getValor();
                                int contador = 0;
                                string[] variablesContenidas;
                                int posicion1 = 0;
                                int posicion2 = 0;

                                for (int j = 0; j < cadena.Length; j++)
                                {
                                    if (cadena[j] == '"')
                                    {
                                        contador++;
                                    }

                                    if (cadena[j] == '\\' && cadena[j + 1] == '"')
                                    {
                                        contador--;
                                    }
                                }

                                if (contador > 2)
                                {

                                    contador = 0;

                                    for (int j = 0; j < cadena.Length; j++)
                                    {
                                        if (contador == 1 && cadena[j] == '"')
                                        {
                                            contador++;
                                            posicion2 = j;
                                        }

                                        if (contador == 0 && cadena[j] == '"')
                                        {
                                            contador++;
                                            posicion1 = j;
                                        }

                                        if (contador == 2)
                                        {
                                            contador = 0;
                                            cadena = cadena.Substring(0, posicion1) + cadena.Substring(posicion2 + 1, cadena.Length - (posicion2 + 1));
                                        }
                                    }

                                    cadena = cadena.Replace(" ", "");

                                    variablesContenidas = cadena.Split('+');

                                    for (int j = 0; j < variablesContenidas.Length; j++)
                                    {
                                        if (!variablesContenidas[j].Equals(""))
                                        {
                                            if (!Regex.IsMatch(variablesContenidas[j], "\"[^\"]*\"") && !Regex.IsMatch(variablesContenidas[j], "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variablesContenidas[j].Contains("["))
                                            {
                                                errores.Add(errorAsignacion + variables[i].getLinea());
                                            }
                                            else if (Regex.IsMatch(variablesContenidas[j], "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !Regex.IsMatch(variablesContenidas[j], "\"[^\"]*\"") && !variablesContenidas[j].Contains("["))
                                            {
                                                string tipoBusqueda = "";

                                                for (int k = i - 1; k >= 0; k--)
                                                {
                                                    if (variablesContenidas[j].Equals(variables[k].getNombre()))
                                                    {
                                                        tipoBusqueda = variables[j].getTipo();
                                                    }
                                                }

                                                if (tipoBusqueda.Equals(""))
                                                {
                                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                                }
                                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                                {
                                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                                }
                                            }
                                            else if (variablesContenidas[j].Contains("["))
                                            {
                                                //Queda pendiente si el espacio existe en el arreglo
                                                string auxiliarValor = variablesContenidas[j].Split('[')[0].Replace(" ", "");

                                                string tipoBusqueda = "";

                                                for (int k = i - 1; k >= 0; k--)
                                                {
                                                    if (auxiliarValor.Equals(variables[k].getNombre()))
                                                    {
                                                        tipoBusqueda = variables[j].getTipo();
                                                    }
                                                }

                                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                                if (tipoBusqueda.Equals(""))
                                                {
                                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                                }
                                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                                {
                                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                                }
                                            }
                                        }
                                    }

                                }
                                else if (cadena[cadena.Length - 1] != '"' || cadena[0] != '"')
                                {

                                    contador = 0;

                                    for (int j = 0; j < cadena.Length; j++)
                                    {
                                        if (contador == 1 && cadena[j] == '"')
                                        {
                                            contador++;
                                            posicion2 = j;
                                        }

                                        if (contador == 0 && cadena[j] == '"')
                                        {
                                            contador++;
                                            posicion1 = j;
                                        }

                                        if (contador == 2)
                                        {
                                            contador = 0;
                                            cadena = cadena.Substring(0, posicion1) + cadena.Substring(posicion2 + 1, cadena.Length - (posicion2 + 1));
                                        }
                                    }

                                    cadena = cadena.Replace(" ", "");

                                    variablesContenidas = cadena.Split('+');

                                    for (int j = 0; j < variablesContenidas.Length; j++)
                                    {
                                        if (!variablesContenidas[j].Equals(""))
                                        {
                                            if (!Regex.IsMatch(variablesContenidas[j], "\"[^\"]*\"") && !Regex.IsMatch(variablesContenidas[j], "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variablesContenidas[j].Contains("["))
                                            {
                                                errores.Add(errorAsignacion + variables[i].getLinea());
                                            }
                                            else if (Regex.IsMatch(variablesContenidas[j], "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !Regex.IsMatch(variablesContenidas[j], "\"[^\"]*\"") && !variablesContenidas[j].Contains("["))
                                            {
                                                string tipoBusqueda = "";

                                                for (int k = i - 1; k >= 0; k--)
                                                {
                                                    if (variablesContenidas[j].Equals(variables[k].getNombre()))
                                                    {
                                                        tipoBusqueda = variables[j].getTipo();
                                                    }
                                                }

                                                if (tipoBusqueda.Equals(""))
                                                {
                                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                                }
                                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                                {
                                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                                }
                                            }
                                            else if (variablesContenidas[j].Contains("["))
                                            {
                                                //Queda pendiente si el espacio existe en el arreglo
                                                string auxiliarValor = variablesContenidas[j].Split('[')[0].Replace(" ", "");

                                                string tipoBusqueda = "";

                                                for (int k = i - 1; k >= 0; k--)
                                                {
                                                    if (auxiliarValor.Equals(variables[k].getNombre()))
                                                    {
                                                        tipoBusqueda = variables[j].getTipo();
                                                    }
                                                }

                                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                                if (tipoBusqueda.Equals(""))
                                                {
                                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                                }
                                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                                {
                                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Nothing
                                }*/
                            }

                            #endregion

                            break;

                        case "float":
                            //Pendiente expresion con variables
                            #region Float

                            if (!float.TryParse(variables[i].getValor(), out residuoF) && !Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\""))
                            {
                                //Nothing
                            }
                            else if (!float.TryParse(variables[i].getValor(), out residuoF) && Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !Regex.IsMatch(variables[i].getValor(), "\'[^\']\'") && !variables[i].getValor().Equals("true") && !variables[i].getValor().Equals("false"))
                            {

                                if (variables[i].getValor().Contains("(") || variables[i].getValor().Contains(")") || variables[i].getValor().Contains("+") || variables[i].getValor().Contains("-") || variables[i].getValor().Contains("*") || variables[i].getValor().Contains("/"))
                                {
                                    //Console.WriteLine("Es una expresion con variables: " + variables[i].getLinea());
                                }
                                else
                                {

                                    string tipoBusqueda = "";

                                    for (int j = i - 1; j >= 0; j--)
                                    {
                                        if (variables[i].getValor().Equals(variables[j].getNombre()))
                                        {
                                            tipoBusqueda = variables[j].getTipo();
                                        }
                                    }

                                    if (tipoBusqueda.Equals(""))
                                    {
                                        errores.Add(errorDeclaracion + variables[i].getLinea());
                                    }
                                    else if (!tipoBusqueda.Equals(auxiliarTipo))
                                    {
                                        errores.Add(errorAsignacion + variables[i].getLinea());
                                    }
                                }

                            }
                            else if (!float.TryParse(variables[i].getValor(), out residuoF) && variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !variables[i].getValor().Contains("+") && !variables[i].getValor().Contains("-") && !variables[i].getValor().Contains("*") && !variables[i].getValor().Contains("/") && !variables[i].getValor().Contains("%"))
                            {

                                string auxiliarValor = variables[i].getValor().Split('[')[0].Replace(" ", "");

                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (auxiliarValor.Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else if (!float.TryParse(variables[i].getValor(), out residuoF) && (Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") || Regex.IsMatch(variables[i].getValor(), "\'[^\']\'")))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            else if (variables[i].getValor().Equals("true") || variables[i].getValor().Equals("false"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }

                            #endregion

                            break;

                        case "double":

                            //Pendiente expresion con variables
                            #region Double

                            if (!double.TryParse(variables[i].getValor(), out residuoD) && !Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\""))
                            {
                                //Nothing
                            }
                            else if (!double.TryParse(variables[i].getValor(), out residuoD) && Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !Regex.IsMatch(variables[i].getValor(), "\'[^\']\'") && !variables[i].getValor().Equals("true") && !variables[i].getValor().Equals("false"))
                            {

                                if (variables[i].getValor().Contains("(") || variables[i].getValor().Contains(")") || variables[i].getValor().Contains("+") || variables[i].getValor().Contains("-") || variables[i].getValor().Contains("*") || variables[i].getValor().Contains("/"))
                                {
                                    //Console.WriteLine("Es una expresion con variables: " + variables[i].getLinea());
                                }
                                else
                                {

                                    string tipoBusqueda = "";

                                    for (int j = i - 1; j >= 0; j--)
                                    {
                                        if (variables[i].getValor().Equals(variables[j].getNombre()))
                                        {
                                            tipoBusqueda = variables[j].getTipo();
                                        }
                                    }

                                    if (tipoBusqueda.Equals(""))
                                    {
                                        errores.Add(errorDeclaracion + variables[i].getLinea());
                                    }
                                    else if (!tipoBusqueda.Equals(auxiliarTipo))
                                    {
                                        errores.Add(errorAsignacion + variables[i].getLinea());
                                    }
                                }

                            }
                            else if (!double.TryParse(variables[i].getValor(), out residuoD) && variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !variables[i].getValor().Contains("+") && !variables[i].getValor().Contains("-") && !variables[i].getValor().Contains("*") && !variables[i].getValor().Contains("/") && !variables[i].getValor().Contains("%"))
                            {

                                string auxiliarValor = variables[i].getValor().Split('[')[0].Replace(" ", "");

                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (auxiliarValor.Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else if (!double.TryParse(variables[i].getValor(), out residuoD) && (Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") || Regex.IsMatch(variables[i].getValor(), "\'[^\']\'")))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            else if (variables[i].getValor().Equals("true") || variables[i].getValor().Equals("false"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }

                            #endregion

                            break;

                        case "boolean":

                            #region Boolean

                            if (!variables[i].getValor().Equals("true") && !variables[i].getValor().Equals("false") && !Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\""))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            else if (!variables[i].getValor().Equals("true") && !variables[i].getValor().Equals("false") && Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\""))
                            {
                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (variables[i].getValor().Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else if (variables[i].getValor().Contains("[") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\""))
                            {
                                string auxiliarValor = variables[i].getValor().Split('[')[0].Replace(" ", "");

                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (auxiliarValor.Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else if (Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\""))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            else
                            {
                                //Nothing
                            }

                            break;

                        #endregion

                        case "char":

                            #region Char

                            if (Regex.IsMatch(variables[i].getValor(), "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*") && !Regex.IsMatch(variables[i].getValor(), "\'[^\']\'") && !Regex.IsMatch(variables[i].getValor(), "\"[^\"]*\"") && !variables[i].getValor().Equals("true") && !variables[i].getValor().Equals("false") && !variables[i].getValor().Contains("["))
                            {
                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (variables[i].getValor().Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else if (variables[i].getValor().Contains("["))
                            {
                                string auxiliarValor = variables[i].getValor().Split('[')[0].Replace(" ", "");
                                string tipoBusqueda = "";

                                for (int j = i - 1; j >= 0; j--)
                                {
                                    if (auxiliarValor.Equals(variables[j].getNombre()))
                                    {
                                        tipoBusqueda = variables[j].getTipo();
                                    }
                                }

                                tipoBusqueda = tipoBusqueda.Split('[')[0].Replace(" ", "");

                                if (tipoBusqueda.Equals(""))
                                {
                                    errores.Add(errorDeclaracion + variables[i].getLinea());
                                }
                                else if (!tipoBusqueda.Equals(auxiliarTipo))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }
                            else
                            {
                                if (Regex.IsMatch(variables[i].getValor(), "\'[^\']\'"))
                                {
                                    //Nothing
                                }
                                else
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }

                            #endregion

                            break;

                        default:

                            #region Arreglos

                            if (variables[i].getTipo().Contains("[") && variables[i].getValor().Contains("["))
                            {
                                string tipoDeclarado = variables[i].getTipo().Split('[')[0].Replace(" ", "");
                                string tipoAsignado = variables[i].getValor().Split('[')[0].Replace("new", "").Replace(" ", "");

                                if (!tipoDeclarado.Equals(tipoAsignado))
                                {
                                    errores.Add(errorAsignacion + variables[i].getLinea());
                                }
                            }

                            #endregion

                            break;
                    }

                }
            }

            return errores;
        }

        public static string evaluarExpresion(string expression)
        {
            VsaEngine engine = VsaEngine.CreateEngine();
            try
            {
                object o = Eval.JScriptEvaluate(expression, engine);
                return System.Convert.ToDouble(o).ToString();
            }
            catch
            {
                return "Error";
            }
            engine.Close();
        }
    }
}
