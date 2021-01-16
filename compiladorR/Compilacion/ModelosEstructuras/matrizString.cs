using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class matrizString
    {
        private string nombre;
        private string[,] matriz;

        public matrizString()
        {
            this.nombre = "";
            this.matriz = new string[0, 0];
        }

        public matrizString(string nombre)
        {
            this.nombre = nombre;
            this.matriz = new string[0, 0];
        }

        public matrizString(string nombre, string[,] matriz)
        {
            this.nombre = nombre;
            this.matriz = new string[matriz.GetLength(0), matriz.GetLength(1)];
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = matriz[i, j];
                }
            }
        }

        public matrizString(string nombre, int filas, int columnas)
        {
            this.nombre = nombre;
            this.matriz = new string[filas, columnas];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public string[,] getMatriz()
        {
            return this.matriz;
        }

        public string getElemento(int fila, int columna)
        {
            return this.matriz[fila, columna];
        }

        public void setMatriz(string[,] matriz)
        {
            this.matriz = new string[matriz.GetLength(0), matriz.GetLength(1)];
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = matriz[i, j];
                }
            }
        }

        public void setLongitud(int filas, int columnas)
        {
            this.matriz = new string[filas, columnas];
        }

        public void setElemento(int fila, int columna, string valor)
        {
            this.matriz[fila, columna] = valor;
        }
    }
}
