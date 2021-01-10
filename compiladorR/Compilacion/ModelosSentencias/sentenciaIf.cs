using compiladorR.Analisis.Semantica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosSentencias
{
    class sentenciaIf
    {
        private List<elementoToken> tokens;
        private string condicion;
        private int codigo;

        public sentenciaIf()
        {
            tokens = new List<elementoToken>();
            condicion = "";
            codigo = 0;
        }

        public sentenciaIf(string condicion, List<elementoToken> tokens, int codigo)
        {
            this.tokens = new List<elementoToken>();
            for(int i = 0; i < tokens.Count; i++)
            {
                this.tokens.Add(tokens[i]);
            }
            this.condicion = condicion;
            this.codigo = codigo;
        }

        public sentenciaIf(string condicion)
        {
            this.tokens = new List<elementoToken>();
            this.condicion = condicion;
            this.codigo = 0;
        }

        public sentenciaIf(List<elementoToken> tokens)
        {
            this.tokens = new List<elementoToken>();
            for (int i = 0; i < tokens.Count; i++)
            {
                this.tokens.Add(tokens[i]);
            }
            this.condicion = "";
            this.codigo = 0;
        }

        public List<elementoToken> getTokens()
        {
            return this.tokens;
        }

        public elementoToken getToken(int indice)
        {
            return this.tokens[indice];
        }

        public void setTokens(List<elementoToken> tokens)
        {
            this.tokens = tokens;
        }

        public string getCondicion()
        {
            return this.condicion;
        }

        public void setCondicion(string condicion)
        {
            this.condicion = condicion;
        }

        public int getCodigo()
        {
            return this.codigo;
        }
    }
}
