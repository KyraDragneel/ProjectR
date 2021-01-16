using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class arregloChar
    {
        private string nombre;
        private char[] arreglo;

        public arregloChar()
        {
            this.nombre = "";
            this.arreglo = new char[0];
        }

        public arregloChar(string nombre)
        {
            this.nombre = nombre;
            this.arreglo = new char[0];
        }

        public arregloChar(string nombre, char[] arreglo)
        {
            this.nombre = nombre;
            this.arreglo = new char[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public arregloChar(string nombre, int longitud)
        {
            this.nombre = nombre;
            this.arreglo = new char[longitud];

            for(int i = 0; i < this.arreglo.Length; i++)
            {
                this.arreglo[i] = ' ';
            }
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public char[] getArreglo()
        {
            return this.arreglo;
        }

        public char getElemento(int posicion)
        {
            return this.arreglo[posicion];
        }

        public void setArreglo(char[] arreglo)
        {
            this.arreglo = new char[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public void setLongitud(int longitud)
        {
            this.arreglo = new char[longitud];

            for (int i = 0; i < this.arreglo.Length; i++)
            {
                this.arreglo[i] = ' ';
            }
        }

        public void setElemento(int posicion, char valor)
        {
            this.arreglo[posicion] = valor;
        }
    }
}
