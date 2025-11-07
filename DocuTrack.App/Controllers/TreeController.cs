
namespace DocuTrack.Controllers
{
    using DocuTrack.Models;
    using DocuTrack.Views;
    public class TreeController
    {
        private readonly ConsoleView view;
        private readonly BinaryTree arbol;

        public TreeController(ConsoleView view)
        {
            this.view = view;
            arbol = new BinaryTree();
        }

        public void RunAllUseCases()
        {
            ConsoleView.PrintHeader("Construcción del árbol y operaciones - DocuTrack S.A.");
            var items = PrepareInitialItems();
            ConsoleView.PrintMessage("Insertando elementos iniciales:\n");
            arbol.InsertMultiple(items, out var results);
            foreach (var r in results) ConsoleView.PrintInsertResult(r.nombre, r.esCarpeta, r.inserted, r.comparisons);
            if (arbol.Raiz != null)
                view.PrintAsciiTree(arbol.Raiz);

            ConsoleView.PrintHeader("Búsquedas rápidas");
            var leftExisting = new[] { "ArchivoA1.txt", "Capas" };
            var rightExisting = new[] { "Zelda.docx", "Fotos" };
            var inexistentes = new[] { "NoExiste1", "ArchivoPerdido.txt" };
            foreach (var q in leftExisting) { var (node, cmp) = arbol.Search(q); ConsoleView.PrintSearchResult(q, node, cmp); }
            foreach (var q in rightExisting) { var (node, cmp) = arbol.Search(q); ConsoleView.PrintSearchResult(q, node, cmp); }
            foreach (var q in inexistentes) { var (node, cmp) = arbol.Search(q); ConsoleView.PrintSearchResult(q, node, cmp); }

            ConsoleView.PrintHeader("Actualizaciones selectivas (Eliminar + Insertar)");
            string hojaAnt = "tmp.log";
            string hojaNuevo = "tmp_renamed.log";
            UpdateByDeleteInsert(hojaAnt, hojaNuevo, esCarpeta: false);
            if (arbol.Raiz != null)
                view.PrintAsciiTree(arbol.Raiz);

            string nodoUnoHijoAnt = "Docs";
            string nodoUnoHijoNuevo = "Docs_New";
            UpdateByDeleteInsert(nodoUnoHijoAnt, nodoUnoHijoNuevo, esCarpeta: true);
            if (arbol.Raiz != null)
                view.PrintAsciiTree(arbol.Raiz);

            string nombreRaizAntiguo = arbol.Raiz?.Nombre ?? "";
            string nombreRaizNuevo = nombreRaizAntiguo + "_ROOT";
            UpdateByDeleteInsert(nombreRaizAntiguo, nombreRaizNuevo, arbol.Raiz?.EsCarpeta ?? true);
            if (arbol.Raiz != null)
                view.PrintAsciiTree(arbol.Raiz);

            ConsoleView.PrintHeader("Eliminaciones selectivas");
            string eliminarHoja = "ArchivoB2.pdf";
            var (deleted1, case1) = arbol.Delete(eliminarHoja);
            ConsoleView.PrintDeleteResult(eliminarHoja, deleted1, case1);
            if (arbol.Raiz != null)
                view.PrintAsciiTree(arbol.Raiz);

            string eliminarUnHijo = nodoUnoHijoNuevo;
            var (deleted2, case2) = arbol.Delete(eliminarUnHijo);
            ConsoleView.PrintDeleteResult(eliminarUnHijo, deleted2, case2);
            if (arbol.Raiz != null)
                view.PrintAsciiTree(arbol.Raiz);

            string eliminarRaiz = arbol.Raiz?.Nombre ?? string.Empty;
            var (deleted3, case3) = arbol.Delete(eliminarRaiz);
            ConsoleView.PrintDeleteResult(eliminarRaiz, deleted3, case3);
            if (arbol.Raiz != null)
                view.PrintAsciiTree(arbol.Raiz);

            ConsoleView.PrintHeader("Recorridos de verificación finales");
            var pre = arbol.Preorden();
            var ino = arbol.Inorden();
            var post = arbol.Postorden();
            var niv = arbol.PorNiveles();
            ConsoleView.PrintTraversals(pre, ino, post, niv);
            ConsoleView.PrintHeight(arbol.Altura());

            ConsoleView.PrintMessage("\nChecklist finalizado. Revisar README para justificaciones y lista de nombres iniciales.");
        }

        private static IEnumerable<(string nombre, bool esCarpeta)> PrepareInitialItems()
        {
            var list = new List<(string, bool)>
            {
                ("MDocs", true),
                ("Capas", true),
                ("ArchivoA1.txt", false),
                ("ArchivoA2.txt", false),
                ("Docs", true),
                ("Fotos", true),
                ("Zelda.docx", false),
                ("ArchivoB2.pdf", false),
                ("tmp.log", false),
                ("Musica", true),
                ("Notas.txt", false),
                ("Videos", true),
                ("Imagen.png", false),
                ("Backup", true)
            };
            return list;
        }

        private void UpdateByDeleteInsert(string antiguo, string nuevo, bool esCarpeta)
        {
            if (string.IsNullOrWhiteSpace(antiguo) || string.IsNullOrWhiteSpace(nuevo)) { ConsoleView.PrintUpdateResult(antiguo, nuevo, false); return; }
            var (foundNew, _) = arbol.Search(nuevo);
            if (foundNew != null) { ConsoleView.PrintUpdateResult(antiguo, nuevo, false); return; }
            var (deleted, _) = arbol.Delete(antiguo);
            if (!deleted) { ConsoleView.PrintUpdateResult(antiguo, nuevo, false); return; }
            var (inserted, _) = arbol.Insert(nuevo, esCarpeta);
            ConsoleView.PrintUpdateResult(antiguo, nuevo, inserted);
        }


    }
}