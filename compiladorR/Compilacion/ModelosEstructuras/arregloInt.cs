using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class arregloInt
    {
        private string nombre;
        private int[] arreglo;

        public arregloInt()
        {
            this.nombre = "";
            this.arreglo = new int[0];
        }

        public arregloInt(string nombre)
        {
            this.nombre = nombre;
            this.arreglo = new int[0];
        }

        public arregloInt(string nombre, int[] arreglo)
        {
            this.nombre = nombre;
            this.arreglo = new int[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public arregloInt(string nombre,int longitud)
        {
            this.nombre = nombre;
            this.arreglo = new int[longitud];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public int[] getArreglo()
        {
            return this.arreglo;
        }

        public int getElemento(int posicion)
        {
            return this.arreglo[posicion];
        }

        public void setArreglo(int[] arreglo)
        {
            this.arreglo = new int[arreglo.Length];
            for(int i = 0; i < arreglo.Length;i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public void setLongitud(int longitud)
        {
            this.arreglo = new int[longitud];
        }

        public void setElemento(int posicion,int valor)
        {
            this.arreglo[posicion] = valor;
        }
    }
}
