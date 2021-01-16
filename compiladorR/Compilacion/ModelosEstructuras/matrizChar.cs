using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class matrizChar
    {
        private string nombre;
        private char[,] matriz;

        public matrizChar()
        {
            this.nombre = "";
            this.matriz = new char[0, 0];
        }

        public matrizChar(string nombre)
        {
            this.nombre = nombre;
            this.matriz = new char[0, 0];
        }

        public matrizChar(string nombre, char[,] matriz)
        {
            this.nombre = nombre;
            this.matriz = new char[matriz.GetLength(0), matriz.GetLength(1)];
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = matriz[i, j];
                }
            }
        }

        public matrizChar(string nombre, int filas, int columnas)
        {
            this.nombre = nombre;
            this.matriz = new char[filas, columnas];

            for(int i = 0; i < this.matriz.GetLength(0); i++)
            {
                for(int j = 0; j < this.matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = ' ';
                }
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

        public char[,] getMatriz()
        {
            return this.matriz;
        }

        public char getElemento(int fila, int columna)
        {
            return this.matriz[fila, columna];
        }

        public void setMatriz(char[,] matriz)
        {
            this.matriz = new char[matriz.GetLength(0), matriz.GetLength(1)];
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
            this.matriz = new char[filas, columnas];

            for (int i = 0; i < this.matriz.GetLength(0); i++)
            {
                for (int j = 0; j < this.matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = ' ';
                }
            }
        }

        public void setElemento(int fila, int columna, char valor)
        {
            this.matriz[fila, columna] = valor;
        }
    }
}
