
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

            string result = "Implementar";

            return result;
        }


        public String Consulta2(List<string> datos)
        {

            string result = "Implementar";

            return result;
        }



        public String Consulta3(List<string> datos)
        {
            string result = "Implementar";

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

                Heapify(lista, tamaño, 0);
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

        private void Heapify(List<Dato> lista, int n, int i)
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

                Heapify(lista, n, mayor);
            }
        }
        private void ConstruirHeap(List<Dato> lista)
        {
            for (int i = lista.Count / 2 - 1; i >= 0; i--)
            {
                Heapify(lista, lista.Count, i);
            }
        }
    }
}

