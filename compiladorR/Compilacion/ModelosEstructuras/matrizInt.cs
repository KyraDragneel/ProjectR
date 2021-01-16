using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class matrizInt
    {
        private string nombre;
        private int[,] matriz;

        public matrizInt()
        {
            this.nombre = "";
            this.matriz = new int[0,0];
        }

        public matrizInt(string nombre)
        {
            this.nombre = nombre;
            this.matriz = new int[0, 0];
        }

        public matrizInt(string nombre, int[,] matriz)
        {
            this.nombre = nombre;
            this.matriz = new int[matriz.GetLength(0),matriz.GetLength(1)];
            for(int i = 0; i < matriz.GetLength(0);i++)
            {
                for(int j = 0; j < matriz.GetLength(1);j++)
                {
                    this.matriz[i,j] = matriz[i,j];
                }
            }
        }

        public matrizInt(string nombre, int filas, int columnas)
        {
            this.nombre = nombre;
            this.matriz = new int[filas,columnas];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public int[,] getMatriz()
        {
            return this.matriz;
        }

        public int getElemento(int fila,int columna)
        {
            return this.matriz[fila,columna];
        }

        public void setMatriz(int[,] matriz)
        {
            this.matriz = new int[matriz.GetLength(0), matriz.GetLength(1)];
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
            this.matriz = new int[filas, columnas];
        }

        public void setElemento(int fila, int columna, int valor)
        {
            this.matriz[fila, columna] = valor;
        }
    }
}
