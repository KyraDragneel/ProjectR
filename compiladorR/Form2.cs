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

        public static string Show()
        {
            var box = new Form2();
            box.ShowDialog();

            return valor;
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            valor = textBox1.Text;
            this.Close();
        }
    }
}
