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

    public void MostrarMatrizDeAdyacencias() {

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write(" ");
        for (var n = 0; n < _nodos; n++) {
            Console.Write($" {n}"); // Mostrar todos los nodos (morados)
        }

        Console.WriteLine();

        for (var n = 0; n < _nodos; n++) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(n);

            for (var m = 0; m < _nodos; m++) {

                var arista = _adyacencias[n, m];
                // Mostrar las aristas generadas
                Console.ForegroundColor = (arista == 1) ? ConsoleColor.White : ConsoleColor.Red;
                Console.Write($" {arista}");
            }

            Console.WriteLine();
        }
    }

    public void CalcularInDegree() {
        // incremenetar in-degree del nodo mientras más conexiones tenga
        for (var nodo = 0; nodo < _nodos; nodo++) {
            for(var m = 0; m < _nodos; m++) {
                var arista = _adyacencias[nodo, m];

                if (arista == 1)
                    _inDegrees[nodo]++;
            }
        }   
    }

    public void MostrarInDegree() {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("-------IN DEGREES-------");
        for (var nodo = 0; nodo < _nodos; nodo++) {
            Console.WriteLine($"Nodo: {nodo}, in: {_inDegrees[nodo]}");
        }
    }

    public int EncontrarNodoConInDegree0() {
        var encontrado = false;
        var nodo = 0;

        for (; nodo < _nodos; nodo++) {
            if (_inDegrees[nodo] == 0) {
                encontrado = true;
                break;
            }
        }

        return encontrado? nodo : -1;
    }

    public void MarcarComoVisto(int nodo) {
        _inDegrees[nodo] = -1; // Registrar como nodo visto

        for (var n = 0; n < _nodos; n++) {
            if (_adyacencias[nodo, n] == 1)
                _inDegrees[n]--;
        }
    }

}
