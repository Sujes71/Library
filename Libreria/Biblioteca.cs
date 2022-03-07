using System;
using System.IO;
using System.Collections.Generic;

namespace Libreria
{
    public class Biblioteca
    {
        private string libraryName;
        public string LIBRARYNAME { get { return this.libraryName; } }
        private SortedSet<Usuario> users;
        private SortedSet<Libro> books;

        public Biblioteca(string libraryName)
        {
            this.libraryName = libraryName;
            users = new SortedSet<Usuario>(new UsuarioComp());
            books = new SortedSet<Libro>();
        }
        public bool AddLibro(Libro book)
        {
            if (this.books.Contains(book))
            {
                foreach (Libro bo in this.books)
                {
                    if (!bo.Equals(book)) continue;
                    if (book.CONTADOR == 1)
                        bo.IncrementarContador();
                    else
                        bo.IncrementarContador(book.CONTADOR);
                    break;
                }
            }
            return this.books.Add(book);
        }
        public bool AddUsuario(Usuario user)
        {
            return this.users.Add(user);
        }
        public int SizeUsuarios()
        {
            return this.users.Count;
        }
        public int SizeLibros()
        {
            return this.books.Count;
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
                if (line[1].Equals("-"))
                    this.AddUsuario(new Usuario(line[0]));
                else
                    this.AddUsuario(new Usuario(line[0], line[1]));
                if (line[4].Equals("-"))
                    this.AddLibro(new Libro(line[2], int.Parse(line[3])));
                else
                    this.AddLibro(new Libro(line[2], int.Parse(line[3]), double.Parse(line[4])));
            }
            reader.Close();
            return true;
        }
        public Usuario GetUsuario(Usuario user)
        {
            foreach (Usuario us in this.users)
            {
                if (!user.Equals(us)) continue;
                return us;
            }
            return null;
        }
        public Libro GetLibro(Libro book)
        {
            foreach (Libro bo in this.books)
            {
                if (!book.Equals(bo)) continue;
                return book;
            }
            return null;
        }
        public bool PrestarLibro(Usuario user, Libro book)
        {
            Libro baux = null;
            if (this.books.Contains(book))
            {
                foreach (Libro bo in this.books)
                {
                    if (!bo.Equals(book)) continue;
                    if (bo.CONTADOR == 1)
                    {
                        baux = bo;
                        this.books.Remove(bo);
                        break;
                    }
                    else
                    {
                        bo.DecrementarContador();
                        baux = bo;
                        break;
                    }
                }
                return this.GetUsuario(user).Add(new Libro(baux));
            }
            return false;
        }
        public bool DevolverLibro(Usuario user, Libro book)
        {
            foreach (Libro bo in this.GetUsuario(user))
            {
                if (!bo.Equals(book)) continue;

                foreach (Usuario us in this.users)
                {
                    if (!us.Equals(user)) continue;
                    if (bo.CONTADOR == 1)
                    {
                        us.Remove(bo);
                    }
                    else
                    {
                        us.DEUDA -= us.GetLibro(bo.TITULO).PRECIOBASE;
                        us.DEUDA = Math.Round(us.DEUDA, 2);
                        bo.DecrementarContador();
                    }
                    foreach (Libro libro in this.books)
                    {
                        if (!libro.Equals(bo)) continue;
                        libro.IncrementarContador();
                        break;
                    }
                    break;
                }
                return this.books.Add(book);
            }
            return false;
        }
        public LinkedList<String> RentedBooks()
        {
            LinkedList<String> list = new LinkedList<String>();
            foreach (var par in this)
            {
                if (par.Key.Size() == 0) continue;
                foreach (Libro book in par.Key)
                {
                    list.AddLast(book.TITULO + "<" + book.CONTADOR + "> = " + par.Key.NOMBRE);
                }

            }
            return list;
        }
        public LinkedList<String> WhoHaveRented(Libro book)
        {
            LinkedList<String> list = new LinkedList<String>();
            foreach (Usuario us in this.users)
            {
                foreach (Libro bo in us)
                {
                    if (!bo.Equals(book)) continue;
                    list.AddLast(us.NOMBRE + " {" + us.TELEFONO + "}");
                    break;
                }
            }
            return list;
        }
        public LinkedList<String> WhatBooksHaveRented(Usuario user)
        {
            LinkedList<String> list = new LinkedList<String>();
            foreach (Usuario us in this.users)
            {
                if (!us.Equals(user)) continue;
                foreach (Libro bo in us)
                {
                    list.AddLast(bo.TITULO + "<" + bo.CONTADOR + ">");
                }
            }
            return list;
        }
        public override bool Equals(object obj)
        {
            return (this.CompareTo((Biblioteca)obj) == 0);
        }
        public override int GetHashCode()
        {
            return this.libraryName.GetHashCode();
        }
        public int CompareTo(Biblioteca otra)
        {
            return this.libraryName.CompareTo(otra.libraryName);
        }
        public string ConsultoryToString()
        {
            string salida = this.libraryName.ToUpper() + "->";
            int iter = 0;
            foreach (var par in this)
            {
                salida += par.Key.NOMBRE + " {" + par.Key.TELEFONO + "} {" + par.Key.DEUDA + " euros} = [";
                foreach (Libro book in par.Key)
                {
                    salida += book.TITULO + "<" + book.CONTADOR + ">" + " { " + book.PRECIOBASE + " euros}";
                    if (iter < par.Key.Size() - 1)
                        salida += ", ";
                    iter++;
                }
                iter = 0;
                salida += "]\n";
            }
            return salida;
        }
        public override string ToString()
        {
            string salida = this.libraryName.ToUpper() + "->USUARIOS: [";
            int iter = 0;
            foreach (Usuario user in this.users)
            {
                salida += user.NOMBRE + " {" + user.TELEFONO + "} " + "{" + user.DEUDA + " euros}";
                if (iter < this.users.Count - 1)
                    salida += ", ";
                iter++;
            }
            iter = 0;
            salida += "] LIBROS: [";
            foreach (Libro libro in this.books)
            {
                salida += libro.TITULO + " {" + libro.CONTADOR + "} " + "{" + libro.PRECIOBASE + " euros}";
                if (iter < this.books.Count - 1)
                    salida += ", ";
                iter++;
            }
            salida += "]";
            return salida;
        }
        public Dictionary<Usuario, List<Libro>>.Enumerator GetEnumerator()
        {
            Dictionary<Usuario, List<Libro>> dict = new Dictionary<Usuario, List<Libro>>();
            List<Libro> listaLibros;
            foreach (Usuario user in this.users)
            {
                listaLibros = new List<Libro>();
                foreach (Libro book in this.books)
                {
                    if (user.Contains(book))
                        listaLibros.Add(book);
                }
                dict.Add(user, listaLibros);
            }
            return dict.GetEnumerator();
        }
    }
}