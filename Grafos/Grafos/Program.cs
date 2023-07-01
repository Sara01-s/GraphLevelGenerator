using Graphs;

var graph = new Graph(5);
var squares = new int[] { 0, 1, 2, 3, 4 };

foreach (var square in squares) {
    graph.AddNode(square);
}

graph.AddEdge(0, 0);
graph.AddEdge(0, 1);
graph.AddEdge(1, 1);
graph.AddEdge(2, 1);
graph.AddEdge(3, 1);
graph.AddEdge(3, 2);
graph.AddEdge(2, 4);

// *                Quiero generar esto ¿está bien?
// *
// *                          0 1 2 3 4
// *                        0 1 1
// *                        1   1
// *                        2   1 1 1 1
// *                        3   1 1
// *                        4

graph.PrintVertices();
graph.PrintEdges();