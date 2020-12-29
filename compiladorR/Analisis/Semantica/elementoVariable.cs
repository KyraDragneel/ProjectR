using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class elementoVariable
    {
        private string tipo;
        private string nombre;
        private string valor;
        private int linea;

        public elementoVariable()
        {
            tipo = "";
            nombre = "";
            valor = "";
            linea = 0;
        }

        public elementoVariable(string tipo, string nombre, string valor, int linea)
        {
            this.tipo = tipo;
            this.nombre = nombre;
            this.valor = valor;
            this.linea = linea;
        }

        public void setTipo(string tipo)
        {
            this.tipo = tipo;
        }

        public string getTipo()
        {
            return tipo;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public string getNombre()
        {
            return nombre;
        }

        public void setValor(string valor)
        {
            this.valor = valor;
        }

        public string getValor()
        {
            return valor;
        }

        public void setLinea(int linea)
        {
            this.linea = linea;
        }

        public int getLinea()
        {
            return linea;
        }
    }
}
