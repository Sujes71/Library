using System.Collections.Generic;

namespace Libreria
{
    public class UsuarioComp : IComparer<Usuario>
    {
        public int Compare(Usuario a, Usuario b)
        {
            int cmp = a.DEUDA.CompareTo(b.DEUDA);
            return (cmp != 0) ? -cmp : a.NOMBRE.CompareTo(b.NOMBRE);
        }
    }
}