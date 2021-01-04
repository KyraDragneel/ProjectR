using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosDatos
{
    class tipoDouble
    {
        private string nombre;
        private double valor;

        public tipoDouble()
        {
            nombre = "";
        }

        public tipoDouble(string nombre)
        {
            this.nombre = nombre;
        }

        public tipoDouble(string nombre, double valor)
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

        public double getValor()
        {
            return valor;
        }

        public void setValor(double valor)
        {
            this.valor = valor;
        }
    }
}
