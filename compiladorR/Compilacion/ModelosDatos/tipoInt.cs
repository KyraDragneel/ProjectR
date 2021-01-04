using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosDatos
{
    class tipoInt
    {
        private string nombre;
        private int valor;

        public tipoInt()
        {
            nombre = "";
        }

        public tipoInt(string nombre)
        {
            this.nombre = nombre;
        }

        public tipoInt(string nombre, int valor)
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

        public int getValor()
        {
            return valor;
        }

        public void setValor(int valor)
        {
            this.valor = valor;
        }
    }
}
