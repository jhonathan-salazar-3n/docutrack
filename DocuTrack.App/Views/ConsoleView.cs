using DocuTrack.Models;
using System.Text;

namespace DocuTrack.Views
{
    public class ConsoleView
    {
        public static void PrintHeader(string title)
        {
            Console.WriteLine(new string('=', 70));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 70));
        }

        public static void PrintInsertResult(string nombre, bool esCarpeta, bool inserted, int comparisons)
        {
            if (inserted) Console.WriteLine($"Insertado: {nombre} {(esCarpeta ? "(Carpeta)" : "(Archivo)")} - Comparaciones: {comparisons}"); else Console.WriteLine($"Duplicado rechazado: {nombre} - Comparaciones: {comparisons}");
        }

        public static void PrintSearchResult(string nombre, Nodo? node, int comparisons)
        {
            if (node != null) Console.WriteLine($"Busqueda: '{nombre}' - ENCONTRADO -> {node} - Comparaciones: {comparisons}"); else Console.WriteLine($"Busqueda: '{nombre}' - NO ENCONTRADO - Comparaciones: {comparisons}");
        }

        public static void PrintDeleteResult(string nombre, bool deleted, string caseType)
        {
            if (!deleted) Console.WriteLine($"Eliminar: '{nombre}' - NO ENCONTRADO"); else Console.WriteLine($"Eliminar: '{nombre}' - Eliminado. Caso: {caseType}");
        }

        public static void PrintUpdateResult(string antiguo, string nuevo, bool success)
        {
            if (success) Console.WriteLine($"Actualizar: '{antiguo}' -> '{nuevo}' - OK (Eliminar+Insertar)"); else Console.WriteLine($"Actualizar: '{antiguo}' -> '{nuevo}' - FALLIDO (nuevo existe o antiguo no existe)");
        }

        public static void PrintTraversals(IEnumerable<Nodo> pre, IEnumerable<Nodo> ino, IEnumerable<Nodo> post, IEnumerable<Nodo> niv)
        {
            Console.WriteLine();
            Console.WriteLine("Recorridos:");
            Console.WriteLine("Preorden : " + StringJoinNames(pre));
            Console.WriteLine("Inorden  : " + StringJoinNames(ino));
            Console.WriteLine("Postorden: " + StringJoinNames(post));
            Console.WriteLine("Por niveles: " + StringJoinNames(niv));
        }

        private static string StringJoinNames(IEnumerable<Nodo> list)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var n in list)
            {
                if (!first) sb.Append(", ");
                sb.Append(n.Nombre + (n.EsCarpeta ? "(C)" : "(A)"));
                first = false;
            }
            return sb.ToString();
        }

        public static void PrintHeight(int altura)
        {
            Console.WriteLine($"Altura del árbol: {altura}");
        }

        public void PrintAsciiTree(Nodo raiz)
        {
            Console.WriteLine();
            Console.WriteLine("Estructura del árbol (ASCII):");
            if (raiz == null) { Console.WriteLine("(árbol vacío)"); return; }
            PrintNode(raiz, "", true);
            Console.WriteLine();
        }

        private void PrintNode(Nodo node, string indent, bool last)
        {
            Console.Write(indent);
            if (last) { Console.Write("└──"); indent += "   "; } else { Console.Write("├──"); indent += "│  "; }
            Console.WriteLine(node.Nombre + (node.EsCarpeta ? " (C)" : " (A)"));
            var children = new List<Nodo>();
            if (node.Izquierdo != null) children.Add(node.Izquierdo);
            if (node.Derecho != null) children.Add(node.Derecho);
            for (int i = 0; i < children.Count; i++) PrintNode(children[i], indent, i == children.Count - 1);
        }

        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}