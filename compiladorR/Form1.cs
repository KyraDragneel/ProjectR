using compiladorR.Analisis.Gramaticas;
using compiladorR.Analisis.Semantica;
using Irony.Parsing;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void run_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("Error");
                areaResultado.AppendText("Su codigo contiene errores" + "\n");
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

                variables = deteccionVariables.detectarVariables(lista);


                for (int i = 0; i < variables.Count; i++)
                {
                    //Console.WriteLine((i + 1) + "- Tipo: " + variables[i].getTipo() + " Nombre: " + variables[i].getNombre() + " Valor: " + variables[i].getValor() + " Linea: " + variables[i].getLinea());
                }

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

                areaResultado.AppendText("Analis correcto" + "\n");

                for (int i = 0; i < listaErrores.Count; i++)
                {
                    areaResultado.AppendText(listaErrores[i] + "\n");
                }
            }
        }
    }
}
