using Graphs;

var _graph = new Graph();
var _directions = new List<Vertex>() {
    new Vertex( 0,  1),
    new Vertex( 0, -1),
    new Vertex( 1,  0),
    new Vertex(-1,  0),
};


for (int i = 0; i < 10; i++) {
    for (int j = 0; j < 10; j++) {

        var gridPos = new Vertex(i, j);

        _graph.AddNode(gridPos);

        foreach (var dir in _directions) {
            if (dir.i + i < 0  ) continue;      
            if (dir.i + i >= 10) continue;      
            if (dir.j + j < 0  ) continue;      
            if (dir.j + j >= 10) continue;

            var gridValidCell = new Vertex(gridPos.i + dir.i, gridPos.j + dir.j);

            _graph.AddEdge(gridPos, gridValidCell);

        }

    }
}

_graph.PrintVertices();
_graph.PrintEdges();

// Let's make a road!
var _possibleDirections = new List<Vertex>() {
    new Vertex(0,  1),
    new Vertex(0, -1),
    new Vertex(1,  0),
    // Don't use left
};


var _initPos = new Vertex(0, 0);
var _road = new List<Vertex>() { _initPos };
var _forbidden = new HashSet<Vertex>() { _initPos };

while (_road.Last().i < 9) { // Mientras no me salga, sigo caminando

    var validPositions = new List<Vertex>();
    var currentPos = _road.Last();

    foreach (var dir in _possibleDirections) { // Evaluar vecinos
        // El Sujeto es la posición en la dirección indicada, desde la posición actual (puede ser la celda de arriba, abajo o derecha)
        var cellCandidate = new Vertex(currentPos.i + dir.i, currentPos.j + dir.j);

        if (!_graph.AdjacencyList[currentPos].Contains(cellCandidate)) continue; // Huir si el grafo no contiene al vecino evaluado
        if (_forbidden.Contains(cellCandidate)) continue; // Huir si la celda está prohibida
        
        validPositions.Add(cellCandidate);
    }
    
    var randomValidPosition = Random.Shared.Next(validPositions.Count);
    var randomCellCandidate = validPositions[randomValidPosition];

    _road.Add(randomCellCandidate);
    _forbidden.Add(randomCellCandidate);

    if (_road.Last().i == 9) Console.WriteLine("Yuju!");
}


Console.WriteLine("Road: ");
Console.ForegroundColor = ConsoleColor.Magenta;
_road.ForEach(cell => Console.Write($"({cell.i}, {cell.j}) -> "));








 