using compiladorR.Analisis.Gramaticas;
using compiladorR.Analisis.Semantica;
using compiladorR.Compilacion;
using compiladorR.Compilacion.ModelosDatos;
using compiladorR.Compilacion.ModelosSentencias;
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

        List<sentenciaIf> sentenciasIf = new List<sentenciaIf>();
        List<sentenciaSwitch> sentenciasSwitch = new List<sentenciaSwitch>();

        bool valorStop;

        public Form1()
        {
            InitializeComponent();
            this.MaximumSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
        }

        private void run_Click(object sender, EventArgs e)
        {
            valorStop = false;
            cambiarPanelEstado();
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
                areaResultado.AppendText("Error: Código no permitido por la gramatica" + "\n");
                valorStop = true;
                cambiarPanelEstado();
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
                        /*
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
                        
                        Console.WriteLine();
                        for(int i = 0; i < sentenciasIf.Count; i++)
                        {
                            Console.WriteLine("Condicion: "+sentenciasIf[i].getCondicion());
                            Console.WriteLine("Tokens");
                            for(int j = 0; j < sentenciasIf[i].getTokens().Count; j++)
                            {
                                Console.WriteLine(sentenciasIf[i].getTokens()[j].getNombre());
                            }
                        }
                        Console.WriteLine("");

                        Console.WriteLine();
                        for (int i = 0; i < sentenciasSwitch.Count; i++)
                        {
                            Console.WriteLine("Caso: " + sentenciasSwitch[i].getCaso());
                            Console.WriteLine("Tokens");
                            for (int j = 0; j < sentenciasSwitch[i].getTokens().Count; j++)
                            {
                                Console.WriteLine(sentenciasSwitch[i].getTokens()[j].getNombre());
                            }
                        }
                        Console.WriteLine("");*/

                        if (valorStop == false)
                        {
                            areaResultado.AppendText("Proceso finalizado" + "\n");
                        }
                        
                    }
                    catch(Exception ex)
                    {
                        valorStop = true;
                        cambiarPanelEstado();
                        areaResultado.AppendText("Error: "+ex.Message + "\n");
                        areaResultado.SelectionStart = areaResultado.Text.Length;
                        areaResultado.ScrollToCaret();
                    }                  
                }
                else
                {
                    valorStop = true;
                    cambiarPanelEstado();
                    listaErrores = ordenacionErrores.ordenarErrores(listaErrores);

                    for (int i = 0; i < listaErrores.Count; i++)
                    {
                        areaResultado.AppendText(listaErrores[i] + "\n");
                        areaResultado.SelectionStart = areaResultado.Text.Length;
                        areaResultado.ScrollToCaret();
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

            sentenciasIf.Clear();
            sentenciasSwitch.Clear();
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

                    if(valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
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

                    if (valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
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

                    if (valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
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
                else if(tokens[i].getNombre().Equals("if"))
                {
                    sentenciasIf.Clear();
                    posicionTermino = i;
                    int posicionPrimerCorchete = i;
                    int contadorCorchetes = 0;
                    bool seguroElse = false;

                    for(int j = i + 1; j < tokens.Count; j++)
                    {
                        if(tokens[j].getNombre().Equals("{"))
                        {
                            posicionPrimerCorchete = j;
                            contadorCorchetes++;
                            j = tokens.Count;
                        }
                    }

                    for(int j = posicionPrimerCorchete + 1; j < tokens.Count; j++)
                    {
                        if(tokens[j].getNombre().Equals("{"))
                        {
                            contadorCorchetes++;
                            seguroElse = false;
                        }

                        if(tokens[j].getNombre().Equals("}"))
                        {
                            contadorCorchetes--;
                        }

                        if(contadorCorchetes == 0 && seguroElse == false)
                        {
                            if(tokens[j+1].getNombre().Equals("else"))
                            {
                                seguroElse = true;
                            }
                            else
                            {
                                posicionTermino = j;
                                j = tokens.Count;
                            }
                        }
                    }

                    lineaEjecutada = obtenerTokensLinea(tokens, i, posicionTermino);

                    i = posicionTermino;

                    evaluarIf(lineaEjecutada);

                    if (valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
                }
                else if(tokens[i].getNombre().Equals("switch"))
                {
                    sentenciasSwitch.Clear();
                    posicionTermino = i;
                    int posicionPrimerCorchete = i;
                    int contadorCorchetes = 0;

                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            posicionPrimerCorchete = j;
                            contadorCorchetes++;
                            j = tokens.Count;
                        }
                    }

                    for (int j = posicionPrimerCorchete + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            contadorCorchetes++;
                        }

                        if (tokens[j].getNombre().Equals("}"))
                        {
                            contadorCorchetes--;
                        }

                        if (contadorCorchetes == 0)
                        {
                                posicionTermino = j;
                                j = tokens.Count;
                        }
                    }

                    lineaEjecutada = obtenerTokensLinea(tokens, i, posicionTermino);

                    i = posicionTermino;

                    evaluarSwitch(lineaEjecutada);

                    if (valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
                }
                else if(tokens[i].getNombre().Equals("for"))
                {
                    posicionTermino = i;
                    int posicionPrimerCorchete = i;
                    int contadorCorchetes = 0;

                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            posicionPrimerCorchete = j;
                            contadorCorchetes++;
                            j = tokens.Count;
                        }
                    }

                    for (int j = posicionPrimerCorchete + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            contadorCorchetes++;
                        }

                        if (tokens[j].getNombre().Equals("}"))
                        {
                            contadorCorchetes--;
                        }

                        if (contadorCorchetes == 0)
                        {
                            posicionTermino = j;
                            j = tokens.Count;
                        }
                    }

                    lineaEjecutada = obtenerTokensLinea(tokens, i, posicionTermino);

                    i = posicionTermino;

                    evaluarFor(lineaEjecutada);

                    if (valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
                }
                else if (tokens[i].getNombre().Equals("while"))
                {
                    posicionTermino = i;
                    int posicionPrimerCorchete = i;
                    int contadorCorchetes = 0;

                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            posicionPrimerCorchete = j;
                            contadorCorchetes++;
                            j = tokens.Count;
                        }
                    }

                    for (int j = posicionPrimerCorchete + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            contadorCorchetes++;
                        }

                        if (tokens[j].getNombre().Equals("}"))
                        {
                            contadorCorchetes--;
                        }

                        if (contadorCorchetes == 0)
                        {
                            posicionTermino = j;
                            j = tokens.Count;
                        }
                    }

                    lineaEjecutada = obtenerTokensLinea(tokens, i, posicionTermino);

                    i = posicionTermino;

                    evaluarWhile(lineaEjecutada);

                    if (valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
                }
                else if (tokens[i].getNombre().Equals("do"))
                {
                    posicionTermino = i;
                    int posicionPrimerCorchete = i;
                    int contadorCorchetes = 0;
                    int contadorParentesis = 0;

                    for (int j = i + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            posicionPrimerCorchete = j;
                            contadorCorchetes++;
                            j = tokens.Count;
                        }
                    }

                    for (int j = posicionPrimerCorchete + 1; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("{"))
                        {
                            contadorCorchetes++;
                        }

                        if (tokens[j].getNombre().Equals("}"))
                        {
                            contadorCorchetes--;
                        }

                        if (contadorCorchetes == 0)
                        {
                            posicionTermino = j;
                            j = tokens.Count;
                        }
                    }

                    for (int j = posicionTermino + 2; j < tokens.Count; j++)
                    {
                        if (tokens[j].getNombre().Equals("("))
                        {
                            contadorParentesis++;
                        }

                        if (tokens[j].getNombre().Equals(")"))
                        {
                            contadorParentesis--;
                        }

                        if (contadorParentesis == 0)
                        {
                            posicionTermino = j;
                            j = tokens.Count;
                        }
                    }

                    posicionTermino++;

                    lineaEjecutada = obtenerTokensLinea(tokens, i, posicionTermino);

                    i = posicionTermino;

                    evaluarDoWhile(lineaEjecutada);

                    if (valorStop == true)
                    {
                        cambiarPanelEstado();
                        return;
                    }
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
                                nuevoValor = Form2.Show("int",variablesDetectadas[i].getNombre());
                                if(nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
                                areaResultado.AppendText(nuevoValor + "\n");
                                areaResultado.SelectionStart = areaResultado.Text.Length;
                                areaResultado.ScrollToCaret();
                                variablesInt.Add(new tipoInt(variablesDetectadas[i].getNombre(), Int32.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("float", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
                                areaResultado.AppendText(nuevoValor + "\n");
                                areaResultado.SelectionStart = areaResultado.Text.Length;
                                areaResultado.ScrollToCaret();
                                variablesFloat.Add(new tipoFloat(variablesDetectadas[i].getNombre(), float.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("double", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
                                areaResultado.AppendText(nuevoValor + "\n");
                                areaResultado.SelectionStart = areaResultado.Text.Length;
                                areaResultado.ScrollToCaret();
                                variablesDouble.Add(new tipoDouble(variablesDetectadas[i].getNombre(), double.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("String", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
                                areaResultado.AppendText(nuevoValor + "\n");
                                areaResultado.SelectionStart = areaResultado.Text.Length;
                                areaResultado.ScrollToCaret();
                                variablesString.Add(new tipoString(variablesDetectadas[i].getNombre(), nuevoValor));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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

                                                if(valorEvaluado.Equals("Error"))
                                                {
                                                    areaResultado.AppendText("Error: La expresion no ha podido ser evaluada. Linea: "+ variablesDetectadas[i].getLinea() + "\n");
                                                    valorStop = true;
                                                    return;
                                                }
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
                                nuevoValor = Form2.Show("char", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
                                areaResultado.AppendText(nuevoValor + "\n");
                                areaResultado.SelectionStart = areaResultado.Text.Length;
                                areaResultado.ScrollToCaret();
                                variablesChar.Add(new tipoChar(variablesDetectadas[i].getNombre(), nuevoValor[0]));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                if(!variablesDetectadas[i].getValor().Contains("'"))
                                {
                                    valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("boolean", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
                                areaResultado.AppendText(nuevoValor + "\n");
                                areaResultado.SelectionStart = areaResultado.Text.Length;
                                areaResultado.ScrollToCaret();
                                variablesBoolean.Add(new tipoBoolean(variablesDetectadas[i].getNombre(), bool.Parse(nuevoValor)));
                            }
                            else if (!variablesDetectadas[i].getValor().Equals(""))
                            {
                                if (!variablesDetectadas[i].getValor().Contains("true") && !variablesDetectadas[i].getValor().Contains("false"))
                                {
                                    valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                            areaResultado.AppendText("Error: Declaracion de variable no permitida");
                            areaResultado.SelectionStart = areaResultado.Text.Length;
                            areaResultado.ScrollToCaret();
                            valorStop = true;
                            return;
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
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

                                if (valorEvaluado.Equals("Error"))
                                {
                                    areaResultado.AppendText("Error: La expresion no ha podido ser evaluada" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }

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
                    areaResultado.SelectionStart = areaResultado.Text.Length;
                    areaResultado.ScrollToCaret();
                }
                else
                {
                    areaResultado.AppendText(nuevoValor);
                    areaResultado.SelectionStart = areaResultado.Text.Length;
                    areaResultado.ScrollToCaret();
                }
                
            }
            catch(Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
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
                                nuevoValor = Form2.Show("int", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
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
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("float", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
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
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("double", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
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
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("String", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
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
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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

                                                if (valorEvaluado.Equals("Error"))
                                                {
                                                    areaResultado.AppendText("Error: La expresion no ha podido ser evaluada. Linea: " + variablesDetectadas[i].getLinea() + "\n");
                                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                                    areaResultado.ScrollToCaret();
                                                    valorStop = true;
                                                    return;
                                                }

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
                                nuevoValor = Form2.Show("char", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
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
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                                nuevoValor = Form2.Show("boolean", variablesDetectadas[i].getNombre());
                                if (nuevoValor.Equals("close"))
                                {
                                    areaResultado.AppendText("Programa Detenido" + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    valorStop = true;
                                    return;
                                }
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
                                valoresVariable = generarTokensConcatenacion(variablesDetectadas[i].getValor());

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
                            areaResultado.AppendText("Error: Conflictos al editar variables" + "\n");
                            areaResultado.SelectionStart = areaResultado.Text.Length;
                            areaResultado.ScrollToCaret();
                            valorStop = true;
                            return;
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
            }
        }

        private void evaluarIf(List<elementoToken> linea)
        {
            try
            {
                int contadorCorchetes;
                int contadorParentesis;
                string condicion;
                List<elementoToken> tokens;

                for(int i = 0; i < linea.Count; i++)
                {
                    contadorParentesis = 0;
                    contadorCorchetes = 0;

                    if(linea[i].getNombre().Equals("if"))
                    {
                        condicion = "";
                        tokens = new List<elementoToken>();

                        for(int j = i + 1; j < linea.Count; j++)
                        {
                            if(linea[j].getNombre().Equals("("))
                            {
                                contadorParentesis++;
                            }
                            else if(linea[j].getNombre().Equals(")"))
                            {
                                contadorParentesis--;
                            }
                            
                            if(condicion.Equals(""))
                            {
                                condicion = linea[j].getNombre();
                            }
                            else
                            {
                                condicion = condicion + " " + linea[j].getNombre();
                            }

                            if(contadorParentesis == 0)
                            {
                                condicion = condicion.Substring(1,condicion.Length-2);
                                condicion = reemplazarVariablesCondicion(condicion);
                                condicion = evaluarCondicion(condicion);

                                if(condicion.Equals("error"))
                                {
                                    valorStop = true;
                                    areaResultado.AppendText("Error: La condicion ingresada no es valida. Linea: " + linea[i].getLinea() + "\n");
                                    areaResultado.SelectionStart = areaResultado.Text.Length;
                                    areaResultado.ScrollToCaret();
                                    return;
                                }

                                i = j;
                                j = linea.Count;
                            }
                        }

                        for(int j = i + 1; j < linea.Count; j++)
                        {
                            if(linea[j].getNombre().Equals("{"))
                            {
                                contadorCorchetes++;
                            }
                            else if(linea[j].getNombre().Equals("}"))
                            {
                                contadorCorchetes--;
                            }

                            tokens.Add(linea[j]);

                            if(contadorCorchetes == 0)
                            {
                                sentenciasIf.Add(new sentenciaIf(condicion,tokens));
                                i = j;
                                j = linea.Count;
                            }
                        }
                    }
                    else if(linea[i].getNombre().Equals("else") && !linea[i+1].getNombre().Equals("if"))
                    {
                        contadorCorchetes = 0;
                        condicion = "";
                        tokens = new List<elementoToken>();

                        for (int j = i + 1; j < linea.Count; j++)
                        {
                            if (linea[j].getNombre().Equals("{"))
                            {
                                contadorCorchetes++;
                            }
                            else if (linea[j].getNombre().Equals("}"))
                            {
                                contadorCorchetes--;
                            }

                            tokens.Add(linea[j]);

                            if (contadorCorchetes == 0)
                            {
                                sentenciasIf.Add(new sentenciaIf(condicion, tokens));
                                i = j;
                                j = linea.Count;
                            }
                        }
                    }
                }

                for(int i = 0; i < sentenciasIf.Count; i++)
                {
                    if(sentenciasIf[i].getCondicion().Equals("True") || sentenciasIf[i].getCondicion().Equals(""))
                    {
                        compilarInstrucciones(sentenciasIf[i].getTokens());
                        i = sentenciasIf.Count;
                    }
                }
            }
            catch(Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
            }
        }

        private void evaluarSwitch(List<elementoToken> linea)
        {
            try
            {
                int posicionInicio = 0;
                int contadorParentesis = 0;
                int contadorCase = 0;
                int auxJ = 0;
                string valorEntrada = "";
                string valor = "";
                List<elementoToken> tokens = new List<elementoToken>();

                for (int i = 1; i < linea.Count; i++)
                {
                    if (linea[i].getNombre().Equals("("))
                    {
                        contadorParentesis++;
                    }
                    else if (linea[i].getNombre().Equals(")"))
                    {
                        contadorParentesis--;
                    }

                    if (valorEntrada.Equals(""))
                    {
                        valorEntrada = linea[i].getNombre();
                    }
                    else
                    {
                        valorEntrada = valorEntrada + " " + linea[i].getNombre();
                    }

                    if (contadorParentesis == 0)
                    {
                        valorEntrada = valorEntrada.Substring(1, valorEntrada.Length - 2);
                        valorEntrada = reemplazarVariablesValor(valorEntrada);
                        valorEntrada = evaluarCondicion(valorEntrada);

                        if (valorEntrada.Equals("Error"))
                        {
                            valorStop = true;
                            areaResultado.AppendText("Error: La condicion ingresada no es valida. Linea: " + linea[i].getLinea() + "\n");
                            areaResultado.SelectionStart = areaResultado.Text.Length;
                            areaResultado.ScrollToCaret();
                            return;
                        }

                        posicionInicio = i + 1;
                        i = linea.Count;
                    }
                }

                for (int i = posicionInicio; i < linea.Count; i++)
                {
                    valor = "";
                    contadorCase = 0;
                    tokens = new List<elementoToken>();

                    if(linea[i].getNombre().Equals("case") || linea[i].getNombre().Equals("default"))
                    {
                        contadorCase++;
                        
                        if(linea[i].getNombre().Equals("case"))
                        {
                            valor = linea[i + 1].getNombre().Replace("\"","").Replace("'","");
                            auxJ = i + 3;
                        }
                        else
                        {
                            valor = "default";
                            auxJ = i + 2;
                        }

                        for(int j = auxJ ; j < linea.Count; j++)
                        {
                            if(linea[j].getNombre().Equals("case") || linea[j].getNombre().Equals("default"))
                            {
                                contadorCase++;
                            }
                            else if(linea[j].getNombre().Equals("break"))
                            {
                                contadorCase--;
                            }

                            tokens.Add(linea[j]);

                            if(contadorCase == 0)
                            {
                                tokens.Add(linea[j+1]);
                                j++;
                                sentenciasSwitch.Add(new sentenciaSwitch(valor,tokens));
                                i = j;
                                j = linea.Count;
                            }
                        }
                    }
                }

                for (int i = 0; i < sentenciasSwitch.Count; i++)
                {
                    if (sentenciasSwitch[i].getCaso().Equals(valorEntrada) || sentenciasSwitch[i].getCaso().Equals("default"))
                    {
                        compilarInstrucciones(sentenciasSwitch[i].getTokens());
                        i = sentenciasSwitch.Count;
                    }
                }
            }
            catch(Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
            }
        }

        private void evaluarFor(List<elementoToken> linea)
        {
            Console.WriteLine("Entre al for");
            try
            {
                int contadorCorchetes = 0;
                int contadorParentesis = 0;
                int puntoControl = 0;
                int contadorPuntosComa = 0;

                List<elementoToken> tokensCondiciones = new List<elementoToken>();
                List<elementoToken> tokensInstrucciones = new List<elementoToken>();
                List<elementoToken> tokensInstruccionesAux = new List<elementoToken>();

                List<elementoToken> tokensValor = new List<elementoToken>();
                string condicion = "";
                List<elementoToken> tokensIncremento = new List<elementoToken>();

                for (int i = 1; i < linea.Count; i++)
                {
                    if (linea[i].getNombre().Equals("("))
                    {
                        contadorParentesis++;
                    }
                    else if (linea[i].getNombre().Equals(")"))
                    {
                        contadorParentesis--;
                    }

                    tokensCondiciones.Add(linea[i]);

                    if (contadorParentesis == 0)
                    {
                        puntoControl = i;
                        i = linea.Count;
                    }
                }

                for (int i = puntoControl + 1; i < linea.Count; i++)
                {
                    if (linea[i].getNombre().Equals("{"))
                    {
                        contadorCorchetes++;
                    }
                    else if (linea[i].getNombre().Equals("}"))
                    {
                        contadorCorchetes--;
                    }

                    tokensInstrucciones.Add(new elementoToken(linea[i].getNombre(), linea[i].getTipo(), linea[i].getLinea()));
                    tokensInstruccionesAux.Add(new elementoToken(linea[i].getNombre(), linea[i].getTipo(), linea[i].getLinea()));

                    if (contadorCorchetes == 0)
                    {
                        i = linea.Count;
                    }
                }

                for(int i = 0; i < tokensCondiciones.Count; i++)
                {
                    if(condicion.Equals(""))
                    {
                        condicion = tokensCondiciones[i].getNombre();
                    }
                    else
                    {
                        condicion = condicion + " " + tokensCondiciones[i].getNombre();
                    }
                }

                condicion = condicion.Split(';')[1];

                for(int i = 1; i < tokensCondiciones.Count-1; i++)
                {
                    if(contadorPuntosComa == 0)
                    {
                        tokensValor.Add(tokensCondiciones[i]);
                    }
                    else if (contadorPuntosComa == 2)
                    {
                        tokensIncremento.Add(tokensCondiciones[i]);
                    }
                    
                    if (tokensCondiciones[i].getNombre().Equals(";"))
                    {
                        contadorPuntosComa++;
                    }              
                }

                tokensIncremento.Add(tokensValor[tokensValor.Count-1]);

                if(tokensValor[0].getNombre().Equals("int"))
                {
                    for(int i = 0; i < variablesInt.Count; i++)
                    {
                        if(tokensValor[1].getNombre().Equals(variablesInt[i].getNombre()))
                        {
                            tokensValor[0].setNombre("");
                        }
                    }
                }

                compilarInstrucciones(tokensValor);

                while(evaluarCondicion(reemplazarVariablesCondicion(condicion)).Equals("True") && valorStop == false)
                {
                    compilarInstrucciones(tokensInstrucciones);
                    compilarInstrucciones(tokensIncremento);

                    tokensInstrucciones.Clear();

                    for(int i = 0; i < tokensInstruccionesAux.Count; i++)
                    {
                        tokensInstrucciones.Add(new elementoToken(tokensInstruccionesAux[i].getNombre(),tokensInstruccionesAux[i].getTipo(),tokensInstruccionesAux[i].getLinea()));
                    }
                }
            }
            catch(Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
            }
        }

        private void evaluarWhile(List<elementoToken> linea)
        {         
            try
            {
                int contadorCorchetes = 0;
                int contadorParentesis = 0;
                int puntoControl = 0;

                List<elementoToken> tokensCondiciones = new List<elementoToken>();
                List<elementoToken> tokensInstrucciones = new List<elementoToken>();
                List<elementoToken> tokensInstruccionesAux = new List<elementoToken>();

                string condicion = "";

                for (int i = 1; i < linea.Count; i++)
                {
                    if (linea[i].getNombre().Equals("("))
                    {
                        contadorParentesis++;
                    }
                    else if (linea[i].getNombre().Equals(")"))
                    {
                        contadorParentesis--;
                    }

                    tokensCondiciones.Add(linea[i]);

                    if (contadorParentesis == 0)
                    {
                        puntoControl = i;
                        i = linea.Count;
                    }
                }

                for (int i = puntoControl + 1; i < linea.Count; i++)
                {
                    if (linea[i].getNombre().Equals("{"))
                    {
                        contadorCorchetes++;
                    }
                    else if (linea[i].getNombre().Equals("}"))
                    {
                        contadorCorchetes--;
                    }

                    tokensInstrucciones.Add(new elementoToken(linea[i].getNombre(), linea[i].getTipo(), linea[i].getLinea()));
                    tokensInstruccionesAux.Add(new elementoToken(linea[i].getNombre(), linea[i].getTipo(), linea[i].getLinea()));

                    if (contadorCorchetes == 0)
                    {
                        i = linea.Count;
                    }
                }

                for (int i = 0; i < tokensCondiciones.Count; i++)
                {
                    if (condicion.Equals(""))
                    {
                        condicion = tokensCondiciones[i].getNombre();
                    }
                    else
                    {
                        condicion = condicion + " " + tokensCondiciones[i].getNombre();
                    }
                }

                while (evaluarCondicion(reemplazarVariablesCondicion(condicion)).Equals("True") && valorStop == false)
                {
                    compilarInstrucciones(tokensInstrucciones);

                    tokensInstrucciones.Clear();

                    for (int i = 0; i < tokensInstruccionesAux.Count; i++)
                    {
                        tokensInstrucciones.Add(new elementoToken(tokensInstruccionesAux[i].getNombre(), tokensInstruccionesAux[i].getTipo(), tokensInstruccionesAux[i].getLinea()));
                    }
                }
            }
            catch (Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
            }
        }

        private void evaluarDoWhile(List<elementoToken> linea)
        {
            try
            {
                int contadorCorchetes = 0;
                int contadorParentesis = 0;
                int puntoControl = 0;

                List<elementoToken> tokensCondiciones = new List<elementoToken>();
                List<elementoToken> tokensInstrucciones = new List<elementoToken>();
                List<elementoToken> tokensInstruccionesAux = new List<elementoToken>();

                string condicion = "";

                for (int i = 1; i < linea.Count; i++)
                {
                    if (linea[i].getNombre().Equals("{"))
                    {
                        contadorCorchetes++;
                    }
                    else if (linea[i].getNombre().Equals("}"))
                    {
                        contadorCorchetes--;
                    }

                    tokensInstrucciones.Add(new elementoToken(linea[i].getNombre(), linea[i].getTipo(), linea[i].getLinea()));
                    tokensInstruccionesAux.Add(new elementoToken(linea[i].getNombre(), linea[i].getTipo(), linea[i].getLinea()));

                    if (contadorCorchetes == 0)
                    {
                        puntoControl = i;
                        i = linea.Count;
                    }
                }

                for (int i = puntoControl + 2; i < linea.Count; i++)
                {
                    if (linea[i].getNombre().Equals("("))
                    {
                        contadorParentesis++;
                    }
                    else if (linea[i].getNombre().Equals(")"))
                    {
                        contadorParentesis--;
                    }

                    tokensCondiciones.Add(linea[i]);

                    if (contadorParentesis == 0)
                    {
                        i = linea.Count;
                    }
                }

                for (int i = 0; i < tokensCondiciones.Count; i++)
                {
                    if (condicion.Equals(""))
                    {
                        condicion = tokensCondiciones[i].getNombre();
                    }
                    else
                    {
                        condicion = condicion + " " + tokensCondiciones[i].getNombre();
                    }
                }

                do
                {
                    compilarInstrucciones(tokensInstrucciones);

                    tokensInstrucciones.Clear();

                    for (int i = 0; i < tokensInstruccionesAux.Count; i++)
                    {
                        tokensInstrucciones.Add(new elementoToken(tokensInstruccionesAux[i].getNombre(), tokensInstruccionesAux[i].getTipo(), tokensInstruccionesAux[i].getLinea()));
                    }
                }
                while (evaluarCondicion(reemplazarVariablesCondicion(condicion)).Equals("True") && valorStop == false);

            }
            catch (Exception ex)
            {
                areaResultado.AppendText("Error: " + ex.Message + "\n");
                areaResultado.SelectionStart = areaResultado.Text.Length;
                areaResultado.ScrollToCaret();
                valorStop = true;
                return;
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

        private List<elementoToken> generarTokensConcatenacion(string valor)
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
                areaResultado.AppendText("Error: Conflictos al generar tokens de asignacion" + "\n");
                valorStop = true;
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

        private List<elementoToken> generarTokensCondicion(string valor)
        {
            List<elementoToken> tokens = new List<elementoToken>();

            string codigo = valor;
            var gramatica = new gramaticaCondicion();

            var parser = new Parser(gramatica);
            var arbol = parser.Parse(codigo);

            if (arbol.Root == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    Console.WriteLine(arbol.ParserMessages[i].Message);
                }
                areaResultado.AppendText("Error: Conflictos al generar tokens de condicion" + "\n");
                valorStop = true;
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

        private List<elementoToken> generarTokensValor(string valor)
        {
            List<elementoToken> tokens = new List<elementoToken>();

            string codigo = valor;
            var gramatica = new gramaticaValor();

            var parser = new Parser(gramatica);
            var arbol = parser.Parse(codigo);

            if (arbol.Root == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    Console.WriteLine(arbol.ParserMessages[i].Message);
                }
                areaResultado.AppendText("Error: Conflictos al generar tokens de condicion" + "\n");
                valorStop = true;
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

        private string reemplazarVariablesCondicion(string expresion)
        {
            string resultado = "";
            List<elementoToken> valoresVariable = new List<elementoToken>();

            valoresVariable = generarTokensCondicion(expresion);

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
                            valoresVariable[j].setNombre("\""+variablesString[k].getValor().ToString()+"\"");
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
                            valoresVariable[j].setNombre("'"+variablesChar[k].getValor().ToString()+"'");
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
                            valoresVariable[j].setNombre(variablesBoolean[k].getValor().ToString().ToLower());
                        }
                    }
                }
            }

            resultado = valoresVariable[0].getNombre();

            for(int i = 1; i < valoresVariable.Count; i++)
            {
                resultado = resultado + " " + valoresVariable[i].getNombre();
            }

            return resultado;
        }

        private string reemplazarVariablesValor(string expresion)
        {
            string resultado = "";
            List<elementoToken> valoresVariable = new List<elementoToken>();

            valoresVariable = generarTokensValor(expresion);

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
                            valoresVariable[j].setNombre("\"" + variablesString[k].getValor().ToString() + "\"");
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
                            valoresVariable[j].setNombre("'" + variablesChar[k].getValor().ToString() + "'");
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
                            valoresVariable[j].setNombre(variablesBoolean[k].getValor().ToString().ToLower());
                        }
                    }
                }
            }

            resultado = valoresVariable[0].getNombre();

            for (int i = 1; i < valoresVariable.Count; i++)
            {
                resultado = resultado + " " + valoresVariable[i].getNombre();
            }

            return resultado;
        }

        private string evaluarExpresion(string expression)
        {
            VsaEngine engine = VsaEngine.CreateEngine();
            try
            {
                object o = Eval.JScriptEvaluate(expression, engine);
                return System.Convert.ToDouble(o).ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: "+ex.Message);
                return "Error";
            }
            engine.Close();
        }

        private string evaluarCondicion(string expression)
        {
            VsaEngine engine = VsaEngine.CreateEngine();
            try
            {
                object o = Eval.JScriptEvaluate(expression, engine);
                return System.Convert.ToString(o);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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
            //this.Location = new Point(0, 0);
            //this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            maximizar.Visible = false;
            restaurar.Visible = true;
        }

        private void Restaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            //this.Location = new Point(0, 0);
            //this.Size = new Size(1338, 781);
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Size = new Size(1338, 781);
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

        private void entrada_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.V)
            {
                entrada.Text += (string)Clipboard.GetData("Text");
                e.Handled = true;
            }
        }

        private void cambiarPanelEstado()
        {
            if (valorStop == true)
            {
                panelEstado.GradientBottomLeft = Color.Red;
                panelEstado.GradientBottomRight = Color.Red;
                panelEstado.GradientTopLeft = Color.Red;
                panelEstado.GradientTopRight = Color.FromArgb(255, 128, 0);
            }
            else
            {
                panelEstado.GradientBottomLeft = Color.FromArgb(16, 169, 104);
                panelEstado.GradientBottomRight = Color.FromArgb(16, 169, 104);
                panelEstado.GradientTopLeft = Color.FromArgb(16, 169, 104);
                panelEstado.GradientTopRight = Color.FromArgb(46, 230, 71);
            }
        }
    }
}
