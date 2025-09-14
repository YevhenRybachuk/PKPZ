using System;

class Program
{
    static string CheckPoint(double x, double y, double R)
    {
        if (x >= 0 && y >= 0)
        {
            double dist = x * x + y * y;
            if (Math.Abs(dist - R * R) < 1e-9) return "On the edge";
            if (dist < R * R) return "Yes";
            return "No";
        }

        if (x <= 0 && y <= 0 && x >= -R && y >= -R)
        {
            double line = x + y + R;
            if (Math.Abs(line) < 1e-9 || Math.Abs(x + R) < 1e-9 || Math.Abs(y + R) < 1e-9)
                return "On the edge";
            if (line > 0) return "Yes";
            return "No";
        }

        return "No";
    }

    static void Main()
    {
        Console.WriteLine();
        Console.Write("Enter R: ");
        double R = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter x: ");
        double x = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter y: ");
        double y = Convert.ToDouble(Console.ReadLine());

        string result = CheckPoint(x, y, R);
        Console.WriteLine("Result: " + result);
    }
}