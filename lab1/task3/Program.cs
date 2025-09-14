using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Chessboard 8x8 (1,1 - bottom left corner).");

        (int x, int y) king = ReadCoordinates("White King");
        (int x, int y) pawn = ReadCoordinates("White Pawn");
        (int x, int y) knight = ReadCoordinates("Black Knight");

        if (AreSameCell(king, pawn) || AreSameCell(king, knight) || AreSameCell(pawn, knight))
        {
            Console.WriteLine("Error: two or more pieces are on the same square.");
            return;
        }

        Console.WriteLine("\nWho makes the first move? (1 - King, 2 - Pawn, 3 - Knight)");
        int choice = int.Parse(Console.ReadLine() ?? "0");

        switch (choice)
        {
            case 1:
                KingMove(king, pawn, knight);
                break;
            case 2:
                PawnMove(pawn, king, knight);
                break;
            case 3:
                KnightMove(knight, king, pawn);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void KingMove((int x, int y) king, (int x, int y) pawn, (int x, int y) knight)
    {
        if (IsKingAttack(king, knight))
            Console.WriteLine("White King attacks the Black Knight.");
        else if (IsKnightAttack(knight, pawn) && IsKingAttack(king, pawn))
            Console.WriteLine("White King defends the White Pawn from the Black Knight.");
        else
            Console.WriteLine("White King makes a simple move.");
    }

    static void PawnMove((int x, int y) pawn, (int x, int y) king, (int x, int y) knight)
    {
        if (IsPawnAttack(pawn, knight))
            Console.WriteLine("White Pawn attacks the Black Knight.");
        else if (IsKnightAttack(knight, king) && IsPawnAttack(pawn, king))
            Console.WriteLine("White Pawn defends the White King.");
        else
            Console.WriteLine("White Pawn makes a simple move.");
    }

    static void KnightMove((int x, int y) knight, (int x, int y) king, (int x, int y) pawn)
    {
        if (IsKnightAttack(knight, pawn))
            Console.WriteLine("Black Knight attacks the White Pawn.");
        else if (IsKnightAttack(knight, king))
            Console.WriteLine("Black Knight attacks the White King.");
        else
            Console.WriteLine("Black Knight makes a simple move.");
    }
    static bool IsKingAttack((int x, int y) king, (int x, int y) other)
    {
        return Math.Abs(king.x - other.x) <= 1 && Math.Abs(king.y - other.y) <= 1;
    }

    static bool IsPawnAttack((int x, int y) pawn, (int x, int y) other)
    {
        return (other.y == pawn.y + 1) && (other.x == pawn.x + 1 || other.x == pawn.x - 1);
    }

    static bool IsKnightAttack((int x, int y) knight, (int x, int y) other)
    {
        int dx = Math.Abs(knight.x - other.x);
        int dy = Math.Abs(knight.y - other.y);
        return (dx == 2 && dy == 1) || (dx == 1 && dy == 2);
    }

    static (int, int) ReadCoordinates(string name)
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write($"{name} (x y): ");
            string[] parts = (Console.ReadLine() ?? "").Split();

            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y) &&
                x >= 1 && x <= 8 &&
                y >= 1 && y <= 8)
            {
                return (x, y);
            }

            Console.WriteLine("Invalid input! Please enter two numbers between 1 and 8 (example: 4 5).");
        }
    }

    static bool AreSameCell((int x, int y) a, (int x, int y) b)
    {
        return a.x == b.x && a.y == b.y;
    }
}
