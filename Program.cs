using System;
using System.Text;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 

    // Result holds index and area of the triangle with maximum area
    public class Result
    {
        public int Index { get; }
        public double Area { get; }

        public Result(int index, double area)
        {
            Index = index;
            Area = area;
        }
    }
    
    public class Triangle
    {
        // Expose coordinates via read-only properties (encapsulation)
        public (double X, double Y) A { get; }
        public (double X, double Y) B { get; }
        public (double X, double Y) C { get; }

        public Triangle(double ax, double ay, double bx, double by, double cx, double cy)
        {
            A = (ax, ay);
            B = (bx, by);
            C = (cx, cy);
            // Avoid logging in constructor; use external logging if needed
        }

        // Area as a property (computed on demand)
        public double Area
        {
            get
            {
                return Math.Abs(
                    (A.X * (B.Y - C.Y) +
                     B.X * (C.Y - A.Y) +
                     C.X * (A.Y - B.Y)) / 2.0
                );
            }
        }

        // Use epsilon for floating-point comparison
        private const double Epsilon = 1e-9;

        public bool IsDegenerate => Math.Abs(Area) < Epsilon;
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
                double ax = rnd.Next(-50, 51);
                double ay = rnd.Next(-50, 51);
                double bx = rnd.Next(-50, 51);
                double by = rnd.Next(-50, 51);
                double cx = rnd.Next(-50, 51);
                double cy = rnd.Next(-50, 51);

                triangles[i] = new Triangle(ax, ay, bx, by, cx, cy);
                Console.WriteLine($"Трикутник {i + 1}: A({ax};{ay}), B({bx};{by}), C({cx};{cy})");
                Console.WriteLine($"Площа трикутника {i + 1}: {triangles[i].Area:F2}{(triangles[i].IsDegenerate ? " (вироджений)" : string.Empty)}");
            }

            var result = FindMaxArea(triangles);
            if (result == null)
            {
                Console.WriteLine("Не вдалося знайти трикутник з ненульовою площею.");
            }
            else
            {
                Console.WriteLine($"Трикутник з найбільшою площею: #{result.Index + 1}, площа = {result.Area:F2}");
            }
        }

        // Finds the triangle with the maximum area (skipping degenerate triangles).
        // Returns null if input is null/empty or no non-degenerate triangle exists.
        public static Result FindMaxArea(Triangle[] triangles)
        {
            if (triangles == null || triangles.Length == 0)
                return null;

            int start = -1;
            for (int i = 0; i < triangles.Length; i++)
            {
                if (!triangles[i].IsDegenerate)
                {
                    start = i;
                    break;
                }
            }

            if (start == -1)
                return null;

            double maxArea = triangles[start].Area;
            int maxIndex = start;

            for (int i = start + 1; i < triangles.Length; i++)
            {
                if (triangles[i].IsDegenerate)
                    continue;

                double area = triangles[i].Area;
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
