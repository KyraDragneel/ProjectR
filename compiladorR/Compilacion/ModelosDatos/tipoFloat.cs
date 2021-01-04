using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosDatos
{
    class tipoFloat
    {
        private string nombre;
        private float valor;

        public tipoFloat()
        {
            nombre = "";
        }

        public tipoFloat(string nombre)
        {
            this.nombre = nombre;
        }

        public tipoFloat(string nombre, float valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }

        public string getNombre()
        {
            return nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public float getValor()
        {
            return valor;
        }

        public void setValor(float valor)
        {
            this.valor = valor;
        }
    }
}
