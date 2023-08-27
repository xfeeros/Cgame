namespace BombermanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация игрового поля
            char[,] grid = new char[10, 10]; // Пол размером 10x10
            InitializeGrid(grid);

            // Добавление препятствий (стен)
            AddObstacles(grid);

            // Инициализация игрока
            int playerX = 0; // Начальная позиция игрока по X
            int playerY = 0; // Начальная позиция игрока по Y
            char playerChar = 'P'; // Символ игрока

            // Игровой цикл
            bool isGameOver = false;
            while (!isGameOver)
            {
                // Вывод игрового поля на экран
                PrintGrid(grid, playerX, playerY, playerChar);

                // Считывание ввода от игрока
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                // Обработка ввода
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MovePlayer(grid, ref playerX, ref playerY, playerChar, 0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        MovePlayer(grid, ref playerX, ref playerY, playerChar, 0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        MovePlayer(grid, ref playerX, ref playerY, playerChar, -1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        MovePlayer(grid, ref playerX, ref playerY, playerChar, 1, 0);
                        break;
                    case ConsoleKey.Escape:
                        isGameOver = true;
                        break;
                }
            }
        }

        // Инициализация игрового поля
        static void InitializeGrid(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = '.';
                }
            }
        }

        // Добавление препятствий (стен)
        static void AddObstacles(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Добавляем препятствия случайным образом
            Random random = new Random();
            int obstacleCount = random.Next(5, 11); // Случайное количество препятствий от 5 до 10

            for (int i = 0; i < obstacleCount; i++)
            {
                int obstacleX = random.Next(cols);
                int obstacleY = random.Next(rows);

                if (grid[obstacleY, obstacleX] != '#')
                {
                    grid[obstacleY, obstacleX] = '#';
                }
                else
                {
                    i--; // Если в этой позиции уже есть препятствие, повторяем попытку
                }
            }
        }

        // Вывод игрового поля на экран
        static void PrintGrid(char[,] grid, int playerX, int playerY, char playerChar)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == playerY && j == playerX)
                    {
                        Console.Write(playerChar);
                    }
                    else
                    {
                        Console.Write(grid[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        // Перемещение игрока по игровому полю (с учетом препятствий)
        static void MovePlayer(char[,] grid, ref int playerX, ref int playerY, char playerChar, int deltaX, int deltaY)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int newPlayerX = playerX + deltaX;
            int newPlayerY = playerY + deltaY;
            if (newPlayerX >= 0 && newPlayerX < cols && newPlayerY >= 0 && newPlayerY < rows && grid[newPlayerY, newPlayerX] != '#')
            {
                // Если новая позиция не является стеной, перемещаем игрока
                grid[playerY, playerX] = '.'; // Освобождаем текущую позицию игрока
                playerX = newPlayerX;
                playerY = newPlayerY;
                grid[playerY, playerX] = playerChar; // Обновляем позицию игрока на игровом поле
            }
        }
    }
}