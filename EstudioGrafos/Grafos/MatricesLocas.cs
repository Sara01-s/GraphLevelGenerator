#if false
var _table = new int[10, 10];
var _pos = (2, 2);

_table[_pos.Item1, _pos.Item2] = 8;
_table[1, 2] = 1; // up
_table[3, 2] = 0; // down
_table[2, 3] = 0; // right
_table[2, 1] = 1; // left

(int, int) GetValidPosition((int, int) coords, int[,] table) {
    var validPositions = new List<(int, int)>();

    var up    = (coords.Item1, coords.Item2 + 1);
    var down  = (coords.Item1, coords.Item2 - 1);
    var right = (coords.Item1 + 1, coords.Item2);
    var left  = (coords.Item1 - 1, coords.Item2);

    var sides = new List<(int, int)>() { up, down, right, left };

    foreach (var side in sides) {
        if (table[side.Item1, side.Item2] == 1) {
            Color(ConsoleColor.Red);
            Console.WriteLine($"No se puede ir a {side}");
        }
        else if (table[side.Item1, side.Item2] == 0) {
            Color(ConsoleColor.Green);
            Console.WriteLine($"Puedes ir a {side}");
            validPositions.Add(side);
        }
    }

    Color(ConsoleColor.Magenta);
    return coords;
}

Console.WriteLine($"Evaluando pos: {GetValidPosition(_pos, _table)}");
PrintTable();


static void Color(ConsoleColor color) => Console.ForegroundColor = color;

void PrintTable() {
    Console.Write(" ");

    var rows = _table.Length / 10;

    for (int n = 0; n < rows; n++) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($" {n}");
    }

    Console.WriteLine();

    for (int n = 0; n < rows; n++) {

        Color(ConsoleColor.Yellow);
        Console.Write(n);

        for (int m = 0; m < rows; m++) {
            Color((_table[n, m] == 0) ? ConsoleColor.DarkGray : ConsoleColor.Red);
            if (_table[n, m] == 8) Color(ConsoleColor.White);


            Console.Write($" {_table[n, m]}");

        }

        Console.WriteLine();
    }
}
#endif