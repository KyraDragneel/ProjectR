using compiladorR.Analisis.Gramaticas;
using compiladorR.Analisis.Semantica;
using compiladorR.Compilacion;
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
        private string addButtonFullPath = "C:\\Users\\maryf\\Desktop\\ProjectR\\recursos\\add.png";
        private string closeButtonFullPath = "C:\\Users\\maryf\\Desktop\\ProjectR\\recursos\\cerrar.png";
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

                variables = deteccionVariables.detectarVariables(lista);

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
                    List<string> instrucciones = new List<string>();

                    compiladorInstrucciones.compilarInstrucciones(lista);

                    for (int i = 0; i < instrucciones.Count; i++)
                    {
                        Console.WriteLine(instrucciones[i]);
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

    }
}
