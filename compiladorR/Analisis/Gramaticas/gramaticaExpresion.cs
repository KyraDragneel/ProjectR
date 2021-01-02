using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Gramaticas
{
    class gramaticaExpresion : Grammar
    {
        public static class noTerminales
        {
            public const string Raiz = "<raiz>";
            public const string Expresion = "<expresion>";
            public const string OperadorAritmetico = "<operador-aritmetico>";
        }

        public static class terminales
        {
            public const string Mas = "+";
            public const string Menos = "-";
            public const string Por = "*";
            public const string Entre = "/";
            public const string Modulo = "%";

            public const string ParentesisAbrir = "(";
            public const string ParentesisCerrar = ")";
            public const string CorcheteAbrir = "[";
            public const string CorcheteCerrar = "]";

        }

        public static class expresionesRegulares
        {
            public const string Comentario = "comentario";
            public const string ComentarioRegex = "(\\/\\*(\\s*|.*?)*\\*\\/)|(\\/\\/.*)";
            public const string Nombre = "id";
            public const string NombreRegex = "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*";
            public const string Numero = "numero";
            public const string NumeroRegex = "\\d+[f|d]?(\\.\\d+[f|d]?)?";
            public const string String = "string";
            public const string StringRegex = "\"[^\"]*\"";
            public const string Char = "char";
            public const string CharRegex = "\'[^\']\'";
        }

        public gramaticaExpresion() : base()
        {
            #region noTerminales
            var raiz = new NonTerminal(noTerminales.Raiz);
            var expresion = new NonTerminal(noTerminales.Expresion);
            var operadorAritmetico = new NonTerminal(noTerminales.OperadorAritmetico);
            #endregion

            #region Terminales

            #region Simbolos
            var parentesisAbrir_ = ToTerm(terminales.ParentesisAbrir);
            var parentesisCerrar_ = ToTerm(terminales.ParentesisCerrar);
            var corcheteAbrir_ = ToTerm(terminales.CorcheteAbrir);
            var corcheteCerrar_ = ToTerm(terminales.CorcheteCerrar);
            #endregion

            #region Operadores aritmeticos
            var mas_ = ToTerm(terminales.Mas);
            var menos_ = ToTerm(terminales.Menos);
            var por_ = ToTerm(terminales.Por);
            var entre_ = ToTerm(terminales.Entre);
            var modulo_ = ToTerm(terminales.Modulo);
            #endregion

            #region ExpresionesRegulares
            var nombre = new RegexBasedTerminal(expresionesRegulares.Nombre, expresionesRegulares.NombreRegex);
            var numero = new RegexBasedTerminal(expresionesRegulares.Numero, expresionesRegulares.NumeroRegex);
            #endregion

            #endregion

            #region Reglas

            raiz.Rule = expresion;

            expresion.Rule = numero | numero + operadorAritmetico + expresion |
                nombre | nombre + operadorAritmetico + expresion |
                parentesisAbrir_ + expresion + parentesisCerrar_ |
                parentesisAbrir_ + expresion + parentesisCerrar_ + operadorAritmetico + expresion |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ + operadorAritmetico + expresion |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ + corcheteAbrir_ + expresion + corcheteCerrar_ |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ + corcheteAbrir_ + expresion + corcheteCerrar_ + operadorAritmetico + expresion;

            operadorAritmetico.Rule = mas_ | menos_ | por_ | entre_ | modulo_;

            #endregion
            Root = raiz;
        }
    }
}
