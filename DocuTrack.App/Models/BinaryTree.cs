namespace DocuTrack.Models
{
    public class BinaryTree
    {
        public Nodo? Raiz { get; private set; }
        private readonly StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        public (bool inserted, int comparisons) Insert(string nombre, bool esCarpeta)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre vacio", nameof(nombre));
            int comparisons = 0;
            if (Raiz == null)
            {
                Raiz = new Nodo(nombre, esCarpeta);
                return (true, 1);
            }
            Nodo? actual = Raiz;
            Nodo? padre = null;
            while (actual != null)
            {
                padre = actual;
                comparisons++;
                int cmp = comparer.Compare(nombre, actual.Nombre);
                if (cmp == 0) return (false, comparisons);
                actual = cmp < 0 ? actual.Izquierdo : actual.Derecho;
            }
            comparisons++;
            if (padre == null) throw new InvalidOperationException("Padre no puede ser null");
            int cmpPadre = comparer.Compare(nombre, padre.Nombre);
            if (cmpPadre < 0) padre.Izquierdo = new Nodo(nombre, esCarpeta); else padre.Derecho = new Nodo(nombre, esCarpeta);
            return (true, comparisons);
        }

        public (Nodo? node, int comparisons) Search(string nombre)
        {
            int comparisons = 0;
            Nodo? actual = Raiz;
            while (actual != null)
            {
                comparisons++;
                int cmp = comparer.Compare(nombre, actual.Nombre);
                if (cmp == 0) return (actual, comparisons);
                actual = cmp < 0 ? actual.Izquierdo : actual.Derecho;
            }
            return (null, comparisons);
        }

        public (bool deleted, string caseType) Delete(string nombre)
        {
            Nodo? padre = null;
            Nodo? actual = Raiz;
            while (actual != null && comparer.Compare(nombre, actual.Nombre) != 0)
            {
                padre = actual;
                int cmp = comparer.Compare(nombre, actual.Nombre);
                actual = cmp < 0 ? actual.Izquierdo : actual.Derecho;
            }
            if (actual == null) return (false, "NoEncontrado");
            if (actual.Izquierdo == null && actual.Derecho == null)
            {
                if (padre == null) Raiz = null; else if (padre.Izquierdo == actual) padre.Izquierdo = null; else padre.Derecho = null;
                return (true, "Hoja");
            }
            if (actual.Izquierdo == null || actual.Derecho == null)
            {
                Nodo? hijo = actual.Izquierdo ?? actual.Derecho;
                if (padre == null) Raiz = hijo; else if (padre.Izquierdo == actual) padre.Izquierdo = hijo; else padre.Derecho = hijo;
                return (true, "UnHijo");
            }
            Nodo sucesorPadre = actual;
            Nodo? sucesor = actual.Derecho;
            while (sucesor?.Izquierdo != null)
            {
                sucesorPadre = sucesor;
                sucesor = sucesor.Izquierdo;
            }
            if (sucesor != null)
            {
                actual.Nombre = sucesor.Nombre;
                actual.EsCarpeta = sucesor.EsCarpeta;
                if (sucesorPadre.Izquierdo == sucesor) sucesorPadre.Izquierdo = sucesor.Derecho; else sucesorPadre.Derecho = sucesor.Derecho;
            }
            return (true, "DosHijos_Sucesor");
        }

        public bool UpdateByDeleteInsert(string antiguo, string nuevo, bool esCarpeta)
        {
            var (foundNew, _) = Search(nuevo);
            if (foundNew != null) return false;
            var (deleted, _) = Delete(antiguo);
            if (!deleted) return false;
            var (inserted, _) = Insert(nuevo, esCarpeta);
            return inserted;
        }

        public List<Nodo> Preorden()
        {
            var res = new List<Nodo>();
            if (Raiz != null)
                Preorden(Raiz, res);
            return res;
        }
        private void Preorden(Nodo n, List<Nodo> res)
        {
            if (n == null) return;
            res.Add(n);
            if (n.Izquierdo != null)
                Preorden(n.Izquierdo, res);
            if (n.Derecho != null)
                Preorden(n.Derecho, res);
        }

        public List<Nodo> Inorden()
        {
            var res = new List<Nodo>();
            if (Raiz != null)
                Inorden(Raiz, res);
            return res;
        }
        private void Inorden(Nodo n, List<Nodo> res)
        {
            if (n == null) return;
            if (n.Izquierdo != null)
                Inorden(n.Izquierdo, res);
            res.Add(n);
            if (n.Derecho != null)
                Inorden(n.Derecho, res);
        }

        public List<Nodo> Postorden()
        {
            var res = new List<Nodo>();
            if (Raiz != null)
                Postorden(Raiz, res);
            return res;
        }
        private void Postorden(Nodo n, List<Nodo> res)
        {
            if (n == null) return;
            if (n.Izquierdo != null)
                Postorden(n.Izquierdo, res);
            if (n.Derecho != null)
                Postorden(n.Derecho, res);
            res.Add(n);
        }

        public List<Nodo> PorNiveles()
        {
            var res = new List<Nodo>();
            if (Raiz == null) return res;
            var q = new Queue<Nodo>();
            q.Enqueue(Raiz);
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                res.Add(cur);
                if (cur.Izquierdo != null) q.Enqueue(cur.Izquierdo);
                if (cur.Derecho != null) q.Enqueue(cur.Derecho);
            }
            return res;
        }

        public int Altura()
        {
            if (Raiz == null) return 0;
            return Altura(Raiz);
        }
        private int Altura(Nodo n)
        {
            if (n == null) return 0;
            int leftHeight = n.Izquierdo != null ? Altura(n.Izquierdo) : 0;
            int rightHeight = n.Derecho != null ? Altura(n.Derecho) : 0;
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        public void InsertMultiple(IEnumerable<(string nombre, bool esCarpeta)> items, out List<(string nombre, bool esCarpeta, bool inserted, int comparisons)> results)
        {
            results = new List<(string, bool, bool, int)>();
            foreach (var it in items)
            {
                var (ins, cmp) = Insert(it.nombre, it.esCarpeta);
                results.Add((it.nombre, it.esCarpeta, ins, cmp));
            }
        }

    }
}