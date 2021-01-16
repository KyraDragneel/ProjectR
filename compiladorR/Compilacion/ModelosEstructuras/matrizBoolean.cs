using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class matrizBoolean
    {
        private string nombre;
        private bool[,] matriz;

        public matrizBoolean()
        {
            this.nombre = "";
            this.matriz = new bool[0, 0];
        }

        public matrizBoolean(string nombre)
        {
            this.nombre = nombre;
            this.matriz = new bool[0, 0];
        }

        public matrizBoolean(string nombre, bool[,] matriz)
        {
            this.nombre = nombre;
            this.matriz = new bool[matriz.GetLength(0), matriz.GetLength(1)];
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = matriz[i, j];
                }
            }
        }

        public matrizBoolean(string nombre, int filas, int columnas)
        {
            this.nombre = nombre;
            this.matriz = new bool[filas, columnas];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public bool[,] getMatriz()
        {
            return this.matriz;
        }

        public bool getElemento(int fila, int columna)
        {
            return this.matriz[fila, columna];
        }

        public void setMatriz(bool[,] matriz)
        {
            this.matriz = new bool[matriz.GetLength(0), matriz.GetLength(1)];
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
            this.matriz = new bool[filas, columnas];
        }

        public void setElemento(int fila, int columna, bool valor)
        {
            this.matriz[fila, columna] = valor;
        }
    }
}
