namespace Graphs;

public class Graph {

    private readonly Dictionary<int, List<int>> _adjacecyList;
    private readonly int _graphSize;

    public Graph(int graphSize) {
        _adjacecyList = new Dictionary<int, List<int>>();
        _graphSize = graphSize;
    }

    public void AddNode(int index) {
        _adjacecyList.Add(index, new List<int>());
    }

    // Undirected graph
    public void AddEdge(int from, int to) {
        _adjacecyList[from].Add(to);
        _adjacecyList[to].Add(from);
    }

    public void PrintVertices() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("V = { ");
        for (int node = 0; node < _graphSize; node++) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{node} ");
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("}\n");
    }

    public void PrintEdges() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("E = { ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("// Undirected");


        foreach(var (from, to) in _adjacecyList.Select(x => (x.Key, x.Value))) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($" {from} -> ");
            to.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("}");
    }
}
