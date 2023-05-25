using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Diagnostics;

#if NET472
using System.Media;
#endif

namespace rat
{
    class Program
    {
        static string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);

        }
        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        static void Main(string[] args)
        {
            bool ratmarkEn = false;
            bool uncappedEn = false;

            var timer1 = new Stopwatch();

            foreach (string argm in args)
            {
                if(argm == "-m")
                {
                    ratmarkEn = true;
                } else if (argm == "-u")
                {
                    uncappedEn = true;
                }
            }

            if(ratmarkEn)
            {
                timer1.Start();
            }

            var culture = CultureInfo.InvariantCulture;

            var resourceManager = Properties.Resources.ResourceManager;
            var resourceSet = resourceManager.GetResourceSet(culture, createIfNotExists: true, tryParents: true);

            int[] numCount = Enumerable.Range(1, 31).ToArray();
            int spinCount = 0;

#if NET472
            SoundPlayer freebird;
            var assembly = Assembly.GetExecutingAssembly();
            freebird = new SoundPlayer(assembly.GetManifestResourceStream("rat.audio.wav"));
            freebird.PlayLooping();
#endif

            Console.Clear();

            if (ratmarkEn)
            {
                for (int i = 0; i < 250; i++)
                {
                    int cols = Console.WindowWidth, rows = Console.WindowHeight;
                    foreach (int num in numCount)
                    {
                        string resourceName = "rat" + num;
                        string rets = resourceSet.GetString(resourceName);

                        var resourceBytes = System.Convert.FromBase64String(rets);

                        string spinCountText = $"You have been blessed with {spinCount.ToString()} spins of 250.";
                        spinCountText = PadBoth(spinCountText, 160);
                        spinCountText = "\n" + spinCountText + "\n";

                        Console.SetCursorPosition(0, 0);

                        byte[] buffer = Combine(Encoding.ASCII.GetBytes(spinCountText), resourceBytes);
                        using (Stream stdout = Console.OpenStandardOutput(cols * rows))
                        {
                            stdout.Write(buffer, 0, buffer.Length);
                        }
                    }
                    spinCount++;
                }
                timer1.Stop();
                TimeSpan timeTaken = timer1.Elapsed;
                string benchmarkTime = timeTaken.ToString(@"ss\.fff");
                Console.WriteLine();
                Console.WriteLine($"ratmark completed - 250 spins in {benchmarkTime} seconds.");
            }
            else
            {
                while (true)
                {
                    int cols = Console.WindowWidth, rows = Console.WindowHeight;
                    foreach (int num in numCount)
                    {
                        string resourceName = "rat" + num;
                        string rets = resourceSet.GetString(resourceName);

                        byte[] resourceBytes = System.Convert.FromBase64String(rets);

                        string spinCountText = $"You have been blessed with {spinCount.ToString()} spins of the rat.";
                        spinCountText = PadBoth(spinCountText, 160);
                        spinCountText = "\n" + spinCountText + "\n";

                        Console.SetCursorPosition(0, 0);

                        byte[] buffer = Combine(Encoding.ASCII.GetBytes(spinCountText), resourceBytes);
                        using (Stream stdout = Console.OpenStandardOutput(cols * rows))
                        {
                            stdout.Write(buffer, 0, buffer.Length);
                        }
                        if(!uncappedEn)
                        {
                            Thread.Sleep(100);
                        }
                    }
                    spinCount++;
                }

            }
        }
    }
}
