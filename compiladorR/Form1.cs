using compiladorR.Analisis.Gramaticas;
using compiladorR.Analisis.Semantica;
using compiladorR.Compilacion;
using compiladorR.Compilacion.ModelosDatos;
using Irony.Parsing;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compiladorR
{
    public partial class Form1 : Form
    {

        List<tipoBoolean> variablesBoolean = new List<tipoBoolean>();
        List<tipoChar> variablesChar = new List<tipoChar>();
        List<tipoDouble> variablesDouble = new List<tipoDouble>();
        List<tipoFloat> variablesFloat = new List<tipoFloat>();
        List<tipoInt> variablesInt = new List<tipoInt>();
        List<tipoString> variablesString = new List<tipoString>();

        public Form1()
        {
            InitializeComponent();
        }

        private void run_Click(object sender, EventArgs e)
        {
            areaResultado.Clear();
            List<elementoToken> lista = new List<elementoToken>();
            List<elementoVariable> variables = new List<elementoVariable>();
            List<string> listaErrores = new List<string>();
            List<string> auxiliarErrores = new List<string>();

            string codigo = entrada.Text;
            var gramatica = new gramaticaJava();

            var parser = new Parser(gramatica);
            var arbol = parser.Parse(codigo);

            if (arbol.Root == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    Console.WriteLine(arbol.ParserMessages[i].Message);
                }
                areaResultado.AppendText("Por favor revise su codigo" + "\n");
            }
            else
            {
                elementoToken auxiliar;

                for (int i = 0; i < arbol.Tokens.Count - 1; i++)
                {
                    auxiliar = new elementoToken();
                    auxiliar.setNombre(arbol.Tokens[i].Text);
                    auxiliar.setTipo(arbol.Tokens[i].ToString().Split('(')[1].Replace("(", "").Replace(")", "").Replace("}", ""));
                    auxiliar.setLinea(arbol.Tokens[i].Location.Line + 1);
                    lista.Add(auxiliar);
                }

                variables = deteccionVariables.detectarVariables(lista,true);

                /*
                for (int i = 0; i < variables.Count; i++)
                {
                    Console.WriteLine((i + 1) + "- Tipo: " + variables[i].getTipo() + " Nombre: " + variables[i].getNombre() + " Valor: " + variables[i].getValor() + " Linea: " + variables[i].getLinea());
                }*/

                auxiliarErrores = verificarDeclaracionVariables.verificarDeclaracion(variables);

                for (int i = 0; i < auxiliarErrores.Count; i++)
                {
                    listaErrores.Add(auxiliarErrores[i]);
                }

                auxiliarErrores.Clear();

                auxiliarErrores = verificarDeclaracionArreglos.verificarDeclaracion(variables);

                for (int i = 0; i < auxiliarErrores.Count; i++)
                {
                    listaErrores.Add(auxiliarErrores[i]);
                }

                auxiliarErrores.Clear();

                auxiliarErrores = validarValoresVariables.validarValores(variables);

                for (int i = 0; i < auxiliarErrores.Count; i++)
                {
                    listaErrores.Add(auxiliarErrores[i]);
                }

                auxiliarErrores.Clear();

                auxiliarErrores = comprobacionConcatenacion.comprobarConcatenacion(variables);
                
                for (int i = 0; i < auxiliarErrores.Count; i++)
                {
                    listaErrores.Add(auxiliarErrores[i]);
                }

                auxiliarErrores.Clear();

                auxiliarErrores = comprobacionExpresion.comprobarExpresion(variables);

                for (int i = 0; i < auxiliarErrores.Count; i++)
                {
                    listaErrores.Add(auxiliarErrores[i]);
                }

                auxiliarErrores.Clear();

                auxiliarErrores = comprobacionScanner.comprobarScanner(lista, variables);

                for (int i = 0; i < auxiliarErrores.Count; i++)
                {
                    listaErrores.Add(auxiliarErrores[i]);
                }

                auxiliarErrores.Clear();

                if (listaErrores.Count == 0)
                {
                    try
                    {
                        limpiarMemoria();
                        compilarInstrucciones(lista);

                        Console.WriteLine("Variables Int");
                        for(int i = 0; i < variablesInt.Count; i++)
                        {
                            Console.WriteLine("Nombre: "+variablesInt[i].getNombre()+" Valor: "+variablesInt[i].getValor());
                        }
                        Console.WriteLine("");

                        Console.WriteLine("Variables Float");
                        for (int i = 0; i < variablesFloat.Count; i++)
                        {
                            Console.WriteLine("Nombre: " + variablesFloat[i].getNombre() + " Valor: " + variablesFloat[i].getValor());
                        }
                        Console.WriteLine("");

                        Console.WriteLine("Variables Double");
                        for (int i = 0; i < variablesDouble.Count; i++)
                        {
                            Console.WriteLine("Nombre: " + variablesDouble[i].getNombre() + " Valor: " + variablesDouble[i].getValor());
                        }
                        Console.WriteLine("");

                        Console.WriteLine("Variables String");
                        for (int i = 0; i < variablesString.Count; i++)
                        {
                            Console.WriteLine("Nombre: " + variablesString[i].getNombre() + " Valor: " + variablesString[i].getValor());
                        }
                        Console.WriteLine("");

                        Console.WriteLine("Variables Char");
                        for (int i = 0; i < variablesChar.Count; i++)
                        {
                            Console.WriteLine("Nombre: " + variablesChar[i].getNombre() + " Valor: " + variablesChar[i].getValor());
                        }
                        Console.WriteLine("");

                        Console.WriteLine("Variables Boolean");
                        for (int i = 0; i < variablesBoolean.Count; i++)
                        {
                            Console.WriteLine("Nombre: " + variablesBoolean[i].getNombre() + " Valor: " + variablesBoolean[i].getValor());
                        }
                        Console.WriteLine("");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Algo ocurrio al ejecutar la compilacion");
                    }                  
                }
                else
                {
                    listaErrores = ordenacionErrores.ordenarErrores(listaErrores);

                    for (int i = 0; i < listaErrores.Count; i++)
                    {
                        areaResultado.AppendText(listaErrores[i] + "\n");
                    }
                }
            }
        }

        private void limpiarMemoria()
        {
            variablesInt.Clear();
            variablesFloat.Clear();
            variablesDouble.Clear();
            variablesString.Clear();
            variablesChar.Clear();
            variablesBoolean.Clear();
        }

        private void compilarInstrucciones(List<elementoToken> tokens)
        {
            List<elementoToken> lineaEjecutada = new List<elementoToken>();
            int posicionTermino;

            for (int i = ignorarInicio(tokens); i < tokens.Count; i++)
            {
                if(tokens[i].getNombre().Equals("int") || tokens[i].getNombre().Equals("float") || tokens[i].getNombre().Equals("double") || tokens[i].getNombre().Equals("String") || tokens[i].getNombre().Equals("char") || tokens[i].getNombre().Equals("boolean"))
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

                    i = posicionTermino;

                    agregarVariable(lineaEjecutada);
                }
                else if(tokens[i].getTipo().Equals("id"))
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

                    i = posicionTermino;

                    editarVariable(lineaEjecutada);
                }
                else if (tokens[i].getNombre().Equals("System"))
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

                    i = posicionTermino;

                    imprimirPantalla(lineaEjecutada);
                }
                else if(tokens[i].getNombre().Equals("Scanner"))
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

                    i = posicionTermino;
                }
            }
        }

        #region Metodos especificos de compilacion
        private void agregarVariable(List<elementoToken> linea)
        {
            try
            {
                List<elementoVariable> variablesDetectadas = new List<elementoVariable>();
                List<elementoToken> valoresVariable = new List<elementoToken>();

                variablesDetectadas = deteccionVariables.detectarVariables(linea, false);
                string nuevoValor;

                for (int i = 0; i < variablesDetectadas.Count; i++)
                {
                    valoresVariable.Clear();

                    switch(variablesDetectadas[i].getTipo())
                    {
                        case "int":

                            #region Asignar variables tipo Int
                            if(variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");
                                variablesInt.Add(new tipoInt(variablesDetectadas[i].getNombre(), Int32.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if(valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for(int k = 0; k < variablesInt.Count; k++)
                                        {
                                            if(valoresVariable[j].getNombre().Equals(variablesInt[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesInt[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for(int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                nuevoValor = evaluarExpresion(nuevoValor);

                                variablesDetectadas[i].setValor(nuevoValor);

                                variablesInt.Add(new tipoInt(variablesDetectadas[i].getNombre(),Int32.Parse(variablesDetectadas[i].getValor())));
                            }
                            else
                            {
                                variablesInt.Add(new tipoInt(variablesDetectadas[i].getNombre()));
                            }
                            #endregion

                            break;

                        case "float":

                            #region Asignar variables tipo float
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");
                                variablesFloat.Add(new tipoFloat(variablesDetectadas[i].getNombre(), float.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesFloat.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesFloat[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesFloat[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                nuevoValor = evaluarExpresion(nuevoValor);

                                variablesDetectadas[i].setValor(nuevoValor);

                                variablesFloat.Add(new tipoFloat(variablesDetectadas[i].getNombre(), float.Parse(variablesDetectadas[i].getValor())));
                            }
                            else
                            {
                                variablesFloat.Add(new tipoFloat(variablesDetectadas[i].getNombre()));
                            }
                            #endregion

                            break;

                        case "double":

                            #region Asignar variables tipo double
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");
                                variablesDouble.Add(new tipoDouble(variablesDetectadas[i].getNombre(), double.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesDouble.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesDouble[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesDouble[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                nuevoValor = evaluarExpresion(nuevoValor);

                                variablesDetectadas[i].setValor(nuevoValor);

                                variablesDouble.Add(new tipoDouble(variablesDetectadas[i].getNombre(), double.Parse(variablesDetectadas[i].getValor())));
                            }
                            else
                            {
                                variablesDouble.Add(new tipoDouble(variablesDetectadas[i].getNombre()));
                            }
                            #endregion

                            break;

                        case "String":

                            #region Asignar variables tipo String
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");
                                variablesString.Add(new tipoString(variablesDetectadas[i].getNombre(), nuevoValor));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesInt.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesInt[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesInt[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesFloat.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesFloat[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesFloat[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesDouble.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesDouble[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesDouble[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesString.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesString[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesString[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesChar.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesChar[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesChar[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesBoolean.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesBoolean[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesBoolean[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for(int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getNombre().Equals("("))
                                    {
                                        int contadorParentesis = 1;
                                        string valorEvaluado = "(";

                                        for(int k = j + 1; k < valoresVariable.Count; k++)
                                        {
                                            if(valoresVariable[k].getNombre().Equals("("))
                                            {
                                                contadorParentesis++;
                                                valorEvaluado = valorEvaluado + " " + "(";
                                                valoresVariable[k].setNombre("");
                                            }
                                            else if(valoresVariable[k].getNombre().Equals(")"))
                                            {
                                                contadorParentesis--;
                                                valorEvaluado = valorEvaluado + " " + ")";
                                                valoresVariable[k].setNombre("");
                                            }
                                            else
                                            {
                                                valorEvaluado = valorEvaluado + " " + valoresVariable[k].getNombre();
                                                valoresVariable[k].setNombre("");
                                            }

                                            if(contadorParentesis == 0)
                                            {
                                                valorEvaluado = evaluarExpresion(valorEvaluado);
                                                valoresVariable[j].setNombre(valorEvaluado);
                                                j = k;
                                                k = valoresVariable.Count;
                                            }
                                        }
                                    }
                                }
                                
                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    if(!valoresVariable[j].getNombre().Equals(""))
                                    {
                                        nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                    }                                  
                                }

                                nuevoValor = nuevoValor.Replace(" + ", "").Replace("\"", "");

                                variablesDetectadas[i].setValor(nuevoValor);

                                variablesString.Add(new tipoString(variablesDetectadas[i].getNombre(), variablesDetectadas[i].getValor()));
                            }
                            else
                            {
                                variablesString.Add(new tipoString(variablesDetectadas[i].getNombre()));
                            }
                            #endregion

                            break;

                        case "char":

                            #region Asignar variables tipo char
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");
                                variablesChar.Add(new tipoChar(variablesDetectadas[i].getNombre(), nuevoValor[0]));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                if(!variablesDetectadas[i].getValor().Contains("'"))
                                {
                                    valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                    for (int j = 0; j < valoresVariable.Count; j++)
                                    {
                                        if (valoresVariable[j].getTipo().Equals("id"))
                                        {
                                            for (int k = 0; k < variablesChar.Count; k++)
                                            {
                                                if (valoresVariable[j].getNombre().Equals(variablesChar[k].getNombre()))
                                                {
                                                    valoresVariable[j].setNombre(variablesChar[k].getValor().ToString());
                                                }
                                            }
                                        }
                                    }

                                    nuevoValor = valoresVariable[0].getNombre();

                                    for (int j = 1; j < valoresVariable.Count; j++)
                                    {
                                        nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                    }

                                    variablesDetectadas[i].setValor(nuevoValor);

                                    variablesChar.Add(new tipoChar(variablesDetectadas[i].getNombre(), variablesDetectadas[i].getValor()[0]));
                                }
                                else
                                {
                                    variablesChar.Add(new tipoChar(variablesDetectadas[i].getNombre(), variablesDetectadas[i].getValor()[1]));
                                }
                                
                            }
                            else
                            {
                                variablesChar.Add(new tipoChar(variablesDetectadas[i].getNombre()));
                            }
                            #endregion

                            break;

                        case "boolean":

                            #region Asignar variables tipo boolean
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");
                                variablesBoolean.Add(new tipoBoolean(variablesDetectadas[i].getNombre(), bool.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                if (!variablesDetectadas[i].getValor().Contains("true") && !variablesDetectadas[i].getValor().Contains("false"))
                                {
                                    valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                    for (int j = 0; j < valoresVariable.Count; j++)
                                    {
                                        if (valoresVariable[j].getTipo().Equals("id"))
                                        {
                                            for (int k = 0; k < variablesBoolean.Count; k++)
                                            {
                                                if (valoresVariable[j].getNombre().Equals(variablesBoolean[k].getNombre()))
                                                {
                                                    valoresVariable[j].setNombre(variablesBoolean[k].getValor().ToString());
                                                }
                                            }
                                        }
                                    }

                                    nuevoValor = valoresVariable[0].getNombre();

                                    for (int j = 1; j < valoresVariable.Count; j++)
                                    {
                                        nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                    }

                                    variablesDetectadas[i].setValor(nuevoValor);

                                    variablesBoolean.Add(new tipoBoolean(variablesDetectadas[i].getNombre(), bool.Parse(variablesDetectadas[i].getValor())));
                                }
                                else
                                {
                                    variablesBoolean.Add(new tipoBoolean(variablesDetectadas[i].getNombre(), bool.Parse(variablesDetectadas[i].getValor())));
                                }

                            }
                            else
                            {
                                variablesBoolean.Add(new tipoBoolean(variablesDetectadas[i].getNombre()));
                            }
                            #endregion

                            break;

                        default:
                            Console.WriteLine("Algo ocurrio en el case de agregar variables");
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio algo al agregar una variable: " + ex.Message);
            }          
        }

        private void imprimirPantalla(List<elementoToken> linea)
        {
            try
            {
                string nuevoValor;

                for (int i = 6; i < linea.Count - 2; i++)
                {
                    if (linea[i].getTipo().Equals("id"))
                    {
                        for (int j = 0; j < variablesInt.Count; j++)
                        {
                            if (linea[i].getNombre().Equals(variablesInt[j].getNombre()))
                            {
                                linea[i].setNombre(variablesInt[j].getValor().ToString());
                            }
                        }
                    }
                }

                for (int i = 6; i < linea.Count - 2; i++)
                {
                    if (linea[i].getTipo().Equals("id"))
                    {
                        for (int j = 0; j < variablesFloat.Count; j++)
                        {
                            if (linea[i].getNombre().Equals(variablesFloat[j].getNombre()))
                            {
                                linea[i].setNombre(variablesFloat[j].getValor().ToString());
                            }
                        }
                    }
                }

                for (int i = 6; i < linea.Count - 2; i++)
                {
                    if (linea[i].getTipo().Equals("id"))
                    {
                        for (int j = 0; j < variablesDouble.Count; j++)
                        {
                            if (linea[i].getNombre().Equals(variablesDouble[j].getNombre()))
                            {
                                linea[i].setNombre(variablesDouble[j].getValor().ToString());
                            }
                        }
                    }
                }

                for (int i = 6; i < linea.Count - 2; i++)
                {
                    if (linea[i].getTipo().Equals("id"))
                    {
                        for (int j = 0; j < variablesString.Count; j++)
                        {
                            if (linea[i].getNombre().Equals(variablesString[j].getNombre()))
                            {
                                linea[i].setNombre(variablesString[j].getValor().ToString());
                            }
                        }
                    }
                }

                for (int i = 6; i < linea.Count - 2; i++)
                {
                    if (linea[i].getTipo().Equals("id"))
                    {
                        for (int j = 0; j < variablesChar.Count; j++)
                        {
                            if (linea[i].getNombre().Equals(variablesChar[j].getNombre()))
                            {
                                linea[i].setNombre(variablesChar[j].getValor().ToString());
                            }
                        }
                    }
                }

                for (int i = 6; i < linea.Count - 2; i++)
                {
                    if (linea[i].getTipo().Equals("id"))
                    {
                        for (int j = 0; j < variablesBoolean.Count; j++)
                        {
                            if (linea[i].getNombre().Equals(variablesBoolean[j].getNombre()))
                            {
                                linea[i].setNombre(variablesBoolean[j].getValor().ToString());
                            }
                        }
                    }
                }

                for (int i = 6; i < linea.Count - 2; i++)
                {
                    if (linea[i].getNombre().Equals("("))
                    {
                        int contadorParentesis = 1;
                        string valorEvaluado = "(";

                        for (int j = i + 1; j < linea.Count; j++)
                        {
                            if (linea[j].getNombre().Equals("("))
                            {
                                contadorParentesis++;
                                valorEvaluado = valorEvaluado + " " + "(";
                                linea[j].setNombre("");
                            }
                            else if (linea[j].getNombre().Equals(")"))
                            {
                                contadorParentesis--;
                                valorEvaluado = valorEvaluado + " " + ")";
                                linea[j].setNombre("");
                            }
                            else
                            {
                                valorEvaluado = valorEvaluado + " " + linea[j].getNombre();
                                linea[j].setNombre("");
                            }

                            if (contadorParentesis == 0)
                            {
                                valorEvaluado = evaluarExpresion(valorEvaluado);
                                linea[i].setNombre(valorEvaluado);
                                i = j;
                                j = linea.Count;
                            }
                        }
                    }
                }

                nuevoValor = linea[6].getNombre();

                for (int i = 7; i < linea.Count - 2; i++)
                {
                    if (!linea[i].getNombre().Equals(""))
                    {
                        nuevoValor = nuevoValor + " " + linea[i].getNombre();
                    }
                }

                nuevoValor = nuevoValor.Replace(" + ", "").Replace("\"", "");

                if(linea[4].getNombre().Equals("println"))
                {
                    areaResultado.AppendText(nuevoValor + "\n");
                }
                else
                {
                    areaResultado.AppendText(nuevoValor);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio algo al imprimir en pantalla: " + ex.Message);
            }
        }

        private void editarVariable(List<elementoToken> linea)
        {
            try
            {
                List<elementoVariable> variablesDetectadas = new List<elementoVariable>();
                List<elementoToken> valoresVariable = new List<elementoToken>();

                variablesDetectadas = deteccionVariables.detectarVariables(linea, false);
                string tipoVariable;
                string nuevoValor;

                for(int i = 0; i < variablesDetectadas.Count; i++)
                {
                    tipoVariable = "";

                    #region Determinar tipo
                    for (int j = 0; j < variablesInt.Count; j++)
                    {
                        if (variablesDetectadas[i].getNombre().Equals(variablesInt[j].getNombre()))
                        {
                            tipoVariable = "int";
                        }
                    }

                    if (tipoVariable.Equals(""))
                    {
                        for (int j = 0; j < variablesFloat.Count; j++)
                        {
                            if (variablesDetectadas[i].getNombre().Equals(variablesFloat[j].getNombre()))
                            {
                                tipoVariable = "float";
                            }
                        }
                    }

                    if (tipoVariable.Equals(""))
                    {
                        for (int j = 0; j < variablesDouble.Count; j++)
                        {
                            if (variablesDetectadas[i].getNombre().Equals(variablesDouble[j].getNombre()))
                            {
                                tipoVariable = "double";
                            }
                        }
                    }

                    if (tipoVariable.Equals(""))
                    {
                        for (int j = 0; j < variablesString.Count; j++)
                        {
                            if (variablesDetectadas[i].getNombre().Equals(variablesString[j].getNombre()))
                            {
                                tipoVariable = "String";
                            }
                        }
                    }

                    if (tipoVariable.Equals(""))
                    {
                        for (int j = 0; j < variablesChar.Count; j++)
                        {
                            if (variablesDetectadas[i].getNombre().Equals(variablesChar[j].getNombre()))
                            {
                                tipoVariable = "char";
                            }
                        }
                    }

                    if (tipoVariable.Equals(""))
                    {
                        for (int j = 0; j < variablesBoolean.Count; j++)
                        {
                            if (variablesDetectadas[i].getNombre().Equals(variablesBoolean[j].getNombre()))
                            {
                                tipoVariable = "boolean";
                            }
                        }
                    }
                    #endregion

                    switch (tipoVariable)
                    {
                        case "int":

                            #region Editar variables tipo Int
                            if(variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");

                                for (int j = 0; j < variablesInt.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesInt[j].getNombre()))
                                    {
                                        variablesInt[j].setValor(Int32.Parse(nuevoValor));
                                    }
                                }
                            }
                            else
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesInt.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesInt[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesInt[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                nuevoValor = evaluarExpresion(nuevoValor);

                                variablesDetectadas[i].setValor(nuevoValor);

                                for (int j = 0; j < variablesInt.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesInt[j].getNombre()))
                                    {
                                        variablesInt[j].setValor(Int32.Parse(variablesDetectadas[i].getValor()));
                                    }
                                }
                            }
                            
                            #endregion

                            break;

                        case "float":

                            #region Editar variables tipo float
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");

                                for (int j = 0; j < variablesFloat.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesFloat[j].getNombre()))
                                    {
                                        variablesFloat[j].setValor(float.Parse(nuevoValor));
                                    }
                                }
                            }
                            else
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesFloat.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesFloat[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesFloat[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                nuevoValor = evaluarExpresion(nuevoValor);

                                variablesDetectadas[i].setValor(nuevoValor);

                                for (int j = 0; j < variablesFloat.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesFloat[j].getNombre()))
                                    {
                                        variablesFloat[j].setValor(float.Parse(variablesDetectadas[i].getValor()));
                                    }
                                }
                            }
                                
                            #endregion

                            break;

                        case "double":

                            #region Editar variables tipo double
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");

                                for (int j = 0; j < variablesDouble.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesDouble[j].getNombre()))
                                    {
                                        variablesDouble[j].setValor(double.Parse(nuevoValor));
                                    }
                                }
                            }
                            else
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesDouble.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesDouble[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesDouble[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                nuevoValor = evaluarExpresion(nuevoValor);

                                variablesDetectadas[i].setValor(nuevoValor);

                                for (int j = 0; j < variablesDouble.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesDouble[j].getNombre()))
                                    {
                                        variablesDouble[j].setValor(double.Parse(variablesDetectadas[i].getValor()));
                                    }
                                }
                            }
                            
                            #endregion

                            break;

                        case "String":

                            #region Editar variables tipo String
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");

                                for (int j = 0; j < variablesString.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesString[j].getNombre()))
                                    {
                                        variablesString[j].setValor(nuevoValor);
                                    }
                                }
                            }
                            else
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesInt.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesInt[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesInt[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesFloat.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesFloat[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesFloat[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesDouble.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesDouble[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesDouble[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesString.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesString[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesString[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesChar.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesChar[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesChar[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesBoolean.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesBoolean[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesBoolean[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getNombre().Equals("("))
                                    {
                                        int contadorParentesis = 1;
                                        string valorEvaluado = "(";

                                        for (int k = j + 1; k < valoresVariable.Count; k++)
                                        {
                                            if (valoresVariable[k].getNombre().Equals("("))
                                            {
                                                contadorParentesis++;
                                                valorEvaluado = valorEvaluado + " " + "(";
                                                valoresVariable[k].setNombre("");
                                            }
                                            else if (valoresVariable[k].getNombre().Equals(")"))
                                            {
                                                contadorParentesis--;
                                                valorEvaluado = valorEvaluado + " " + ")";
                                                valoresVariable[k].setNombre("");
                                            }
                                            else
                                            {
                                                valorEvaluado = valorEvaluado + " " + valoresVariable[k].getNombre();
                                                valoresVariable[k].setNombre("");
                                            }

                                            if (contadorParentesis == 0)
                                            {
                                                valorEvaluado = evaluarExpresion(valorEvaluado);
                                                valoresVariable[j].setNombre(valorEvaluado);
                                                j = k;
                                                k = valoresVariable.Count;
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    if (!valoresVariable[j].getNombre().Equals(""))
                                    {
                                        nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                    }
                                }

                                nuevoValor = nuevoValor.Replace(" + ", "").Replace("\"", "");

                                variablesDetectadas[i].setValor(nuevoValor);

                                for (int j = 0; j < variablesString.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesString[j].getNombre()))
                                    {
                                        variablesString[j].setValor(variablesDetectadas[i].getValor());
                                    }
                                }
                            }
                            
                            #endregion

                            break;

                        case "char":

                            #region Editar variables tipo char
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");

                                for (int j = 0; j < variablesChar.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesChar[j].getNombre()))
                                    {
                                        variablesChar[j].setValor(nuevoValor[0]);
                                    }
                                }
                            }
                            else if (!variablesDetectadas[i].getValor().Contains("'"))
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesChar.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesChar[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesChar[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                variablesDetectadas[i].setValor(nuevoValor);

                                for (int j = 0; j < variablesChar.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesChar[j].getNombre()))
                                    {
                                        variablesChar[j].setValor(variablesDetectadas[i].getValor()[0]);
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < variablesChar.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesChar[j].getNombre()))
                                    {
                                        variablesChar[j].setValor(variablesDetectadas[i].getValor()[1]);
                                    }
                                }
                            }
                            #endregion

                            break;

                        case "boolean":

                            #region Editar variables tipo boolean
                            if (variablesDetectadas[i].getValor().Contains(".next"))
                            {
                                nuevoValor = Form2.Show();
                                areaResultado.AppendText(nuevoValor + "\n");

                                for (int j = 0; j < variablesBoolean.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesBoolean[j].getNombre()))
                                    {
                                        variablesBoolean[j].setValor(bool.Parse(nuevoValor));
                                    }
                                }
                            }
                            else if (!variablesDetectadas[i].getValor().Contains("true") && !variablesDetectadas[i].getValor().Contains("false"))
                            {
                                valoresVariable = generarTokensValor(variablesDetectadas[i].getValor());

                                for (int j = 0; j < valoresVariable.Count; j++)
                                {
                                    if (valoresVariable[j].getTipo().Equals("id"))
                                    {
                                        for (int k = 0; k < variablesBoolean.Count; k++)
                                        {
                                            if (valoresVariable[j].getNombre().Equals(variablesBoolean[k].getNombre()))
                                            {
                                                valoresVariable[j].setNombre(variablesBoolean[k].getValor().ToString());
                                            }
                                        }
                                    }
                                }

                                nuevoValor = valoresVariable[0].getNombre();

                                for (int j = 1; j < valoresVariable.Count; j++)
                                {
                                    nuevoValor = nuevoValor + " " + valoresVariable[j].getNombre();
                                }

                                variablesDetectadas[i].setValor(nuevoValor);

                                for (int j = 0; j < variablesBoolean.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesBoolean[j].getNombre()))
                                    {
                                        variablesBoolean[j].setValor(bool.Parse(variablesDetectadas[i].getValor()));
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < variablesBoolean.Count; j++)
                                {
                                    if (variablesDetectadas[i].getNombre().Equals(variablesBoolean[j].getNombre()))
                                    {
                                        variablesBoolean[j].setValor(bool.Parse(variablesDetectadas[i].getValor()));
                                    }
                                }
                            }
                            #endregion

                            break;

                        default:
                            Console.WriteLine("Algo ocurrio en el case de editar variables: "+variablesDetectadas[i].getLinea());
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocurrio algo al editar una variable: "+ex.Message);
            }
        }

        #endregion

        #region Metodos auxiliares de compilacion
        private int ignorarInicio(List<elementoToken> tokens)
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

        private List<elementoToken> obtenerTokensLinea(List<elementoToken> tokens, int inicio, int final)
        {
            List<elementoToken> lista = new List<elementoToken>();

            for (int i = inicio; i <= final; i++)
            {
                lista.Add(tokens[i]);
            }

            return lista;
        }

        private List<elementoToken> generarTokensValor(string valor)
        {
            List<elementoToken> tokens = new List<elementoToken>();

            string codigo = valor;
            var gramatica = new gramaticaConcatenacion();

            var parser = new Parser(gramatica);
            var arbol = parser.Parse(codigo);

            if (arbol.Root == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    Console.WriteLine(arbol.ParserMessages[i].Message);
                }
                areaResultado.AppendText("Detalles al generar tokens de asignacion" + "\n");
            }
            else
            {
                elementoToken auxiliar;

                for (int i = 0; i < arbol.Tokens.Count - 1; i++)
                {
                    auxiliar = new elementoToken();
                    auxiliar.setNombre(arbol.Tokens[i].Text);
                    auxiliar.setTipo(arbol.Tokens[i].ToString().Split('(')[1].Replace("(", "").Replace(")", "").Replace("}", ""));
                    auxiliar.setLinea(arbol.Tokens[i].Location.Line + 1);
                    tokens.Add(auxiliar);
                }
            }

            return tokens;
        }

        private string evaluarExpresion(string expression)
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

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            menuArchivo.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Maximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            maximizar.Visible = false;
            restaurar.Visible = true;
        }

        private void Restaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            restaurar.Visible = false;
            maximizar.Visible = true;
        }

        private void AreaResultado_TextChanged(object sender, EventArgs e)
        {
            

        }
        
        class MenuColorTable : ProfessionalColorTable
        {
            public MenuColorTable()
            {
                base.UseSystemColors = false;
                
            }
            public override System.Drawing.Color MenuBorder
            {
                    get { return Color.FromArgb(28, 23, 38); }
            }
            public override System.Drawing.Color MenuItemBorder
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuStripGradientBegin
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuStripGradientEnd
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color ToolStripDropDownBackground 
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color ToolStripBorder
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color ToolStripContentPanelGradientBegin
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuItemPressedGradientBegin 
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuItemPressedGradientMiddle
            {
                get { return Color.FromArgb(28, 23, 38); }
            }
            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(28, 23, 38); }
            }

        }
        private void stop_Click(object sender, EventArgs e)
        {
            
        }
    }
}
