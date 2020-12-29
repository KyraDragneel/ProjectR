using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class deteccionVariables
    {
        public static List<elementoVariable> detectarVariables(List<elementoToken> tokens)
        {
            List<elementoVariable> variables = new List<elementoVariable>();
            List<elementoVariable> auxiliarVariables = new List<elementoVariable>();

            for (int i = ignorarInicio(tokens); i < tokens.Count; i++)
            {
                if (tokens[i].getNombre().Equals("int") || tokens[i].getNombre().Equals("float") || tokens[i].getNombre().Equals("double") || tokens[i].getNombre().Equals("String") || tokens[i].getNombre().Equals("char") || tokens[i].getNombre().Equals("boolean"))
                {
                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals(";"))
                        {
                            auxiliarVariables.Clear();
                            auxiliarVariables = evaluarDeclaracion(obtenerTokensLinea(tokens, i, j));

                            for (int k = 0; k < auxiliarVariables.Count; k++)
                            {
                                variables.Add(auxiliarVariables[k]);
                            }

                            i = j;
                            j = tokens.Count;
                        }
                    }
                }
                else if (tokens[i].getNombre().Equals("if") || tokens[i].getNombre().Equals("switch") || tokens[i].getNombre().Equals("while"))
                {
                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals(")"))
                        {
                            i = j;
                            j = tokens.Count;
                        }
                    }
                }
                else if (tokens[i].getNombre().Equals("for"))
                {
                    i = i + 2;

                    if (tokens[i].getTipo().Equals("id"))
                    {
                        for (int j = i; j < tokens.Count; j++)
                        {
                            if (tokens[j].getNombre().Equals(";"))
                            {
                                variables.Add(evaluarAsignacion(obtenerTokensLinea(tokens, i, j)));

                                i = j + 1;
                                j = tokens.Count;
                            }
                        }
                    }
                    else
                    {
                        for (int j = i; j < tokens.Count; j++)
                        {
                            if (tokens[j].getNombre().Equals(";"))
                            {
                                auxiliarVariables.Clear();
                                auxiliarVariables = evaluarDeclaracion(obtenerTokensLinea(tokens, i, j));

                                for (int k = 0; k < auxiliarVariables.Count; k++)
                                {
                                    variables.Add(auxiliarVariables[k]);
                                }

                                i = j + 1;
                                j = tokens.Count;
                            }
                        }
                    }
                    for (int j = i; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals(";"))
                        {
                            i = j + 1;
                            j = tokens.Count;
                        }
                    }
                    for (int j = i; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals(";"))
                        {
                            variables.Add(evaluarAsignacion(obtenerTokensLinea(tokens, i, j)));

                            i = j;
                            j = tokens.Count;
                        }
                    }
                }
                else if (tokens[i].getTipo().Equals("id"))
                {
                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals(";"))
                        {
                            variables.Add(evaluarAsignacion(obtenerTokensLinea(tokens, i, j)));

                            i = j;
                            j = tokens.Count;
                        }
                    }
                }
            }

            return variables;
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

        private static List<elementoVariable> evaluarDeclaracion(List<elementoToken> tokens)
        {
            List<elementoVariable> variables = new List<elementoVariable>();
            Queue<string> valoresGuardados = new Queue<string>();

            bool variablesMultiples = false;

            for (int i = 1; i < tokens.Count; i++)
            {
                if (Regex.IsMatch(tokens[i].getNombre(), "\"[^\"]*\""))
                {
                    valoresGuardados.Enqueue(tokens[i].getNombre());
                    tokens[i].setNombre("\"" + valoresGuardados.Count + "\"");
                }

                if (Regex.IsMatch(tokens[i].getNombre(), "\'[^\']\'"))
                {
                    valoresGuardados.Enqueue(tokens[i].getNombre());
                    tokens[i].setNombre("\'" + valoresGuardados.Count + "\'");
                }

                if (tokens[i].getNombre().Equals(","))
                {
                    variablesMultiples = true;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].getNombre().Contains("\"") || tokens[i].getNombre().Contains("\'"))
                {
                    tokens[i].setNombre(valoresGuardados.Dequeue());
                }
            }

            if (variablesMultiples == true)
            {
                int inicioFragmento = 1;
                int finalFragmento = 1;

                for (int i = 1; i < tokens.Count; i++)
                {
                    if (tokens[i].getNombre().Equals(",") || tokens[i].getNombre().Equals(";"))
                    {
                        finalFragmento = i;
                        variables.Add(generarVariableDeclaracion(tokens, inicioFragmento, finalFragmento));
                        inicioFragmento = i + 1;
                    }
                }
            }
            else
            {
                variables.Add(generarVariableDeclaracion(tokens, 0, tokens.Count - 1));
            }

            return variables;
        }

        private static elementoVariable evaluarAsignacion(List<elementoToken> tokens)
        {
            elementoVariable variable = new elementoVariable();
            string valorGuardado = "";

            for (int i = 1; i < tokens.Count; i++)
            {
                if (Regex.IsMatch(tokens[i].getNombre(), "\"[^\"]*\""))
                {
                    valorGuardado = tokens[i].getNombre();
                    tokens[i].setNombre("\"" + 1 + "\"");
                }

                if (Regex.IsMatch(tokens[i].getNombre(), "\'[^\']\'"))
                {
                    valorGuardado = tokens[i].getNombre();
                    tokens[i].setNombre("\'" + 1 + "\'");
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].getNombre().Contains("\"") || tokens[i].getNombre().Contains("\'"))
                {
                    tokens[i].setNombre(valorGuardado);
                }
            }

            variable = generarVariableAsignacion(tokens);

            return variable;
        }

        private static elementoVariable generarVariableDeclaracion(List<elementoToken> tokens, int inicio, int final)
        {
            elementoVariable variable = new elementoVariable();
            string tipoEsperado = tokens[0].getNombre();
            List<string> ids = new List<string>();

            int contadorCorchetes = 0;
            bool contieneAsignacion = false;
            int posicionAsignacion = 0;

            for (int i = inicio; i < final; i++)
            {
                if (tokens[i].getNombre().Equals("["))
                {
                    contadorCorchetes++;
                }

                if (tokens[i].getTipo().Equals("id"))
                {
                    ids.Add(tokens[i].getNombre());
                }

                if (tokens[i].getNombre().Equals("="))
                {
                    contieneAsignacion = true;
                    posicionAsignacion = i;
                    i = final;
                }

            }

            if (contadorCorchetes == 2)
            {
                if (contieneAsignacion == true)
                {
                    string auxiliarValor = "";

                    for (int i = posicionAsignacion + 1; i < final; i++)
                    {
                        auxiliarValor = auxiliarValor + " " + tokens[i].getNombre();
                    }

                    if (auxiliarValor[0] == ' ')
                    {
                        auxiliarValor = auxiliarValor.Substring(1);
                    }

                    if (!auxiliarValor.Contains("\""))
                    {
                        auxiliarValor = auxiliarValor.Replace(" ", "");
                    }

                    variable = new elementoVariable(tipoEsperado + "[][]", ids[0].Replace(" ", ""), auxiliarValor, tokens[0].getLinea());
                }
                else
                {
                    variable = new elementoVariable(tipoEsperado + "[][]", ids[0].Replace(" ", ""), "", tokens[0].getLinea());
                }
            }
            else if (contadorCorchetes == 1)
            {
                if (contieneAsignacion == true)
                {
                    string auxiliarValor = "";

                    for (int i = posicionAsignacion + 1; i < final; i++)
                    {
                        auxiliarValor = auxiliarValor + " " + tokens[i].getNombre();
                    }

                    if (auxiliarValor[0] == ' ')
                    {
                        auxiliarValor = auxiliarValor.Substring(1);
                    }

                    if (!auxiliarValor.Contains("\""))
                    {
                        auxiliarValor = auxiliarValor.Replace(" ", "");
                    }

                    variable = new elementoVariable(tipoEsperado + "[]", ids[0].Replace(" ", ""), auxiliarValor, tokens[0].getLinea());
                }
                else
                {
                    variable = new elementoVariable(tipoEsperado + "[]", ids[0].Replace(" ", ""), "", tokens[0].getLinea());
                }
            }
            else
            {
                if (contieneAsignacion == true)
                {
                    string auxiliarValor = "";

                    for (int i = posicionAsignacion + 1; i < final; i++)
                    {
                        auxiliarValor = auxiliarValor + " " + tokens[i].getNombre();
                    }

                    if (auxiliarValor[0] == ' ')
                    {
                        auxiliarValor = auxiliarValor.Substring(1);
                    }

                    if (!auxiliarValor.Contains("\""))
                    {
                        auxiliarValor = auxiliarValor.Replace(" ", "");
                    }

                    variable = new elementoVariable(tipoEsperado, ids[0].Replace(" ", ""), auxiliarValor, tokens[0].getLinea());
                }
                else
                {
                    variable = new elementoVariable(tipoEsperado, ids[0].Replace(" ", ""), "", tokens[0].getLinea());
                }
            }

            return variable;
        }

        private static elementoVariable generarVariableAsignacion(List<elementoToken> tokens)
        {
            elementoVariable variable = new elementoVariable();

            string auxiliarValor = "";
            string auxiliarNombre = "";
            int posicionAsignacion = 0;
            bool asignacionEspecial = false;
            bool operacionSimple = false;
            string fragmentoOperacion = "";

            for (int i = 0; i < tokens.Count; i++)
            {

                if (tokens[i].getNombre().Equals("="))
                {
                    posicionAsignacion = i;
                }

                if (tokens[i].getNombre().Equals("+=") || tokens[i].getNombre().Equals("-=") || tokens[i].getNombre().Equals("*=") || tokens[i].getNombre().Equals("/=") || tokens[i].getNombre().Equals("%="))
                {
                    posicionAsignacion = i;
                    asignacionEspecial = true;
                    fragmentoOperacion = tokens[i].getNombre().Substring(0, 1);
                }

                if (tokens[i].getNombre().Equals("++") || tokens[i].getNombre().Equals("--"))
                {
                    posicionAsignacion = i;
                    operacionSimple = true;
                    fragmentoOperacion = tokens[i].getNombre().Substring(0, 1);
                }

                if (posicionAsignacion != 0)
                {
                    i = tokens.Count;
                }

            }

            if (asignacionEspecial == true)
            {
                for (int i = 0; i < posicionAsignacion; i++)
                {
                    auxiliarNombre = auxiliarNombre + " " + tokens[i].getNombre();
                }

                auxiliarValor = auxiliarNombre + " " + fragmentoOperacion;

                for (int i = posicionAsignacion + 1; i < tokens.Count - 1; i++)
                {
                    auxiliarValor = auxiliarValor + " " + tokens[i].getNombre();
                }
            }
            else if (operacionSimple == true)
            {
                for (int i = 0; i < posicionAsignacion; i++)
                {
                    auxiliarNombre = auxiliarNombre + " " + tokens[i].getNombre();
                }

                auxiliarValor = auxiliarNombre + " " + fragmentoOperacion + " 1";
            }
            else
            {
                for (int i = 0; i < posicionAsignacion; i++)
                {
                    auxiliarNombre = auxiliarNombre + " " + tokens[i].getNombre();
                }

                for (int i = posicionAsignacion + 1; i < tokens.Count - 1; i++)
                {
                    auxiliarValor = auxiliarValor + " " + tokens[i].getNombre();
                }
            }

            if (auxiliarValor[0] == ' ')
            {
                auxiliarValor = auxiliarValor.Substring(1);
            }

            if (!auxiliarValor.Contains("\""))
            {
                auxiliarValor = auxiliarValor.Replace(" ", "");
            }

            variable = new elementoVariable("", auxiliarNombre.Replace(" ", ""), auxiliarValor, tokens[0].getLinea());

            return variable;
        }
    }
}
