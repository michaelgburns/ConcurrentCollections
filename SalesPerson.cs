﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CocurrentCollections1
{
    public class SalesPerson
    {
        public string Name { get; private set; }

        public SalesPerson(string id)
        {
            this.Name = id;
        }

        public void Work(StockController stockController, TimeSpan workDay)
        {
            Random rand = new Random(Name.GetHashCode());
            DateTime start = DateTime.Now;
            while (DateTime.Now - start < workDay)
            {
                Thread.Sleep(rand.Next(100));
                bool buy = (rand.Next(100) == 0);
                string itemName = Program.AllShirtNames[rand.Next(Program.AllShirtNames.Count)];
                if (buy)
                {
                    int quantity = rand.Next(9) + 1;
                    stockController.BuyStock(itemName, quantity);
                    DisplayPlayPurchase(itemName, quantity);
                }
                else
                {
                    bool success = stockController.TrySellItem(itemName);
                    DisplaySaleAttempt(success, itemName);
                }
            }
            Console.WriteLine("SalesPerson {0} signing off", this.Name);
        }

        private void DisplaySaleAttempt(bool success, string itemName)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (success)
            {
                Console.WriteLine(string.Format("Thread {0}: {1} sold {2}", threadId, this.Name, itemName));
            }
            else
            {
                Console.WriteLine(string.Format("Thread {0}: {1}: Out of stock", threadId, this.Name));
            }
           
        }

        private void DisplayPlayPurchase(string itemName, int quantity)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(string.Format("Thread {0}: {1} bought {2} of {3}", Thread.CurrentThread.ManagedThreadId, this.Name, quantity, itemName));
        }

    }
}
