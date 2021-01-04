using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class comprobacionScanner
    {
        public static List<string> comprobarScanner(List<elementoToken> tokens, List<elementoVariable> variables)
        {
            List<string> errores = new List<string>();
            bool scannerDeclarado = false;
            string auxiliarImportacion = "";

            string errorImportacion = "Error: La clase Scanner no ha sido importada previamente. Linea: ";
            string errorDeclaracion = "Error: El objeto Scanner no ha sido declarado previamente. Linea: ";
            string errorAsignacion = "Error: El valor asignado no pertenece al tipo esperado. Linea: ";

            #region Comprobar Declaracion Scanner

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].getNombre().Equals("import"))
                {
                    auxiliarImportacion = "import";

                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        auxiliarImportacion = auxiliarImportacion + " " + tokens[j].getNombre();

                        if (tokens[j].getNombre().Equals(";"))
                        {
                            i = j;
                            j = tokens.Count;
                        }
                    }

                    if (auxiliarImportacion.Equals("import java . util . Scanner ;"))
                    {
                        scannerDeclarado = true;
                    }
                }
            }

            #endregion

            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].getTipo().Equals("Scanner"))
                {
                    if (scannerDeclarado == false)
                    {
                        errores.Add(errorImportacion + variables[i].getLinea());
                    }
                }

                if (variables[i].getValor().Contains("next") && !variables[i].getValor().Contains("\""))
                {
                    string[] arregloAuxiliar = variables[i].getValor().Split('.');
                    string tipoVariable = variables[i].getTipo();
                    bool scannerEncontrado = false;

                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (variables[j].getNombre().Equals(arregloAuxiliar[0]) && variables[j].getTipo().Equals("Scanner"))
                        {
                            scannerEncontrado = true;
                        }

                        if (variables[j].getNombre().Equals(variables[i].getNombre()))
                        {
                            tipoVariable = variables[j].getTipo();
                        }
                    }

                    if (scannerEncontrado == false)
                    {
                        errores.Add(errorDeclaracion + variables[i].getLinea());
                    }

                    switch (tipoVariable)
                    {
                        case "int":
                            if (!arregloAuxiliar[1].Equals("nextInt()"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            break;

                        case "float":
                            if (!arregloAuxiliar[1].Equals("nextFloat()"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            break;

                        case "double":
                            if (!arregloAuxiliar[1].Equals("nextDouble()"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            break;

                        case "String":
                            if (!arregloAuxiliar[1].Equals("next()") && !arregloAuxiliar[1].Equals("nextLine()"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            break;

                        case "char":
                            if (!arregloAuxiliar[1].Equals("nextChar()"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            break;

                        case "boolean":
                            if (!arregloAuxiliar[1].Equals("nextBoolean()"))
                            {
                                errores.Add(errorAsignacion + variables[i].getLinea());
                            }
                            break;

                        default:
                            //Nothing
                            break;
                    }
                }
            }

            return errores;
        }
    }
}
