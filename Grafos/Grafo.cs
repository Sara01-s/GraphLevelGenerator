namespace Grafos;
public class Grafo {

    private int[,] _adyacencias;
    private int[] _inDegrees;
    private int _nodos;

    public Grafo(int nodos) {
        _nodos = nodos;

        // Se crea la matriz de adyacencia
        _adyacencias = new int[nodos, nodos];

        // Se crea la lista de in degrees
        _inDegrees = new int[nodos];
    }

    public void AgregarArista(int nodoInicial, int nodoFinal) {
        // Ponemos un 1 en la coordenada indicada por los parámetros
        // esto establece que nodoInicial y nodoFinal están conectados
        _adyacencias[nodoInicial, nodoFinal] = 1;
    }

    public void MostrarAdyacencias() {
        var n = 0;
        var m = 0;

        for (n = 0; n < _nodos; n++) {
            Console.Write($"\t{n}"); // Mostrar nodos separados por tabulaciones
        }

        Console.WriteLine();

        for (n = 0; n < _nodos; n++) {
            Console.Write(n);

            for (m = 0; m < _nodos; m++) {
                Console.Write($"\t{_adyacencias[n, m]}");
            }

            Console.WriteLine();
        }
        
    }
}
