using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Libreria
{
    public class Libro : IComparable<Libro>
    {
        private string titulo;
        public string TITULO { get { return this.titulo; } }
        private int contador;
        public int CONTADOR { get { return this.contador; } }
        private double precio;
        public double PRECIO { get { return this.precio; } set { this.precio = EvaluarPrecio(value); } }
        private double precioBase;
        public double PRECIOBASE { get { return this.precioBase; } }
        private const double IVA = 1.21;
        public double EvaluarPrecio(double precio)
        {
            if (precio < 0)
                return 0;
            precio = precio * IVA;
            return Math.Round(precio, 2);
        }
        private LinkedList<WordFrequency> words;
        public Libro(string titulo)
        {
            this.words = new LinkedList<WordFrequency>();
            this.titulo = titulo.ToLower();
            this.contador = 1;
            this.precio = Math.Round(9.99f * IVA, 2);
            this.precioBase = this.precio;
        }
        public Libro(string titulo, int contador)
        {
            this.words = new LinkedList<WordFrequency>();
            this.titulo = titulo.ToLower();
            this.contador = contador;
            this.precio = Math.Round(9.99f * contador * IVA, 2);
            this.precioBase = Math.Round(9.99f * IVA, 2);
        }
        public Libro(string titulo, int contador, double precioBase)
        {
            this.words = new LinkedList<WordFrequency>();
            this.titulo = titulo.ToLower();
            this.contador = contador;
            this.precio = Math.Round(precioBase * contador * IVA, 2);
            this.precioBase = Math.Round(precioBase * IVA, 2);
        }
        public Libro(Libro book)
        {
            this.words = book.words;
            this.titulo = book.titulo;
            this.contador = 1;
            this.precio = book.precioBase;
            this.precioBase = book.precioBase;
        }
        public void IncrementarContador()
        {
            this.contador++;
            this.precio = Math.Round(this.precioBase * contador, 2);
        }
        public void IncrementarContador(int newContador)
        {
            this.contador += newContador;
            this.precio = Math.Round(this.precioBase * contador, 2);
        }
        public void DecrementarContador()
        {
            this.contador--;
            this.precio = Math.Round(this.precioBase * contador, 2);
        }
        public int Size()
        {
            return this.words.Count;
        }
        public bool Load(string fileName)
        {
            StreamReader reader = null;
            string scan = "";
            string[] line = null;

            try
            {
                reader = new StreamReader(fileName); //Lanzara una excepcion si el archivo especificado no existe
            }
            catch (Exception)
            {
                return false;
            }
            while (true)
            {
                if (reader.Peek() == -1) break;
                scan = reader.ReadLine().Trim().ToLower();
                if (scan.StartsWith("@")) continue;
                line = scan.Trim().Split(" ");

                this.Add(line);
            }
            reader.Close();
            return true;
        }
        public void Revalorizar(double porcentaje)
        {
            this.precioBase = Math.Round(this.precioBase * porcentaje, 2);
            this.precio = Math.Round(this.precioBase * contador, 2);
        }
        public int GetSumOfFrequency()
        {
            int sum = 0;
            foreach (var word in this.words)
            {
                sum += word.FREQUENCY;
            }
            return sum;
        }
        public bool Add(string WordFrequency)
        {
            WordFrequency wf = new WordFrequency(WordFrequency);
            LinkedListNode<WordFrequency> current = this.words.Find(wf);

            if (current == null)
                this.words.AddLast(new WordFrequency(WordFrequency));
            else
                current.Value.IncrementFrequency();

            return current == null;
        }
        public void Add(string[] WordFrequency)
        {
            foreach (string word in WordFrequency)
            {
                if (this.words.Contains(new WordFrequency(word)))
                {
                    this.words.Find(new WordFrequency(word)).Value.IncrementFrequency();
                }
                else
                {
                    this.words.AddLast(new WordFrequency(word));
                }
            }
        }
        public bool Contains(WordFrequency wf)
        {
            if (this.words.Contains(wf))
                return true;
            return false;
        }
        public ArrayList CommonWords(Libro book)
        {
            ArrayList sim = new ArrayList();
            foreach (WordFrequency word in book)
            {
                if (this.words.Contains(word))
                    sim.Add(word.WORD);
            }
            return sim;
        }
        public void CombineBooksOnFirstOne(Libro book)
        {
            foreach (WordFrequency word in book)
            {
                if (this.words.Contains(word))
                    this.words.Find(word).Value.FusionFrequency(word);
                else
                {
                    this.words.AddLast(word);
                }
            }
        }
        public bool Substitute(string word1, string word2)
        {
            if (word2 == null)
            {
                return this.words.Remove(new WordFrequency(word1));
            }
            if (this.words.Contains(new WordFrequency(word1)))
            {
                if (this.words.Contains(new WordFrequency(word2)))
                {
                    int word2Freq = this.words.Find(new WordFrequency(word2)).Value.FREQUENCY;
                    this.words.Remove(new WordFrequency(word2));
                    this.words.Find(new WordFrequency(word1)).Value.Substitute(word2, word2Freq);
                }
                else
                    this.words.Find(new WordFrequency(word1)).Value.Substitute(word2);
            }
            else
                return false;

            return true;
        }
        public int CompareTo(Libro otro)
        {
            return this.titulo.CompareTo(otro.titulo);
        }
        public override bool Equals(object obj)
        {
            return (this.CompareTo((Libro)obj) == 0);
        }
        public override int GetHashCode()
        {
            return this.titulo.GetHashCode();
        }
        public override string ToString()
        {
            string palabras = "";
            int iter = 0;
            palabras += "[";
            foreach (WordFrequency i in this.words)
            {
                palabras += i.WORD + "<" + i.FREQUENCY + ">";
                if (iter < this.words.Count - 1)
                    palabras += ", ";
                iter++;
            }
            palabras += "]";
            return titulo + "<" + contador + "> {" + this.precioBase + " euros} " + "= " + palabras;
        }
        public LinkedList<WordFrequency>.Enumerator GetEnumerator()
        {
            return this.words.GetEnumerator();
        }
    }
}