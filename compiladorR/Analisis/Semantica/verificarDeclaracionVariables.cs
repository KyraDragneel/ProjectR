using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class verificarDeclaracionVariables
    {
        public static List<string> verificarDeclaracion(List<elementoVariable> variables)
        {
            //Lista de errores que regresara
            List<string> errores = new List<string>();
            string errorMultiple = "Error de Variable: La variable ya ha sido declarada previamente. Línea: ";
            string errorDeclaracion = "Error de Variable: La variable no ha sido declarada previamente. Línea: ";

            //Ciclo que recorrera toda la lista de variables
            for (int i = 0; i < variables.Count; i++)
            {
                //Verifica si el tipo de la variable contiene algo y si esto no es un arreglo
                if (!variables[i].getTipo().Equals("") && !variables[i].getTipo().Contains("["))
                {
                    //Con esto verificaremos que solo una vez se imprima en caso de repeticion
                    bool repeticion = false;

                    //Ciclo que recorrera la lista de variables de manera invertida, buscando cualquier tipo de duplicado en la declaracion
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //Verifica si el nombre de la variable que se esta evaluando actualemente es igual a alguna de las que se encuentran anteriormente en la lista
                        if (variables[i].getNombre().Equals(variables[j].getNombre()))
                        {
                            //Verifica si la variable encontrada tiene asignado un tipo, si asi es marca que se encontro una multiple declaracion
                            if (!variables[j].getTipo().Equals(""))
                            {
                                repeticion = true;
                            }
                        }
                    }

                    //Verifica que si se encontro una multiple declaracion
                    if (repeticion == true)
                    {
                        //Agrega el error a la lista de errores con la respectiva linea donde se encontro
                        errores.Add(errorMultiple + variables[i].getLinea());
                    }
                }
                //Verifica si el texto de la variable no contiene nada, osea que es una asignacion
                else if (variables[i].getTipo().Equals("") && !variables[i].getNombre().Contains("["))
                {
                    //Con esto verificamos si fue declarada aunque sea una vez
                    bool declarado = false;

                    //Ciclo que recorrera la lista de variables de manera invertida, buscando su previa declaracion
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //Verifica si el nombre de la variable que se esta evaluando actualemente es igual a alguna de las que se encuentran anteriormente en la lista
                        if (variables[i].getNombre().Equals(variables[j].getNombre()))
                        {
                            //Verifica si la variable encontrada tiene asignado un tipo, si asi es marca que se declaro previamente
                            if (!variables[j].getTipo().Equals(""))
                            {
                                declarado = true;
                            }
                        }
                    }

                    //Verifica si no estaba declarada previamente
                    if (declarado == false)
                    {
                        //Agrega el error a la lista de errores con la respectiva linea donde se encontro
                        errores.Add(errorDeclaracion + variables[i].getLinea());
                    }
                }
            }

            //Se retorna la lista de errores
            return errores;
        }
    }
}
