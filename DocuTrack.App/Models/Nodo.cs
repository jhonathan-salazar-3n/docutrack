namespace DocuTrack.Models
{
    public class Nodo
    {
        public string Nombre { get; set; }
        public bool EsCarpeta { get; set; }
        public Nodo? Izquierdo { get; set; }
        public Nodo? Derecho { get; set; }

        public Nodo(string nombre, bool esCarpeta)
        {
            Nombre = nombre;
            EsCarpeta = esCarpeta;
        }

        public override string ToString()
        {
            return Nombre + (EsCarpeta ? " (C)" : " (A)");
        }
    }

}