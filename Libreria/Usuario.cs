using System;
using System.IO;
using System.Collections.Generic;

namespace Libreria
{
    public class Usuario : IComparable<Usuario>
    {
        private string nombre;
        public string NOMBRE { get { return this.nombre; } }
        private string telefono;
        public string TELEFONO { get { return this.telefono; } set { this.telefono = validarTelefono(value); } }
        public string validarTelefono(string telefono)
        {
            if (TelefonoEsValido(telefono.Trim()))
            {
                telefono = telefono.Insert(3, "-");
                telefono = telefono.Insert(7, "-");
            }
            else
            {
                throw new Exception("El telefono debe tener 9 números");
            }
            return telefono;
        }
        private double deuda;
        public double DEUDA { get { return this.deuda; } set { this.deuda = ValidarDeuda(value); } }
        public double ValidarDeuda(double deuda)
        {
            if (deuda <= 0)
                return 0;
            return Math.Round(deuda, 2);
        }
        private LinkedList<Libro> libros;
        public Usuario(string nombre)
        {
            this.nombre = nombre.Substring(0, 1).ToUpper().Trim();
            this.nombre += nombre.Substring(1).ToLower().Trim();
            this.telefono = "000-000-000";
            this.deuda = 0;
            libros = new LinkedList<Libro>();
        }
        public Usuario(string nombre, string telefono)
        {
            this.nombre = nombre.Substring(0, 1).ToUpper().Trim();
            this.nombre += nombre.Substring(1).ToLower().Trim();
            if (TelefonoEsValido(telefono.Trim()))
            {
                this.telefono = telefono.Insert(3, "-");
                this.telefono = this.telefono.Insert(7, "-");
            }
            else
            {
                throw new Exception("El telefono debe tener 9 números");
            }
            this.deuda = 0;
            libros = new LinkedList<Libro>();
        }
        public int Size()
        {
            return this.libros.Count;
        }
        public bool TelefonoEsValido(string telefono)
        {
            if (telefono.Length == 9)
                return true;
            else
                return false;
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
                scan = reader.ReadLine().Trim();
                if (scan.StartsWith("@")) continue;
                line = scan.Split(":");

                if (line.Length == 2)
                    this.Add(new Libro(line[0], Convert.ToInt32(line[1])));
                else if (line.Length == 3)
                    this.Add(new Libro(line[0], Convert.ToInt32(line[1]), double.Parse(line[2])));
                else
                    this.Add(new Libro(line[0]));
            }
            reader.Close();
            return true;
        }
        public bool Add(string titulo)
        {
            Libro book = new Libro(titulo);
            LinkedListNode<Libro> current = this.libros.Find(book);
            if (current == null)
            {
                this.libros.AddLast(book);
                this.deuda += this.GetLibro(titulo).PRECIOBASE;
                this.deuda = Math.Round(this.deuda, 2);
            }
            else
            {
                current.Value.IncrementarContador();
                this.deuda += this.GetLibro(titulo).PRECIOBASE;
                this.deuda = Math.Round(this.deuda, 2);
            }
            return current == null;
        }
        public bool Add(Libro book)
        {
            LinkedListNode<Libro> current = this.libros.Find(book);
            if (current == null)
            {
                this.libros.AddLast(book);
                this.deuda += this.GetLibro(book.TITULO).PRECIOBASE * book.CONTADOR;
                this.deuda = Math.Round(this.deuda, 2);
            }
            else
            {
                current.Value.IncrementarContador();
                this.deuda += this.GetLibro(book.TITULO).PRECIOBASE * book.CONTADOR;
                this.deuda = Math.Round(this.deuda, 2);
            }
            return current == null;
        }
        public int GetSumOfContadores()
        {
            int sum = 0;
            foreach (var book in this.libros)
            {
                sum += book.CONTADOR;
            }
            return sum;
        }
        public Libro GetLibro(string titulo)
        {
            if (this.libros.Contains(new Libro(titulo)))
                return this.libros.Find(new Libro(titulo)).Value;
            return null;
        }
        public bool Remove(Libro book)
        {
            LinkedListNode<Libro> current = this.libros.Find(book);

            this.libros.Remove(current);
            this.deuda -= current.Value.PRECIOBASE * book.CONTADOR;
            this.deuda = Math.Round(this.deuda, 2);

            return current == null;
        }
        public bool Contains(Libro libro)
        {
            if (this.libros.Contains(libro))
                return true;
            return false;
        }
        public int CompareTo(Usuario otro)
        {
            if (this.nombre.CompareTo(otro.nombre) > 0 || this.telefono.CompareTo(otro.telefono) > 0)
                return 1;
            else if (this.nombre.CompareTo(otro.nombre) < 0 || this.telefono.CompareTo(otro.telefono) < 0)
                return -1;
            else
                return 0;
        }
        public override bool Equals(object obj)
        {
            return this.CompareTo((Usuario)obj) == 0;
        }
        public override int GetHashCode()
        {
            return this.nombre.GetHashCode();
        }
        public override string ToString()
        {
            string libros = "";
            int iter = 0;
            libros += "[";
            foreach (Libro i in this.libros)
            {
                libros += i.TITULO + "<" + i.CONTADOR + "> " + "{" + i.PRECIOBASE + " euros}";
                if (iter < this.libros.Count - 1)
                    libros += ", ";
                iter++;
            }
            libros += "]";
            return "Nombre: {" + this.nombre + "} Telefono: {" + this.telefono + "} Deuda: {" + this.deuda + " euros} = " + libros;
        }
        public LinkedList<Libro>.Enumerator GetEnumerator()
        {
            return this.libros.GetEnumerator();
        }
    }
}