using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Gramaticas
{
    class gramaticaValor : Grammar
    {
        public static class noTerminales
        {
            public const string Raiz = "<raiz>";
            public const string TipoDato = "<tipo-dato>";
            public const string Valor = "<valor>";
            public const string ValorLogico = "<valor-logico>";
            public const string Expresion = "<expresion>";
            //public const string ContenidoArreglo = "<contenido-arreglo>";
            public const string OperadorAritmetico = "<operador-aritmetico>";
            public const string Relacion = "<relacion>";
            public const string OperadorRelacional = "<operador-ralacional>";
            public const string OperacionLogica = "<operacion-logica>";
            public const string OperadorLogico = "<operador-logico>";
        }

        public static class terminales
        {
            public const string Null = "null";
            public const string True = "true";
            public const string False = "false";

            public const string And = "&&";
            public const string Or = "||";
            public const string Not = "!";

            public const string IgualIgual = "==";
            public const string Diferente = "!=";
            public const string Mayor = ">";
            public const string MayorIgual = ">=";
            public const string Menor = "<";
            public const string MenorIgual = "<=";

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
            public const string Nombre = "id";
            public const string NombreRegex = "([a-zA-Z]|_*[a-zA-Z]){1}[a-zA-Z0-9_]*";
            public const string Numero = "numero";
            public const string NumeroRegex = "\\d+[f|d]?(\\.\\d+[f|d]?)?";
            public const string String = "string";
            public const string StringRegex = "\"[^\"]*\"";
            public const string Char = "char";
            public const string CharRegex = "\'[^\']\'";
        }

        public gramaticaValor() : base()
        {
            #region noTerminales
            var raiz = new NonTerminal(noTerminales.Raiz);
            var valor = new NonTerminal(noTerminales.Valor);
            var valorLogico = new NonTerminal(noTerminales.ValorLogico);
            var expresion = new NonTerminal(noTerminales.Expresion);
            var operadorAritmetico = new NonTerminal(noTerminales.OperadorAritmetico);
            var relacion = new NonTerminal(noTerminales.Relacion);
            var operadorRelacional = new NonTerminal(noTerminales.OperadorRelacional);
            var operacionLogica = new NonTerminal(noTerminales.OperacionLogica);
            var operadorLogico = new NonTerminal(noTerminales.OperadorLogico);
            #endregion

            #region Terminales

            #region Palabras Reservadas
            var null_ = ToTerm(terminales.Null);
            var true_ = ToTerm(terminales.True);
            var false_ = ToTerm(terminales.False);
            #endregion

            #region Simbolos
            var parentesisAbrir_ = ToTerm(terminales.ParentesisAbrir);
            var parentesisCerrar_ = ToTerm(terminales.ParentesisCerrar);
            var corcheteAbrir_ = ToTerm(terminales.CorcheteAbrir);
            var corcheteCerrar_ = ToTerm(terminales.CorcheteCerrar);
            #endregion

            #region Operadores logicos
            var and_ = ToTerm(terminales.And);
            var or_ = ToTerm(terminales.Or);
            var not_ = ToTerm(terminales.Not);
            #endregion

            #region Operadores relacionales
            var igualIgual_ = ToTerm(terminales.IgualIgual);
            var diferente_ = ToTerm(terminales.Diferente);
            var mayor_ = ToTerm(terminales.Mayor);
            var mayorIgual_ = ToTerm(terminales.MayorIgual);
            var menor_ = ToTerm(terminales.Menor);
            var menorIgual_ = ToTerm(terminales.MenorIgual);
            #endregion

            #region Operadores aritmeticos
            var mas_ = ToTerm(terminales.Mas);
            var menos_ = ToTerm(terminales.Menos);
            var por_ = ToTerm(terminales.Por);
            var entre_ = ToTerm(terminales.Entre);
            var modulo_ = ToTerm(terminales.Modulo);
            #endregion

            #endregion

            #region ExpresionesRegulares
            var nombre = new RegexBasedTerminal(expresionesRegulares.Nombre, expresionesRegulares.NombreRegex);
            var numero = new RegexBasedTerminal(expresionesRegulares.Numero, expresionesRegulares.NumeroRegex);
            var stringRegex = new RegexBasedTerminal(expresionesRegulares.String, expresionesRegulares.StringRegex);
            var charRegex = new RegexBasedTerminal(expresionesRegulares.Char, expresionesRegulares.CharRegex);
            #endregion

            #region Reglas

            raiz.Rule = valor;

            valor.Rule = null_ | charRegex | valorLogico | expresion | operacionLogica;

            valorLogico.Rule = true_ | false_;

            expresion.Rule = numero | numero + operadorAritmetico + expresion |
                stringRegex | stringRegex + operadorAritmetico + expresion |
                nombre | nombre + operadorAritmetico + expresion |
                parentesisAbrir_ + expresion + parentesisCerrar_ |
                parentesisAbrir_ + expresion + parentesisCerrar_ + operadorAritmetico + expresion |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ + operadorAritmetico + expresion |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ + corcheteAbrir_ + expresion + corcheteCerrar_ |
                nombre + corcheteAbrir_ + expresion + corcheteCerrar_ + corcheteAbrir_ + expresion + corcheteCerrar_ + operadorAritmetico + expresion;

            operadorAritmetico.Rule = mas_ | menos_ | por_ | entre_ | modulo_;

            operacionLogica.Rule = valorLogico | valorLogico + operadorLogico + operacionLogica |
                relacion | relacion + operadorLogico + operacionLogica |
                parentesisAbrir_ + valorLogico + operadorLogico + operacionLogica + parentesisCerrar_ |
                parentesisAbrir_ + valorLogico + operadorLogico + operacionLogica + parentesisCerrar_ + operadorLogico + operacionLogica |
                parentesisAbrir_ + relacion + operadorLogico + operacionLogica + parentesisCerrar_ |
                parentesisAbrir_ + relacion + operadorLogico + operacionLogica + parentesisCerrar_ + operadorLogico + operacionLogica |
                not_ + operacionLogica;

            operadorLogico.Rule = and_ | or_;

            relacion.Rule = valor + operadorRelacional + valor |
                parentesisAbrir_ + valor + operadorRelacional + valor + parentesisCerrar_;

            operadorRelacional.Rule = igualIgual_ | diferente_ | menorIgual_ | menor_ | mayorIgual_ | mayor_;

            #endregion
            Root = raiz;
        }
    }
}
