using Microsoft.VisualStudio.TestTools.UnitTesting;
using Libreria;
using System;
using System.Collections.Generic;
namespace MSTest
{
    [TestClass]
    public class TestLibrary
    {
        string ruta = @"C:\Users\G531\VSCWORKSPACES\VS-CODE\";

        [TestMethod]
        public void TestWordFrequency()
        {
            WordFrequency wf = new WordFrequency("amor");
            WordFrequency wf2 = new WordFrequency("el", 5);
            /*Test Constructor Y método ToString()*/
            Assert.AreEqual("amor<1>", wf.ToString());
            Assert.AreEqual("el<5>", wf2.ToString());
            /*Test Decrement, Increment y Fusion*/
            wf2.DecrementFrequency();
            wf.IncrementFrequency();
            Assert.IsTrue(wf.FREQUENCY == 2);
            Assert.IsTrue(wf2.FREQUENCY == 4);
            wf.FusionFrequency(wf2);
            Assert.IsTrue(wf.FREQUENCY == 6);
            /*Test Substitute methods*/
            wf.Substitute("papo");
            Assert.IsTrue(wf.WORD == "papo");
            wf.Substitute("testing", 2);
            Assert.AreEqual("testing<8>", wf.ToString());
            /*Test Compare y Equal*/
            WordFrequency wf3 = new WordFrequency("el");
            Assert.AreEqual(wf3, wf2);
            Assert.AreNotEqual(wf3, wf);
        }
        [TestMethod]
        public void TestLibro()
        {
            Libro b = new Libro("Harry potter");
            Libro b2 = new Libro("Hansel y grettel", 2);
            Libro b3 = new Libro("Harry el sucio", 3, 20);
            Libro b4 = new Libro(b2);
            /*Test Constructores y método ToString()*/
            Assert.AreEqual("harry potter<1> {12,09 euros} = []", b.ToString());
            Assert.AreEqual("hansel y grettel<2> {12,09 euros} = []", b2.ToString());
            Assert.AreEqual("harry el sucio<3> {24,2 euros} = []", b3.ToString());
            Assert.AreEqual("hansel y grettel<1> {12,09 euros} = []", b4.ToString());
            /*Test diferenciación entre Precio y PrecioBase junto con Increment y Decrement*/
            Assert.IsTrue(b2.PRECIOBASE == 12.09);
            Assert.IsTrue(b2.PRECIO == 24.18);
            b2.IncrementarContador();
            Assert.IsTrue(b2.PRECIO == 36.27);
            b3.DecrementarContador();
            Assert.IsTrue(b3.PRECIO == 48.4);
            /*Test metodos Add*/
            b.Add("estoy");
            b2.Add(new string[] { "elemento", "papo", "morcilla", "empanada", "elemento" });
            Assert.AreEqual("harry potter<1> {12,09 euros} = [estoy<1>]", b.ToString());
            Assert.AreEqual("hansel y grettel<3> {12,09 euros} = [elemento<2>, papo<1>, morcilla<1>, empanada<1>]", b2.ToString());
            /*Test Load y GetSumOfFrequencies()*/
            Assert.IsTrue(b3.Load(ruta + "/archivo.txt"));
            Assert.IsTrue(b3.Size() == 43);
            Assert.IsTrue(b3.Contains(new WordFrequency("y")));
            Assert.IsFalse(b3.Contains(new WordFrequency("jopeliunes")));
            Assert.IsTrue(b3.GetSumOfFrequency() == 56);
            /*Test de Revalorizar y Substitute*/
            Assert.IsTrue(b.PRECIO == 12.09);
            Libro b5 = new Libro(b);
            b.Revalorizar(0.5);
            Assert.IsTrue(b.PRECIO == 6.04);
            b5.Revalorizar(2);
            Assert.IsTrue(b5.PRECIO == 24.18);
            Assert.IsTrue(b.Contains(new WordFrequency("estoy")));
            b.Substitute("estoy", "noestoy");
            Assert.IsFalse(b.Contains(new WordFrequency("estoy")));
            Assert.IsTrue(b.Contains(new WordFrequency("noestoy")));
            b.Substitute("noestoy", null);
            Assert.IsTrue(b.Size() == 0);
            b.Add("estoy");
            b.Add(new string[] { "cura", "cura" });
            Assert.AreEqual("harry potter<1> {6,04 euros} = [estoy<1>, cura<2>]", b.ToString());
            b.Substitute("estoy", "cura");
            Assert.AreEqual("harry potter<1> {6,04 euros} = [cura<3>]", b.ToString());
            b.Substitute("cura", "estoy");
            Assert.AreEqual("harry potter<1> {6,04 euros} = [estoy<3>]", b.ToString());
            /*Test CommonWords y CombineBooksOnFirstOne*/
            b.Add("pepe");
            b4.Add(new string[] { "estoy", "julian", "umtiti", "lebron" });
            Assert.IsTrue(b.Size() == 2);
            Assert.IsTrue(b.CommonWords(b4).Count == 1);
            b.Add(new string[] { "julian", "umtiti", "pepito", "juliacon", "mamamia" });
            Assert.IsTrue(b.CommonWords(b4).Count == 3);
            Assert.IsTrue(b.Size() == 7);
            Assert.IsTrue(b4.Size() == 8);
            b.CombineBooksOnFirstOne(b4);
            Assert.AreEqual(b.Size(), 12);
            Assert.IsTrue(b.GetSumOfFrequency() == 18);
        }
        [TestMethod]
        public void TestUsuario()
        {
            Usuario user = new Usuario("Pepe");
            Usuario user2 = new Usuario("Jesus", "663219938");
            Usuario user3 = null;
            /*Test Contructor y método ToString()*/
            try
            {
                user3 = new Usuario("Juan", "632");
            }
            catch (Exception e)
            {
                Assert.AreEqual("El telefono debe tener 9 números", e.Message);
            }
            Usuario user4 = new Usuario("gabriel");
            Assert.AreEqual("Nombre: {Gabriel} Telefono: {000-000-000} Deuda: {0 euros} = []", user4.ToString());
            Assert.AreEqual("Nombre: {Jesus} Telefono: {663-219-938} Deuda: {0 euros} = []", user2.ToString());
            Usuario user5 = new Usuario("Jesus");
            Assert.IsFalse(user2.Equals(user5));
            user5.TELEFONO = "663219938";
            Assert.IsTrue(user2.Equals(user5));
            try
            {
                user5.TELEFONO = "752";
            }
            catch (Exception e)
            {
                Assert.AreEqual("El telefono debe tener 9 números", e.Message);
            }
            /*Test Add, Remove y GetLibro*/
            user.Add("Pepe el grillo");
            user.Add("Pepe el grillo");
            Assert.AreEqual("Nombre: {Pepe} Telefono: {000-000-000} Deuda: {24,18 euros} = [pepe el grillo<2> {12,09 euros}]", user.ToString());
            user.Add(new Libro("Harry potter", 3, 32));
            Assert.AreEqual("Nombre: {Pepe} Telefono: {000-000-000} Deuda: {140,34 euros} = [pepe el grillo<2> {12,09 euros}, harry potter<3> {38,72 euros}]", user.ToString());
            Assert.IsTrue(user.GetLibro("Harry potter").PRECIO == 116.16);
            user.Remove(new Libro("Harry potter"));
            Assert.IsTrue(user.Size() == 1);
            Assert.IsTrue(user.Contains(new Libro("Pepe el grillo")));
            Assert.IsFalse(user.Contains(new Libro("Harry potter")));
            /*Test Load*/
            Assert.IsTrue(user2.Load(ruta + "/archivo2.txt"));
            Assert.IsTrue(user2.Size() == 6);
            Assert.IsTrue(user2.GetSumOfContadores() == 81);
            Assert.IsTrue(user2.Contains(new Libro("lukaku es el mejor")));
            /*Test UsuarioComp*/
            SortedSet<Usuario> sort = new SortedSet<Usuario>(new UsuarioComp());
            Usuario usuario = new Usuario("pepe");
            Usuario usuario2 = new Usuario("lukaku");
            Usuario usuario3 = new Usuario("anabel");
            Usuario usuario4 = new Usuario("beckam");
            sort.Add(usuario);
            sort.Add(usuario2);
            sort.Add(usuario3);
            sort.Add(usuario4);
            string salida = "Usuarios: ";
            int iter = 0;
            foreach (Usuario us in sort)
            {
                salida += us.NOMBRE;
                if (iter < sort.Count - 1)
                    salida += ", ";
                iter++;
            }
            Assert.AreEqual("Usuarios: Anabel, Beckam, Lukaku, Pepe", salida);
            usuario.DEUDA = 40.2;
            usuario2.DEUDA = 100;
            usuario3.DEUDA = 34;
            usuario4.DEUDA = 12.1;
            sort.Clear();
            sort.Add(usuario);
            sort.Add(usuario2);
            sort.Add(usuario3);
            sort.Add(usuario4);
            iter = 0;
            salida = "Usuarios: ";
            foreach (Usuario us in sort)
            {
                salida += us.NOMBRE;
                if (iter < sort.Count - 1)
                    salida += ", ";
                iter++;
            }
            Assert.AreEqual("Usuarios: Lukaku, Pepe, Anabel, Beckam", salida);
        }
        [TestMethod]
        public void TestBiblioteca()
        {
            Biblioteca b = new Biblioteca("La casa del pescado");
            /*Test AddUsuario, AddLibro, Constructor y ToString()*/
            b.AddLibro(new Libro("Juan el coreano", 3, 12.3));
            b.AddLibro(new Libro("Pepe el grillo"));
            b.AddLibro(new Libro("Pepe el grillo", 4));
            b.AddLibro(new Libro("El pescado cinico", 2));
            b.AddUsuario(new Usuario("Pedro"));
            b.AddUsuario(new Usuario("Jesus", "663219938"));
            b.AddUsuario(new Usuario("Cris"));
            b.AddUsuario(new Usuario("Cris"));
            Assert.IsTrue(b.SizeUsuarios() == 3);
            Assert.IsTrue(b.SizeLibros() == 3);
            Assert.AreEqual("LA CASA DEL PESCADO->USUARIOS: [Cris {000-000-000} {0 euros}, Jesus {663-219-938} {0 euros}, Pedro {000-000-000} {0 euros}]"
            + " LIBROS: [el pescado cinico {2} {12,09 euros}, juan el coreano {3} {14,88 euros}, pepe el grillo {5} {12,09 euros}]", b.ToString());
            /*Test PrestarLibro, DevolverLibro, GetUsuario y GetLibro*/
            b.PrestarLibro(new Usuario("Pedro"), new Libro("juan el coreano"));
            b.PrestarLibro(new Usuario("Pedro"), new Libro("el pescado cinico"));
            Assert.AreEqual("Nombre: {Pedro} Telefono: {000-000-000} Deuda: {26,97 euros} = [juan el coreano<1> {14,88 euros}, el pescado cinico<1> {12,09 euros}]", b.GetUsuario(new Usuario("Pedro")).ToString());
            Assert.IsTrue(b.SizeLibros() == 3);
            Assert.AreEqual("el pescado cinico<1> {12,09 euros} = []", b.GetLibro(new Libro("el pescado cinico")).ToString());
            b.PrestarLibro(new Usuario("Pedro"), new Libro("el pescado cinico"));
            Assert.IsTrue(b.SizeLibros() == 2);
            Assert.AreEqual("Nombre: {Pedro} Telefono: {000-000-000} Deuda: {39,06 euros} = [juan el coreano<1> {14,88 euros}, el pescado cinico<2> {12,09 euros}]", b.GetUsuario(new Usuario("Pedro")).ToString());
            b.PrestarLibro(new Usuario("Jesus", "663219938"), new Libro("Pepe el grillo"));
            Assert.AreEqual("LA CASA DEL PESCADO->USUARIOS: [Cris {000-000-000} {0 euros}, Jesus {663-219-938} {12,09 euros}, Pedro {000-000-000} {39,06 euros}]"
            + " LIBROS: [juan el coreano {2} {14,88 euros}, pepe el grillo {4} {12,09 euros}]", b.ToString());
            b.DevolverLibro(new Usuario("Jesus", "663219938"), new Libro("Pepe el grillo"));
            b.DevolverLibro(new Usuario("Pedro"), new Libro("el pescado cinico"));
            Assert.AreEqual("LA CASA DEL PESCADO->USUARIOS: [Cris {000-000-000} {0 euros}, Jesus {663-219-938} {0 euros}, Pedro {000-000-000} {26,97 euros}]"
            + " LIBROS: [el pescado cinico {1} {12,09 euros}, juan el coreano {2} {14,88 euros}, pepe el grillo {5} {12,09 euros}]", b.ToString());
            /*Test Load*/
            Biblioteca b2 = new Biblioteca("Real madrid FC");
            b2.Load(ruta + "/archivo3.txt");
            Assert.AreEqual("REAL MADRID FC->USUARIOS: [Irene {000-000-000} {0 euros}, Jesús {000-000-000} {0 euros}, Juan {663-221-212} {0 euros},"
            + " Luis {712-119-938} {0 euros}, Monica {000-000-000} {0 euros}, Pepe {663-219-938} {0 euros}]"
            + " LIBROS: [gru mi villano favorito {2} {12,09 euros}, harry el sucio {5} {14,52 euros}, harry potter y la piedra filosa {8} {51 euros}, kylian mbappe {3} {39,93 euros}, que estas haciendo tonto {1} {12,09 euros}, umtiti el polluo {4} {27,83 euros}]", b2.ToString());
            /*Test RentedBooks, WhoHaveRented y WhatBooksHaveRented*/
            Assert.IsTrue(b.RentedBooks().Count == 2);
            Assert.IsTrue(b2.RentedBooks().Count == 0);
            Assert.IsTrue(b.WhoHaveRented(new Libro("Pepe el grillo")).Count == 0);
            b.PrestarLibro(new Usuario("Pedro"), new Libro("Pepe el grillo"));
            Assert.IsTrue(b.WhoHaveRented(new Libro("Pepe el grillo")).Count == 1);
            b.PrestarLibro(new Usuario("Jesus", "663219938"), new Libro("Pepe el grillo"));
            Assert.IsTrue(b.WhoHaveRented(new Libro("Pepe el grillo")).Count == 2);
            Assert.IsTrue(b.WhatBooksHaveRented(new Usuario("Pedro")).Count == 3);
            b.DevolverLibro(new Usuario("Pedro"), new Libro("Pepe el grillo"));
            Assert.IsTrue(b.WhatBooksHaveRented(new Usuario("Pedro")).Count == 2);
            /*Test Iteración Biblioteca GetEnumerator y ConsultoryToString()*/
            string salida = b.LIBRARYNAME.ToUpper() + "->";
            int iter = 0;
            foreach (KeyValuePair<Usuario, List<Libro>> par in b)
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
            Assert.AreEqual(salida, b.ConsultoryToString());
        }
    }
}