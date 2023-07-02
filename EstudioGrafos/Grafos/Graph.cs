namespace Graphs;

public struct Vertex {
    public int i;
    public int j;
    public Vertex(int i, int j) { this.i = i; this.j = j; }
}

public class Graph {

    public Dictionary<Vertex, List<Vertex>> AdjacencyList { get; }

    public Graph() {
        AdjacencyList = new Dictionary<Vertex, List<Vertex>>();
    }

    public void AddNode(Vertex v) {
        AdjacencyList.Add(v, new List<Vertex>());
    }

    // Directed graph
    public void AddEdge(Vertex from, Vertex to) {
        AdjacencyList[from].Add(to);
    }

    public void PrintVertices() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("V = {\n");

        foreach(var v in AdjacencyList.Keys) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($" ({v.i}, {v.j}) ");
            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("}\n");
    }

    public void PrintEdges() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("E = { ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("// Directed");


        foreach(var (v, vNeighbours) in AdjacencyList.Select(vertex => (vertex.Key, vertex.Value))) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($" ({v.i}, {v.j}) -> ");
            vNeighbours.ForEach(neighbour => Console.Write($"({neighbour.i}, {neighbour.j}) "));
            Console.WriteLine();
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("}");
    }
}
