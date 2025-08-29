namespace clock
{
    internal class Program
    {
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

            double d = 0;

            string bg = "";

            for (double y = 1; y < r * 2; y++)
            {
                for (double x = 0.5; x < r * 2; x += 0.5)
                {
                    d = distanceFormula(x - r, y - r);

                    if (d <= r)
                    {
                        if (Math.Round(d) > r - (r / 10))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            bg += "█";
                        }
                        else
                        {
                            bg += " ";
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
                            bg += " ";
                        }
                    }
                }

                bg += "\n";
            }

            Console.WriteLine(bg);

            // Draw time at the bottom
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

            drawLine(r, (sec / 60) * Math.PI * 2, r - 3, ConsoleColor.Red);
            drawLine(r, (min / 60) * Math.PI * 2, r - 5, ConsoleColor.Blue);
            drawLine(r, (hou / 12) * Math.PI * 2, r - 7, ConsoleColor.Green);
        }

        static void drawLine(int r, double a, int length, ConsoleColor c)
        {
            Console.ForegroundColor = c;

            for (double l = 0; l < length; l += 0.5)
            {
                Console.SetCursorPosition((int)((r + (l * Math.Sin(a))) * 2), (int)(r - (l * Math.Cos(a))) );
                Console.Write("█");
            }
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
