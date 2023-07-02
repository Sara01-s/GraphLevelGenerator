using System.Collections.Generic;
using UnityEngine;
using System.Linq;

internal sealed class Graph  {

    // Diccionario que guarda todas las coordenadas de los nodos y con que coordenadas está conectado
    internal Dictionary<Vector2, List<Vector2>> AdjacencyList { get; }

    internal Graph() {
        AdjacencyList = new Dictionary<Vector2, List<Vector2>>();
    }

    // Se añade una nueva coordenada (nodo) y se le asigna una lista de conexiones (también coordenadas)
    internal void AddNode(Vector2 v) {
        AdjacencyList.Add(v, new List<Vector2>());
    }

    // Se le añade una nueva conexión al nodo
    internal void AddEdge(Vector2 node, Vector2 connection) {
        AdjacencyList[node].Add(connection);
    }

    internal void PrintVertices() {
        Debug.Log("V = {");

        foreach(var v in AdjacencyList.Keys) {
            Debug.Log($"agregado nodo ({v.x}, {v.y}) ");
        }

        Debug.Log("}");
    }

    internal void PrintEdges() {
        Debug.Log("E = {");
        foreach(var (node, connections) in AdjacencyList.Select(node => (node.Key, node.Value))) {
            Debug.Log($"nodo ({node.x}, {node.y}) está conectado a: ");
            connections.ForEach(connection => Debug.Log($"({connection.x}, {connection.y}) "));
        }
        Debug.Log("}");
    }

}
