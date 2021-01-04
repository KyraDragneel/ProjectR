using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class elementoError
    {
        private string mensaje;
        private int linea;

        public elementoError()
        {
            mensaje = "";
            linea = 0;
        }

        public elementoError(string mensaje, int linea)
        {
            this.mensaje = mensaje;
            this.linea = linea;
        }

        public string getMensaje()
        {
            return mensaje;
        }

        public void setMensaje(string mensaje)
        {
            this.mensaje = mensaje;
        }

        public int getLinea()
        {
            return linea;
        }

        public void setLinea(int linea)
        {
            this.linea = linea;
        }
    }
}
