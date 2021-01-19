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
    public partial class Form4 : Form
    {
        bool encendido = false;

        public Form4()
        {
            InitializeComponent();
            this.CenterToScreen();
            label4.Text = "";
        }

        public static void Show()
        {
            var box = new Form4();
            box.CenterToScreen();
            box.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(encendido == false)
            {
                encendido = true;
                label4.Text = "Presiona cualquier tecla...";
            }
            else
            {
                encendido = false;
                label4.Text = "";
            }
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
