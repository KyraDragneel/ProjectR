using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class arregloFloat
    {
        private string nombre;
        private float[] arreglo;

        public arregloFloat()
        {
            this.nombre = "";
            this.arreglo = new float[0];
        }

        public arregloFloat(string nombre)
        {
            this.nombre = nombre;
            this.arreglo = new float[0];
        }

        public arregloFloat(string nombre, float[] arreglo)
        {
            this.nombre = nombre;
            this.arreglo = new float[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public arregloFloat(string nombre, int longitud)
        {
            this.nombre = nombre;
            this.arreglo = new float[longitud];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public float[] getArreglo()
        {
            return this.arreglo;
        }

        public float getElemento(int posicion)
        {
            return this.arreglo[posicion];
        }

        public void setArreglo(float[] arreglo)
        {
            this.arreglo = new float[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public void setLongitud(int longitud)
        {
            this.arreglo = new float[longitud];
        }

        public void setElemento(int posicion, float valor)
        {
            this.arreglo[posicion] = valor;
        }
    }
}
