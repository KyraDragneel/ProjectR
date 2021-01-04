using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosDatos
{
    class tipoString
    {
        private string nombre;
        private string valor;

        public tipoString()
        {
            nombre = "";
        }

        public tipoString(string nombre)
        {
            this.nombre = nombre;
        }

        public tipoString(string nombre, string valor)
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

        public string getValor()
        {
            return valor;
        }

        public void setValor(string valor)
        {
            this.valor = valor;
        }
    }
}
