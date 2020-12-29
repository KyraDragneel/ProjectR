using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class elementoToken
    {
        private string nombre;
        private string tipo;
        private int linea;

        public elementoToken()
        {
            nombre = "";
            tipo = "";
            linea = 0;
        }

        public elementoToken(string nombre, string tipo, int linea)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.linea = linea;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public string getNombre()
        {
            return nombre;
        }

        public void setTipo(string tipo)
        {
            this.tipo = tipo;
        }

        public string getTipo()
        {
            return tipo;
        }

        public void setLinea(int linea)
        {
            this.linea = linea;
        }

        public int getLinea()
        {
            return linea;
        }
    }
}
