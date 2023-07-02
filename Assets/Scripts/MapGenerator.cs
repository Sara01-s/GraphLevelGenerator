using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

internal sealed class MapGenerator : MonoBehaviour {
    [Header("Generation Settings")]
    [SerializeField] private Vector2Int _mapSize = new(10, 10);
    [SerializeField] private float _cellGenerationDuration = 0.2f;
    [SerializeField] private GameObject _lastPiece;
    [SerializeField] private GameObject[] _pieces;
    [Space(20), Header("Extra")]
    [SerializeField] private AudioClip _spawnSound;
    [SerializeField] private AudioClip _doneSound;

    private Graph _graph;

    private void Awake() {
        _graph = new Graph();

        GenerateGraph();
        StartCoroutine(GenerateRoad());

        _graph.PrintVertices();
        _graph.PrintEdges();
    }
    
    private void GenerateGraph() {
        var directions = new List<Vector2>() { // Haremos conexiones a estas cuatro direcciones desde una celda
            Vector2.up,
            Vector2.down,
            Vector2.right,
            Vector2.left,
        };

        for (int i = 0; i < _mapSize.x; i++) {
            for (int j = 0; j < _mapSize.y; j++) {

                var gridPos = new Vector2(i, j);                // Pasamos por todos los puntos del gráfo

                _graph.AddNode(gridPos);                        // Añadimos un nodo en cada punto del gráfo

                foreach (var dir in directions) {               // Estoy viendo a mis vecinos en todas direcciones (arriba, abajo, izquierda, derecha)
                    if (dir.x + i < 0  )         continue;      // No me quiero salir del margen de la red
                    if (dir.x + i >= _mapSize.x) continue;      // Ignoraré los vecinos que estén fuera
                    if (dir.y + j < 0  )         continue;      
                    if (dir.y + j >= _mapSize.y) continue;

                    // Pero a los vecinos que sean válidos, les pasaré mi wsp y generaré un contacto (Edge)
                    var gridValidCell = new Vector2(gridPos.x + dir.x, gridPos.y + dir.y);

                    _graph.AddEdge(gridPos, gridValidCell);
                }
            }
        }
    }

    private IEnumerator GenerateRoad() {
        // Let's make a road!
        var possibleDirections = new List<Vector2>() { // Podremos movernos arriba, abajo o a la derecha, nunca a la izquierda
            Vector2.up,
            Vector2.down,
            Vector2.right,
            // No usar izquierda
        };

        var initPos = new Vector2(0, 0);
        var road = new List<Vector2>() { initPos };
        var forbiddenCells = new HashSet<Vector2>() { initPos };

        while (road.Last().x < _mapSize.x - 1) { // Mientras no me salga de la red, sigo avanzando

            var validCells = new List<Vector2>();
            var currentPos = road.Last();

            foreach (var dir in possibleDirections) { // Evaluar cada vecino
                // El candidato es la posición en la dirección indicada, desde la posición actual.
                var cellCandidate = new Vector2(currentPos.x + dir.x, currentPos.y + dir.y);    // Básicamente evaluamos la celda de arriba, abajo o derecha

                if (!_graph.AdjacencyList[currentPos].Contains(cellCandidate)) continue;        // Ignorar celda evaluada si el grafo no la contiene
                if (forbiddenCells.Contains(cellCandidate)) continue;                           // Ignorar celda evaluada si la celda está prohibida
                
                validCells.Add(cellCandidate);                                                  // En este punto solo hay celdas válidas así que las añadimos a una lista
            }
            
            var randomValidPosition = Random.Range(0, validCells.Count); 
            var randomCellCandidate = validCells[randomValidPosition];                          // Elegimos una celda random entre las lista de celdas válidas

            road.Add(randomCellCandidate);                                                      // Añadimos esta celda a nuestro camino
            forbiddenCells.Add(randomCellCandidate);                                            // Prohibimos pasar por esta celda nuevamente

            if (road.Last().x == _mapSize.x - 1)                                                // Si es la fila 9, Llegamos al final yaay :)
                Debug.Log("Termine de generar!");
        }

        // Ahora _road solo contiene piezas que pertenezcan a nuestro camino

        Debug.Log("Empecé a generar!");
        foreach (var cell in road) {
            if (cell.x == _mapSize.x - 1) {
                SpawnLastPieceIn(road.Last());
                Debug.Log("Termine de generar!");
                break;
            }

            SpawnRandomPieceIn(cell);
            yield return new WaitForSeconds(_cellGenerationDuration);
        }
    }

    private void SpawnRandomPieceIn(Vector2 cell) {
        var rand = Random.Range(0, _pieces.Length);
        var piece = Instantiate(_pieces[rand], transform);
        var calculatedPiecePos = cell.x * Vector3.right * piece.transform.localScale.x + cell.y * Vector3.forward * piece.transform.localScale.z;

        piece.transform.position = calculatedPiecePos;

        Debug.Log($"estoy en ({cell.x}, {cell.y}) y ahora");
        PlaySound(piece, _spawnSound, true);
    }

    private void SpawnLastPieceIn(Vector2 cell) {
        var piece = Instantiate(_lastPiece, transform);
        var calculatedPiecePos = cell.x * Vector3.right * piece.transform.localScale.x + cell.y * Vector3.forward * piece.transform.localScale.z;

        piece.transform.position = calculatedPiecePos;

        Debug.Log($"Terminé en ({cell.x}, {cell.y})");
        PlaySound(piece, _doneSound, false);
    }


    private float _pitch;
    private void PlaySound(GameObject from, AudioClip sound, bool incrementPitch) {
        var audio = from.AddComponent<AudioSource>();
        audio.PlayOneShot(sound);

        audio.pitch = (incrementPitch)? audio.pitch = _pitch+=0.1f : audio.pitch;
    }
}
