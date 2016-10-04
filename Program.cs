using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace CocurrentCollections1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, int> stock = new ConcurrentDictionary<string, int>();
            stock.Add("jDays",4);
            stock.Add("technlogyhour",3);
            
            Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            stock.Add("pluralsight", 5);
            stock["buddhistgeeks"] = 5;

            stock["pluralsight"] = 7; // up from 6 - we just bought one

            Console.WriteLine(string.Format("\r\nstock[pluralsight] = {0}", stock["pluralsight"]));

            stock.Remove("jDays");

            Console.WriteLine("\r\nEnumerating:");
            foreach (var keyValPair in stock)
            {
                Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            }

            Console.ReadKey();
        }
    }
}
