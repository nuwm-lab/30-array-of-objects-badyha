using System;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 

    class Result
    { 
    // TODO: do it !
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
            int n = 3;
            Triangle[] triangles = new Triangle[n];
            triangles[0] = new Triangle(0, 0, 4, 0, 0, 3);
            triangles[1] = new Triangle(1, 1, 5, 1, 1, 4);
            triangles[2] = new Triangle(0, 0, 2, 0, 0, 2);

            double maxArea = 0;
            int maxIndex = -1;
            for (int i = 0; i < n; i++)
            {
                double area = triangles[i].Area();
                Console.WriteLine($"Triangle {i + 1} area: {area}");
                if (area > maxArea)
                {
                    maxArea = area;
                    maxIndex = i;
                }
            }

            Console.WriteLine($"Triangle with max area is #{maxIndex + 1} with area {maxArea}");

            // Демонстрація роботи Garbage Collector
            triangles = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
