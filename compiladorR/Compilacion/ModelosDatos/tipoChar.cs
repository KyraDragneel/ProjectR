using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosDatos
{
    class tipoChar
    {
        private string nombre;
        private char valor;

        public tipoChar()
        {
            nombre = "";
        }

        public tipoChar(string nombre)
        {
            this.nombre = nombre;
        }

        public tipoChar(string nombre, char valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }

        public string getNombre()
        {
            return nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public char getValor()
        {
            return valor;
        }

        public void setValor(char valor)
        {
            this.valor = valor;
        }
    }
}
