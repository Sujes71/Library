using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Libreria
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0 };
            int max = array[0];
            int min = array[0];
            foreach (int i in array)
            {

            }
            Console.WriteLine(max);
            Console.WriteLine(min);
        }
        /*Action<int, List<int>> Add = (num, list) =>
        {
            list.Add(num);
            System.Console.WriteLine(num);
        };
        Func<int, List<int>, List<int>, List<int>> Func = (num, list, salida) =>
         {
             foreach (var i in list)
             {
                 salida.Add(num + i);
             }
             return salida;
         };
        Action<List<int>> MostrarLista = (list) =>
        {
            list.ForEach(n => System.Console.WriteLine(n));
        };
        List<int> lista = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        MostrarLista(Func(5, lista, new List<int>()));*/
    }
    public class CurryValidator
    {
        public static readonly Predicate<Curry>[] Pr ={
        d => d.DORSAL != null && d.DORSAL >=0 && d.DORSAL <100,
        d =>d.NOMBRE != null && d.NOMBRE.Length >= 3 && d.NOMBRE.Length < 20,
        };
    }
    public class Validator
    {
        public static bool Validate<T>(T obj, params Predicate<T>[] validations) =>
        validations.ToList().Where(d =>
        {
            return !d(obj);
        }).Count() == 0;
    }
    public class Curry
    {
        public int? DORSAL { get; set; }
        public string NOMBRE { get; set; }
    }
}
