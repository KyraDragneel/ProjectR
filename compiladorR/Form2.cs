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
    public partial class Form2 : Form
    {
        private static string valor = "";
        public Form2()
        {
            InitializeComponent();
        }

        public static string Show(string tipoVariable, string nombreVariable)
        {
            var box = new Form2();
            box.CenterToScreen();
            box.nombreVar.Text = nombreVariable;
            box.tipoVariable.Text = tipoVariable;
            box.ShowDialog();

            return valor;
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            valor = textBox1.Text;
            this.Close();
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            valor = "close";
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                valor = textBox1.Text;
                this.Close();
            }
        }
    }
}
