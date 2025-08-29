namespace clock
{
    internal class Program
    {
        static string bg = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            int radius = getNumber("Enter a radius", 1);

            Console.Clear();

            do
            {
                drawClock(radius);
            }
            while (true);
        }

        static void drawClock(int r)
        {
            Console.SetCursorPosition(0, 0);

            // If we don't have the circle background, get it
            if (bg == "")
            {
                bg = getBG(r);
            }

            // Draw the circle background
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(bg);

            // Draw the time at the bottom
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nTime: {DateTime.Now.ToString()}");

            // draw hands
            double ms = DateTime.Now.Millisecond;
            double sec = DateTime.Now.Second;
            double min = DateTime.Now.Minute;
            double hou = DateTime.Now.Hour;

            sec += (ms / 1000);
            min += (sec / 60);
            hou += (min / 60);

            int secHandLength = (int)Math.Round(r - (double)Math.Max(r / 10, 2));
            secHandLength = (int)(secHandLength * 0.95);
            int minHandLength = (int)(secHandLength * 0.8);
            int houHandLength = (int)(secHandLength * 0.7);

            drawLine(r, (sec / 60) * Math.PI * 2, secHandLength, ConsoleColor.Red);
            drawLine(r, (min / 60) * Math.PI * 2, minHandLength, ConsoleColor.Blue);
            drawLine(r, (hou / 12) * Math.PI * 2, houHandLength, ConsoleColor.Green);
        }

        static void drawLine(int r, double a, int length, ConsoleColor c)
        {
            Console.ForegroundColor = c;

            for (double l = 0; l < length; l += 0.5)
            {
                int cx = (int)((r + (l * Math.Sin(a))) * 2);
                int cy = (int)(r - (l * Math.Cos(a)));

                if (cx >= Console.BufferWidth)
                {
                    cx = Console.BufferWidth - 1;
                }

                if (cy >= Console.BufferHeight)
                {
                    cy = Console.BufferHeight - 1;
                }

                Console.SetCursorPosition(cx, cy);
                Console.Write("█");
            }
        }

        public static string getBG(int r)
        {
            double d = 0;
            int lineWidth = r / 10;

            if (lineWidth < 2)
            {
                lineWidth = 2;
            }

            string cbg = "";

            for (double y = 1; y < r * 2; y++)
            {
                for (double x = 0.5; x < r * 2; x += 0.5)
                {
                    d = distanceFormula(x - r, y - r);

                    if (d <= r)
                    {
                        if (Math.Round(d) > r - lineWidth)
                        {
                            cbg += "█";
                        }
                        else
                        {
                            cbg += " ";
                        }
                    }
                    else
                    {
                        if (x > r)
                        {
                            x = r * 2;
                        }
                        else
                        {
                            cbg += " ";
                        }
                    }
                }

                cbg += "\n";
            }

            return cbg;
        }

        public static int getNumber(string dialogue, int min)
        {
            int l = -1;

            do
            {
                try
                {
                    Console.Write($"{dialogue}: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    l = int.Parse(Console.ReadLine());

                    if (l <= min)
                    {
                        throw new FormatException($"Input must be over {min}."); // Throw an exception.
                    }
                }
                catch (FormatException ex) // If the input is invalid...
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"ERROR: {ex.Message}\n"); // ...Display an error message.
                }
                catch (System.OverflowException ex) // If the system overflows...
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"ERROR: {ex.Message}\n"); // ...Display an error message.
                }
                finally
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            while (l <= min);

            return l;
        }

        public static double distanceFormula(double x, double y)
        {
            return Math.Abs(Math.Sqrt((x * x) + (y * y)));
        }
    }
}
