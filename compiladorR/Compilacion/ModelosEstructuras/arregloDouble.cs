using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class arregloDouble
    {
        private string nombre;
        private double[] arreglo;

        public arregloDouble()
        {
            this.nombre = "";
            this.arreglo = new double[0];
        }

        public arregloDouble(string nombre)
        {
            this.nombre = nombre;
            this.arreglo = new double[0];
        }

        public arregloDouble(string nombre, double[] arreglo)
        {
            this.nombre = nombre;
            this.arreglo = new double[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public arregloDouble(string nombre, int longitud)
        {
            this.nombre = nombre;
            this.arreglo = new double[longitud];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public double[] getArreglo()
        {
            return this.arreglo;
        }

        public double getElemento(int posicion)
        {
            return this.arreglo[posicion];
        }

        public void setArreglo(double[] arreglo)
        {
            this.arreglo = new double[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public void setLongitud(int longitud)
        {
            this.arreglo = new double[longitud];
        }

        public void setElemento(int posicion, double valor)
        {
            this.arreglo[posicion] = valor;
        }
    }
}
