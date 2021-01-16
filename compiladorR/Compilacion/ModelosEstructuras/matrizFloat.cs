using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosEstructuras
{
    class matrizFloat
    {
        private string nombre;
        private float[,] matriz;

        public matrizFloat()
        {
            this.nombre = "";
            this.matriz = new float[0, 0];
        }

        public matrizFloat(string nombre)
        {
            this.nombre = nombre;
            this.matriz = new float[0, 0];
        }

        public matrizFloat(string nombre, float[,] matriz)
        {
            this.nombre = nombre;
            this.matriz = new float[matriz.GetLength(0), matriz.GetLength(1)];
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    this.matriz[i, j] = matriz[i, j];
                }
            }
        }

        public matrizFloat(string nombre, int filas, int columnas)
        {
            this.nombre = nombre;
            this.matriz = new float[filas, columnas];
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public float[,] getMatriz()
        {
            return this.matriz;
        }

        public float getElemento(int fila, int columna)
        {
            return this.matriz[fila, columna];
        }

        public void setMatriz(float[,] matriz)
        {
            this.matriz = new float[matriz.GetLength(0), matriz.GetLength(1)];
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
            this.matriz = new float[filas, columnas];
        }

        public void setElemento(int fila, int columna, float valor)
        {
            this.matriz[fila, columna] = valor;
        }
    }
}
