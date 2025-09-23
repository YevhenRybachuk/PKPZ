using System;

class Program
{
    static void Main()
    {
        int rows = 6, cols = 5;
        int[,] matrix = new int[rows, cols];
        Random rnd = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rnd.Next(-50, 50); 
            }
        }

        Console.WriteLine("Original matrix:");
        PrintMatrix(matrix);

        int maxAbs = Math.Abs(matrix[0, 0]);
        int minAbs = Math.Abs(matrix[0, 0]);
        int maxCol = 0, minCol = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int val = Math.Abs(matrix[i, j]);
                if (val > maxAbs)
                {
                    maxAbs = val;
                    maxCol = j;
                }
                if (val < minAbs)
                {
                    minAbs = val;
                    minCol = j;
                }
            }
        }

        for (int i = 0; i < rows; i++)
        {
            int temp = matrix[i, maxCol];
            matrix[i, maxCol] = matrix[i, minCol];
            matrix[i, minCol] = temp;
        }

        Console.WriteLine("\nModified matrix:");
        PrintMatrix(matrix);

        Console.WriteLine($"\nColumn with max |value| element: {maxCol + 1}");
        Console.WriteLine($"Column with min |value| element: {minCol + 1}");
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j],5}");
            }
            Console.WriteLine();
        }
    }
}