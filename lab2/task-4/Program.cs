using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of rows (n): ");
        int n = int.Parse(Console.ReadLine());

        int[][] jagged = new int[n][];
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter number of elements in row {i + 1}: ");
            int m = int.Parse(Console.ReadLine());
            jagged[i] = new int[m];

            for (int j = 0; j < m; j++)
            {
                jagged[i][j] = rnd.Next(1, 10); 
            }
        }

        Console.WriteLine("\nJagged array:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Row {i + 1}: ");
            foreach (int val in jagged[i])
                Console.Write(val + " ");
            Console.WriteLine();
        }

        Console.Write("\nEnter k1 (start row index, 1-based): ");
        int k1 = int.Parse(Console.ReadLine()) - 1;

        Console.Write("Enter k2 (end row index, 1-based): ");
        int k2 = int.Parse(Console.ReadLine()) - 1;

        int maxCols = 0;
        for (int i = 0; i < n; i++)
            if (jagged[i].Length > maxCols)
                maxCols = jagged[i].Length;

        int[] result = new int[maxCols];

        for (int col = 0; col < maxCols; col++)
        {
            int product = 1;
            bool hasElement = false;

            for (int row = k1; row <= k2 && row < n; row++)
            {
                if (col < jagged[row].Length)
                {
                    product *= jagged[row][col];
                    hasElement = true;
                }
            }

            result[col] = hasElement ? product : 0;
        }

        Console.WriteLine("\nResult array (products):");
        for (int i = 0; i < result.Length; i++)
            Console.Write(result[i] + " ");
    }
}
