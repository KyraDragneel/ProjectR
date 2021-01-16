using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class matrizDouble
    {
        private string nombre;
        private double[,] matriz;

        public matrizDouble()
        {
            this.nombre = "";
            this.matriz = new double[0, 0];
        }

        public matrizDouble(string nombre)
        {
            this.nombre = nombre;
            this.matriz = new double[0, 0];
        }

        public matrizDouble(string nombre, double[,] matriz)
        {
            this.nombre = nombre;
            this.matriz = new double[matriz.GetLength(0), matriz.GetLength(1)];
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = matriz[i, j];
                }
            }
        }

        public matrizDouble(string nombre, int filas, int columnas)
        {
            this.nombre = nombre;
            this.matriz = new double[filas, columnas];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public double[,] getMatriz()
        {
            return this.matriz;
        }

        public double getElemento(int fila, int columna)
        {
            return this.matriz[fila, columna];
        }

        public void setMatriz(double[,] matriz)
        {
            this.matriz = new double[matriz.GetLength(0), matriz.GetLength(1)];
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
            this.matriz = new double[filas, columnas];
        }

        public void setElemento(int fila, int columna, double valor)
        {
            this.matriz[fila, columna] = valor;
        }
    }
}
