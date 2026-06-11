
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using tp1;

namespace tpfinal
{

    public class Estrategia
    {

        public String Consulta1(List<string> datos)
        {

                List<Dato> listaHeap = new List<Dato>();
                List<Dato> listaOrden = new List<Dato>();

                Stopwatch reloj = new Stopwatch();

                // Tiempo Heap
                reloj.Start();

                BuscarConHeap(datos, 5, listaHeap);

                reloj.Stop();

                long tiempoHeap = reloj.ElapsedMilliseconds;

                // Reiniciar reloj
                reloj.Reset();

                // Tiempo Ordenamiento
                reloj.Start();

                BuscarConOtro(datos, 5, listaOrden);

                reloj.Stop();

                long tiempoOrden = reloj.ElapsedMilliseconds;

            string result = "TIEMPOS DE BUSQUEDA DE LOS 5 ELEMENTOS CON MAYOR CANTIDAD DE OCURRENCIAS\n\n";

            result += "Tiempo utilizando Heap: " + tiempoHeap + " ms\n";
            result += "Tiempo utilizando Ordenamiento: " + tiempoOrden + " ms";

            return result;
        }


        public String Consulta2(List<string> datos)
        {
            List<Dato> lista = ContarOcurrencias(datos);

            ConstruirHeap(lista);

            string resultado = "Camino a la hoja izquierda de la Heap:\n\n";

            int actual = 0;

            while (actual < lista.Count)
            {
                resultado += lista[actual].texto +
                             " (Ocurrencia:" +
                             lista[actual].ocurrencia +
                             ")\n";

                actual = 2 * actual + 1;
            }

            return resultado;
        }



        public String Consulta3(List<string> datos)
        {
            List<Dato> lista = ContarOcurrencias(datos);

            ConstruirHeap(lista);

            string result = "NODOS DE LA HEAP POR NIVEL:\n\n";

            int nivel = 0;
            int i = 0;
            int elementosNivel = 1;

            while (i < lista.Count)
            {
                result += "Nivel " + nivel + "\n";

                int fin = i + elementosNivel;

                if (fin > lista.Count)
                {
                    fin = lista.Count;
                }

                for (int j = i; j < fin; j++)
                {
                    result += "-" + lista[j].texto +
                              " (Ocurrencias:" +
                              lista[j].ocurrencia + ")\n";
                }

                result += "\n";

                i = fin;
                nivel++;
                elementosNivel = elementosNivel * 2;
            }

            return result;
        }


        public void BuscarConOtro(List<string> datos, int cantidad, List<Dato> collected)
        {
            List<Dato> lista = ContarOcurrencias(datos);

            for (int i = 0; i < lista.Count - 1; i++)
            {
                int max = i;

                for (int j = i + 1; j < lista.Count; j++)
                {
                    if (lista[j].ocurrencia > lista[max].ocurrencia)
                    {
                        max = j;
                    }
                }

                Dato aux = lista[i];
                lista[i] = lista[max];
                lista[max] = aux;
            }

            for (int i = 0; i < cantidad && i < lista.Count; i++)
            {
                collected.Add(lista[i]);
            }
        }


        public void BuscarConHeap(List<string> datos, int cantidad, List<Dato> collected)
        {
            List<Dato> lista = ContarOcurrencias(datos);

            ConstruirHeap(lista);

            int tamaño = lista.Count;

            for (int i = 0; i < cantidad && tamaño > 0; i++)
            {
                collected.Add(lista[0]);

                lista[0] = lista[tamaño - 1];

                tamaño--;

                ReordenarHeap(lista, tamaño, 0);
            }
        }

        private List<Dato> ContarOcurrencias(List<string> datos)
        {
            List<Dato> lista = new List<Dato>();

            for (int i = 0; i < datos.Count; i++)
            {
                bool encontrado = false;

                for (int j = 0; j < lista.Count; j++)
                {
                    if (lista[j].texto == datos[i])
                    {
                        lista[j].ocurrencia++;
                        encontrado = true;
                    }
                }

                if (!encontrado)
                {
                    lista.Add(new Dato(1, datos[i]));
                }
            }

            return lista;
        }

        private void ReordenarHeap(List<Dato> lista, int n, int i)
        {
            int mayor = i;

            int izquierda = 2 * i + 1;
            int derecha = 2 * i + 2;

            if (izquierda < n && lista[izquierda].ocurrencia > lista[mayor].ocurrencia)
            {
                mayor = izquierda;
            }

            if (derecha < n && lista[derecha].ocurrencia > lista[mayor].ocurrencia)
            {
                mayor = derecha;
            }

            if (mayor != i)
            {
                Dato aux = lista[i];
                lista[i] = lista[mayor];
                lista[mayor] = aux;

                ReordenarHeap(lista, n, mayor);
            }
        }
        private void ConstruirHeap(List<Dato> lista)
        {
            for (int i = lista.Count / 2 - 1; i >= 0; i--)
            {
                ReordenarHeap(lista, lista.Count, i);
            }
        }
    }
}

