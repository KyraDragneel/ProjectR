using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiladorR.Analisis.Semantica
{
    class ordenacionErrores
    {
        public static List<string> ordenarErrores(List<string> errores)
        {
            List<string> erroresOrdenados = new List<string>();
            elementoError[] elementosErrores = new elementoError[errores.Count];
            string[] arregloAuxiliar;
            string cadenaAuxiliar;
            elementoError objetoAuxiliar;

            for (int i = 0; i < errores.Count; i++)
            {
                arregloAuxiliar = errores[i].Split(' ');
                cadenaAuxiliar = arregloAuxiliar[0];

                for (int j = 1; j < arregloAuxiliar.Length - 1; j++)
                {
                    cadenaAuxiliar = cadenaAuxiliar + " " + arregloAuxiliar[j];
                }

                cadenaAuxiliar = cadenaAuxiliar + " ";

                elementosErrores[i] = new elementoError(cadenaAuxiliar, Int32.Parse(arregloAuxiliar[arregloAuxiliar.Length - 1]));
            }

            for (int i = 0; i < elementosErrores.Length; i++)
            {
                for (int j = i + 1; j < elementosErrores.Length; j++)
                {
                    if (elementosErrores[i].getLinea() > elementosErrores[j].getLinea())
                    {
                        objetoAuxiliar = elementosErrores[i];
                        elementosErrores[i] = elementosErrores[j];
                        elementosErrores[j] = objetoAuxiliar;
                    }
                }
            }

            for (int i = 0; i < elementosErrores.Length; i++)
            {
                erroresOrdenados.Add(elementosErrores[i].getMensaje() + elementosErrores[i].getLinea());
            }

            return erroresOrdenados;
        }
    }
}
