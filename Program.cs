using System;
using System.Text;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 

    // Simple DTO for returning index and area of the triangle with max area
    class Result
    {
        public int Index { get; }
        public double Area { get; }

        public Result(int index, double area)
        {
            Index = index;
            Area = area;
        }
    }
    
    class Triangle
    {
        // Make coordinates readonly to prevent external modification
        public readonly (double x, double y) A;
        public readonly (double x, double y) B;
        public readonly (double x, double y) C;

        public Triangle(double ax, double ay, double bx, double by, double cx, double cy)
        {
            A = (ax, ay);
            B = (bx, by);
            C = (cx, cy);
            // Avoid logging in constructor. If needed use Debug.WriteLine outside.
        }

        public double Area()
        {
            // Формула площі трикутника за координатами
            return Math.Abs(
                (A.x * (B.y - C.y) +
                 B.x * (C.y - A.y) +
                 C.x * (A.y - B.y)) / 2.0
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.Write("Введіть кількість трикутників: ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.Write("Введіть коректне додатнє число: ");
            }

            Triangle[] triangles = new Triangle[n];
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                // Use NextDouble scaled to [-50,50] to allow fractional coordinates
                double ax = (rnd.NextDouble() * 100.0) - 50.0;
                double ay = (rnd.NextDouble() * 100.0) - 50.0;
                double bx = (rnd.NextDouble() * 100.0) - 50.0;
                double by = (rnd.NextDouble() * 100.0) - 50.0;
                double cx = (rnd.NextDouble() * 100.0) - 50.0;
                double cy = (rnd.NextDouble() * 100.0) - 50.0;

                triangles[i] = new Triangle(ax, ay, bx, by, cx, cy);
                Console.WriteLine($"Трикутник {i + 1}: A({ax:F2} ; {ay:F2}), B({bx:F2},{by:F2}), C({cx:F2} ; {cy:F2})");
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
            double maxArea = triangles[0].Area();
            int maxIndex = 0;

            for (int i = 1; i < triangles.Length; i++)
            {
                double area = triangles[i].Area();
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
