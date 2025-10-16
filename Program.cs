using System;
using System.Text;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 

    // Simple record for returning index and area of the triangle with max area
    public record Result(int Index, double Area);
    
    // Small immutable point record with PascalCase members
    public record Point(double X, double Y);

    public class Triangle
    {
        // Prefer exposing read-only properties rather than public fields
        public Point A { get; }
        public Point B { get; }
        public Point C { get; }

        public Triangle(double ax, double ay, double bx, double by, double cx, double cy)
        {
            A = new Point(ax, ay);
            B = new Point(bx, by);
            C = new Point(cx, cy);
        }

        // Area as a property; computed on demand
        public double Area => Math.Abs(
                (A.X * (B.Y - C.Y) +
                 B.X * (C.Y - A.Y) +
                 C.X * (A.Y - B.Y)) / 2.0
            );

        // True if points are colinear (area == 0)
        public bool IsDegenerate => Area == 0.0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.Write("Введіть кількість трикутників: ");
            int n;
            string line;
            while ((line = Console.ReadLine()) == null || !int.TryParse(line, out n) || n <= 0)
            {
                Console.Write("Введіть коректне додатнє число: ");
            }

            Triangle[] triangles = new Triangle[n];
            var rnd = new Random();

            const double Range = 50.0; // coordinates will be generated in [-Range, Range]

            for (int i = 0; i < n; i++)
            {
                // Use NextDouble scaled to [-Range,Range] to allow fractional coordinates
                double ax = (rnd.NextDouble() * (2 * Range)) - Range;
                double ay = (rnd.NextDouble() * (2 * Range)) - Range;
                double bx = (rnd.NextDouble() * (2 * Range)) - Range;
                double by = (rnd.NextDouble() * (2 * Range)) - Range;
                double cx = (rnd.NextDouble() * (2 * Range)) - Range;
                double cy = (rnd.NextDouble() * (2 * Range)) - Range;

                triangles[i] = new Triangle(ax, ay, bx, by, cx, cy);
                Console.WriteLine($"Трикутник {i + 1}: A({ax:F2},{ay:F2}), B({bx:F2},{by:F2}), C({cx:F2},{cy:F2})");
            }

            // Find triangle with maximum area using a separate method
            Result result = FindMaxArea(triangles);

            if (result != null && result.Index >= 0)
            {
                Console.WriteLine($"Трикутник з найбільшою площею: #{result.Index + 1}, площа = {result.Area:F2}");
            }
            else
            {
                Console.WriteLine("Не вдалось визначити трикутник з найбільшою площею.");
            }
        }

        // Returns the index and area of the triangle with the maximum area.
        // Guarantees correct behavior for any non-empty array.
        public static Result FindMaxArea(Triangle[] triangles)
        {
            if (triangles == null || triangles.Length == 0)
                return null;

            // Initialize from first triangle to guarantee a valid index
            double maxArea = triangles[0].Area;
            int maxIndex = 0;

            for (int i = 1; i < triangles.Length; i++)
            {
                double area = triangles[i].Area;
                // Optionally, write per-triangle logging outside of this method
                if (area > maxArea)
                {
                    maxArea = area;
                    maxIndex = i;
                }
            }

            return new Result(maxIndex, maxArea);
        }
    }
}
