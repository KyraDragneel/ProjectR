using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class arregloString
    {
        private string nombre;
        private string[] arreglo;

        public arregloString()
        {
            this.nombre = "";
            this.arreglo = new string[0];
        }

        public arregloString(string nombre)
        {
            this.nombre = nombre;
            this.arreglo = new string[0];
        }

        public arregloString(string nombre, string[] arreglo)
        {
            this.nombre = nombre;
            this.arreglo = new string[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public arregloString(string nombre, int longitud)
        {
            this.nombre = nombre;
            this.arreglo = new string[longitud];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public string[] getArreglo()
        {
            return this.arreglo;
        }

        public string getElemento(int posicion)
        {
            return this.arreglo[posicion];
        }

        public void setArreglo(string[] arreglo)
        {
            this.arreglo = new string[arreglo.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                this.arreglo[i] = arreglo[i];
            }
        }

        public void setLongitud(int longitud)
        {
            this.arreglo = new string[longitud];
        }

        public void setElemento(int posicion, string valor)
        {
            this.arreglo[posicion] = valor;
        }
    }
}
