using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class verificarDeclaracionArreglos
    {
        public static List<string> verificarDeclaracion(List<elementoVariable> variables)
        {
            //Lista de errores que regresara
            List<string> errores = new List<string>();
            string errorMultiple = "Error de Arreglos: La variable ya ha sido declarada previamente. Línea: ";
            string errorDeclaracion = "Error de Arreglos: La variable no ha sido declarada previamente. Línea: ";
            string errorIndice = "Error de Arreglos: Se intenta almacenar un valor fuera del índice establecido. Línea: ";

            for (int i = 0; i < variables.Count; i++)
            {
                if (!variables[i].getTipo().Equals("") && variables[i].getTipo().Contains("["))
                {
                    bool repeticion = false;

                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (variables[i].getNombre().Equals(variables[j].getNombre()))
                        {
                            if (!variables[j].getTipo().Equals(""))
                            {
                                repeticion = true;
                            }
                        }
                    }

                    if (repeticion == true)
                    {
                        errores.Add(errorMultiple + variables[i].getLinea());
                    }
                }
                else if (variables[i].getTipo().Equals("") && (variables[i].getNombre().Contains("[") || variables[i].getValor().Contains("new")))
                {
                    int dimensiones = 0;
                    int posicionCorte = 0;
                    int posicionCorte2 = 0;

                    bool declarado = false;

                    for (int j = 0; j < variables[i].getNombre().Length; j++)
                    {
                        if (variables[i].getNombre()[j].ToString().Equals("["))
                        {
                            if (dimensiones == 0)
                            {
                                posicionCorte = j;
                            }
                            if (dimensiones == 1)
                            {
                                posicionCorte2 = j;
                            }
                            dimensiones++;
                        }
                    }

                    if (posicionCorte == 0)
                    {
                        posicionCorte = variables[i].getNombre().Length;
                    }

                    if (dimensiones == 2)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (variables[i].getNombre().Substring(0, posicionCorte).Equals(variables[j].getNombre()))
                            {
                                if (!variables[j].getTipo().Equals(""))
                                {
                                    declarado = true;
                                }
                            }
                        }

                        if (declarado == false)
                        {
                            errores.Add(errorDeclaracion + variables[i].getLinea());
                        }
                        /*
                        for(int j = i - 1; j >= 0; j--)
                        {
                            if(variables[i].getNombre().Substring(0, posicionCorte).Equals(variables[j].getNombre()))
                            {
                                string primerDigito = "";

                                for(int k = 0; k < variables[j].getValor().Length; k++)
                                {
                                    if(variables[j].getValor()[k].ToString().Equals("["))
                                    {
                                        primerDigito = variables[j].getValor()[k + 1].ToString();
                                    }
                                }

                                int digitoInsertado = Int16.Parse(variables[i].getNombre()[posicionCorte + 1].ToString());
                                int digitoTamaño = Int16.Parse(primerDigito);

                                if(digitoInsertado < 0 || digitoInsertado >= digitoTamaño)
                                {
                                    errores.Add(errorIndice + variables[i].getLinea());
                                }

                                j = -1;
                            }
                        }*/
                    }
                    else if (dimensiones == 1)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (variables[i].getNombre().Substring(0, posicionCorte).Equals(variables[j].getNombre()))
                            {
                                if (!variables[j].getTipo().Equals(""))
                                {
                                    declarado = true;
                                }
                            }
                        }

                        if (declarado == false)
                        {
                            errores.Add(errorDeclaracion + variables[i].getLinea());
                        }

                        /*
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (variables[i].getNombre().Substring(0, posicionCorte).Equals(variables[j].getNombre()))
                            {
                                string primerDigito = "0";
                                string segundoDigito = "0";

                                for (int k = 0; k < variables[j].getValor().Length; k++)
                                {
                                    if (variables[j].getValor()[k].ToString().Equals("[") && primerDigito.Equals(""))
                                    {
                                        primerDigito = variables[j].getValor()[k + 1].ToString();
                                    }

                                    if(variables[j].getValor()[k].ToString().Equals("[") && !primerDigito.Equals(""))
                                    {
                                        segundoDigito = variables[j].getValor()[k + 1].ToString();
                                    }
                                }

                                int digitoInsertado = Int16.Parse(variables[i].getNombre()[posicionCorte + 1].ToString());
                                int digitoTamaño = Int16.Parse(primerDigito);
                                int digitoInsertado2 = Int16.Parse(variables[i].getNombre()[posicionCorte + 5].ToString());
                                int digitoTamaño2 = Int16.Parse(segundoDigito);

                                if (digitoInsertado < 0 || digitoInsertado >= digitoTamaño || digitoInsertado2 < 0 || digitoInsertado2 >= digitoTamaño2)
                                {
                                    errores.Add(errorIndice + variables[i].getLinea());
                                }

                                j = -1;
                            }
                        }*/
                    }
                }
            }

            return errores;
        }
    }
}
