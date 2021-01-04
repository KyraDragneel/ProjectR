using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosDatos
{
    class tipoBoolean
    {
        private string nombre;
        private bool valor;

        public tipoBoolean()
        {
            nombre = "";
        }

        public tipoBoolean(string nombre)
        {
            this.nombre = nombre;
        }

        public tipoBoolean(string nombre, bool valor)
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

        public bool getValor()
        {
            return valor;
        }

        public void setValor(bool valor)
        {
            this.valor = valor;
        }
    }
}
