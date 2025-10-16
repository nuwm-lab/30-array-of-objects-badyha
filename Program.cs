using System;
using System.Text;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 

    class Result
    { 
    // TODO: do it!
    }
    
    class Triangle
    {
        public (double x, double y) A, B, C;

        public Triangle(double ax, double ay, double bx, double by, double cx, double cy)
        {
            A = (ax, ay);
            B = (bx, by);
            C = (cx, cy);
            Console.WriteLine("Triangle created.");
        }

        ~Triangle()
        {
            Console.WriteLine("Triangle destroyed.");
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
                double ax = rnd.Next(-50, 51);
                double ay = rnd.Next(-50, 51);
                double bx = rnd.Next(-50, 51);
                double by = rnd.Next(-50, 51);
                double cx = rnd.Next(-50, 51);
                double cy = rnd.Next(-50, 51);

                triangles[i] = new Triangle(ax, ay, bx, by, cx, cy);
                Console.WriteLine($"Трикутник {i + 1}: A({ax};{ay}), B({bx};{by}), C({cx};{cy})");
            }

            double maxArea = 0;
            int maxIndex = -1;
            for (int i = 0; i < n; i++)
            {
                double area = triangles[i].Area();
                Console.WriteLine($"Площа трикутника {i + 1}: {area}");
                if (area > maxArea)
                {
                    maxArea = area;
                    maxIndex = i;
                }
            }

            Console.WriteLine($"Трикутник з найбільшою площею: #{maxIndex + 1}, площа = {maxArea}");

            triangles = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
