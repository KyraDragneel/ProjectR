using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class arregloBoolean
    {
        private string nombre;
        private bool[] arreglo;

        public arregloBoolean()
        {
            this.nombre = "";
            this.arreglo = new bool[0];
        }

        public arregloBoolean(string nombre)
        {
            this.nombre = nombre;
            this.arreglo = new bool[0];
        }

        public arregloBoolean(string nombre, bool[] arreglo)
        {
            this.nombre = nombre;
            this.arreglo = new bool[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public arregloBoolean(string nombre, int longitud)
        {
            this.nombre = nombre;
            this.arreglo = new bool[longitud];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public bool[] getArreglo()
        {
            return this.arreglo;
        }

        public bool getElemento(int posicion)
        {
            return this.arreglo[posicion];
        }

        public void setArreglo(bool[] arreglo)
        {
            this.arreglo = new bool[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public void setLongitud(int longitud)
        {
            this.arreglo = new bool[longitud];
        }

        public void setElemento(int posicion, bool valor)
        {
            this.arreglo[posicion] = valor;
        }
    }
}
