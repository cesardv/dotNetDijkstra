namespace Dijkstra
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    using global::Dijkstra.DataStructures;

    using My = global::Dijkstra.Helpers;

    public class Program
    {
        public static void Main(string[] args)
        {

            PrintHeader();

            var validfile = string.Empty;

            if (args.Count() != 0 && File.Exists(args[0])) // If cmd-line passed filepath is wrong fail silently and prompt
            {
                Console.WriteLine("Loading passed commandline params ...");
                validfile = Path.GetFullPath(args[0]);
            }
            else
            {
                // Ask user to input file location
                var tries = 3;

                while (tries >= 0)
                {
                    Console.WriteLine("Please enter the file path of the graph (data) file: (Ex: C:\\graphs\\g1.txt)");
                    Console.Write("Filepath:> ");
                    var infilepath = Console.ReadLine();
                    if (File.Exists(infilepath))
                    {
                        validfile = infilepath;
                        break;
                    }

                    tries--;

                    if (tries < 0)
                    {
                        Console.WriteLine(
                            "You have exceeded the allowed number of tries, the program will now exit!\nBye");
                        Console.ReadKey();
                        return;
                    }
                }
            }
            
            // now parse file and create data structure
            Console.WriteLine("Loading data from file: " + Path.GetFileName(validfile));
            try
            {
                //var binheap = My.GraphReader.CreateBinHeap(validfile);
                var graph = My.GraphReader.CreateGraphList(validfile);
                var sourceNodeDesired = graph.First();
                var idofsourcenode = sourceNodeDesired.Id;
                var dijkstra = new Dijkstra(graph, sourceNodeDesired);
                var result = dijkstra.Run();
                //result.Remove(sourceNodeDesired); // source has distance 0 
                //var shortestPath = result.Min(n => n.Distance);
                var res = result.TakeWhile(n => n.ParentNode != null);
                var lenghtofSPTree = res.Sum(n => n.Distance);

                PrintResults(res);

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
            }
        }

        protected static void PrintResults(IEnumerable<Node> res)
        {
            var outStr = string.Format("From source node #{} the shortests distances are as follows");
        }

        private static void PrintHeader()
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("* Dijkstra's Algorithm C# Implementation *");
            Console.WriteLine("******************************************");
            Console.WriteLine("\n  Created by Cesar D Velez-R for CSC4250  - Dr. Alex Zelikovsky Spring 2014\n");
            Console.WriteLine(">>See README.txt file in case of any questions on how this program works<<\n");
        }
    }
}
