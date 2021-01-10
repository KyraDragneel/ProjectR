using compiladorR.Analisis.Semantica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Compilacion.ModelosSentencias
{
    class sentenciaSwitch
    {
        private List<elementoToken> tokens;
        private string caso;

        public sentenciaSwitch()
        {
            tokens = new List<elementoToken>();
            caso = "";
        }

        public sentenciaSwitch(string caso, List<elementoToken> tokens)
        {
            this.tokens = new List<elementoToken>();
            for (int i = 0; i < tokens.Count; i++)
            {
                this.tokens.Add(tokens[i]);
            }
            this.caso = caso;
        }

        public sentenciaSwitch(string caso)
        {
            this.tokens = new List<elementoToken>();
            this.caso = caso;
        }

        public sentenciaSwitch(List<elementoToken> tokens)
        {
            this.tokens = new List<elementoToken>();
            for (int i = 0; i < tokens.Count; i++)
            {
                this.tokens.Add(tokens[i]);
            }
            this.caso = "";
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

        public string getCaso()
        {
            return this.caso;
        }

        public void setCaso(string caso)
        {
            this.caso = caso;
        }
    }
}
