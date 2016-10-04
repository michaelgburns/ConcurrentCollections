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
            var orders = new ConcurrentQueue<string>();
            //PlaceOrders(orders, "Mark");
            //PlaceOrders(orders, "Ramdevi");
            Task task1 = Task.Run(() => PlaceOrders(orders, "Mark"));
            Task task2 = Task.Run(() => PlaceOrders(orders, "Ramdevi"));
            Task.WaitAll(task1, task2);

            foreach (string order in orders)            
                Console.WriteLine("ORDER: " + order);

            Console.WriteLine("_______________________________________________________");
            Parallel.ForEach(orders, ProcessOrders);
            
            Console.ReadLine();
        }

        private static void ProcessOrders(string order)
        {
            Console.WriteLine("Inside the parallel foreach : {0}", order);
        }

        //static object _lockObj = new object();

        private static void PlaceOrders(ConcurrentQueue<string> orders, string customerName)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                string orderName = string.Format("{0} wants t-shirt {1}", customerName, i + 1);

                //lock (_lockObj)
                //{
                //    orders.Enqueue(orderName);
                //} 
                orders.Enqueue(orderName);
            }
        }
    }
}
