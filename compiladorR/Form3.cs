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
    public partial class Form3 : Form
    {
        private static string valor = "";

        public Form3()
        {
            InitializeComponent();
        }

        public static string Show()
        {
            var box = new Form3();
            box.CenterToScreen();
            box.ShowDialog();

            return valor;
        }

        private void si_Click(object sender, EventArgs e)
        {
            valor = "si";
            this.Close();
        }

        private void no_Click(object sender, EventArgs e)
        {
            valor = "no";
            this.Close();
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                valor = "si";
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            valor = "no";
            this.Close();
        }
    }
}
