using PiePopper.Crackers;
using PiePopper.Scripts;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace PiePopper
{
    public class Program
    {
        /// <summary>
        /// File size references.
        /// </summary>
        private static string[] SizeReferences { get; } = new string[] { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

        public static void Main(string[] args)
        {
            // Is the input valid?
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("Input invalid.");
            }
            else
            {
                // Parse the input file.
                if (!IsSupported(args[0], out Popper popper, out PieHitList details))
                {
                    Console.WriteLine($"{Path.GetFileName(args[0])} is not supported yet!");
                }
                else
                {
                    // Cache details.
                    string file = Path.GetFileName(args[0]);
                    string name = popper.GetType().Name;
                    string sizeBefore = FormatSizeFromBytes(File.ReadAllBytes(args[0]).Length);

                    Console.WriteLine($"{file} -> {popper.GetType()} by {details.authorName}[{details.version} @ {details.dateCracked}]");
                    Console.WriteLine("Starting crack . . .\n");

                    // Does the target use patreoncore?
                    if (details.usesPatreonCore && !(popper is PatreonCore))
                    {
                        Console.WriteLine("Cracking PatreonCore . . .\n");
                        PatreonCore core = new PatreonCore();
                        core.Bake(args[0]);

                        if (core.Crack())
                        { Console.WriteLine("Cracked PatreonCore!\n"); }
                        else
                        { Console.WriteLine("Unable to crack PatreonCore!\n"); }
                    }

                    Console.WriteLine("Cleaning assembly from his shitty attempt at obfuscation. . .\n");
                    De4dot.Clean(args[0], out string cleaned);
                    File.Copy(args[0], Path.Combine(Directory.GetParent(args[0]).FullName, $"ORIGINAL-{file}"), true);
                    File.Copy(cleaned, args[0], true);
                    File.Delete(cleaned);
                    Console.WriteLine($"Cleaning complete, file size before '{sizeBefore}', size AFTER '{FormatSizeFromBytes(File.ReadAllBytes(args[0]).Length)}'!\n");

                    Console.WriteLine($"Baking up a storm in {name} . . .\n");
                    popper.Bake(args[0]);
                    Console.WriteLine($"{name} baked and ready . . .\n");

                    Console.WriteLine($"Cracking {file}!\n");
                    Console.WriteLine($"=== [ {name} ] ===");
                    if (popper.Crack())
                    {
                        Console.WriteLine($"=== [ END ] ===\n");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Crack Complete!\nFuck you piepop.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine($"=== [ END ] ===\n");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("It's possible the crack may need updating OR this mod doesn't need cracking(!!), please report this with the following information:\n\n" +
                                               $"Crack = {name} by {details.authorName} [for {details.version} @ {details.dateCracked}]\n" +
                                               $"File = [wants {details.crackName}] {file}\n" +
                                               $"Attempt Date = {DateTime.Now.ToShortDateString()} using {file}\n"
                                         );
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            Console.WriteLine("\n\nPress any key to exit . . .");
            while (!Console.KeyAvailable)
            { Thread.Sleep(1); }
        }

        /// <summary>
        /// Is the file supported?
        /// </summary>
        private static bool IsSupported(string file, out Popper popper, out PieHitList details)
        {
            Type[] crackers = typeof(Popper).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Popper))).ToArray();

            foreach (Type type in crackers)
            {
                // Special exclusion.
                if (type == typeof(PatreonCore))
                { continue; }

                PieHitList tmp = (PieHitList)Attribute.GetCustomAttribute(type, typeof(PieHitList));

                if (tmp != null && file.Contains(tmp.crackName))
                {
                    details = tmp;
                    popper = (Popper)Activator.CreateInstance(type);
                    return true;
                }
            }

            // Attempt patreon core anyway.
            popper = new PatreonCore();
            details = (PieHitList)Attribute.GetCustomAttribute(popper.GetType(), typeof(PieHitList));
            return true;
        }

        /// <summary>
        /// Convert the input bytes to a readable size..
        /// </summary>
        private static string FormatSizeFromBytes(int byteCount)
        {
            if (byteCount == 0)
            { return "0" + SizeReferences[0]; }

            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 2);

            return (Math.Sign(byteCount) * num).ToString() + SizeReferences[place];
        }
    }
}