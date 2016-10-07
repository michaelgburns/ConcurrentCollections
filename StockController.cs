using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

namespace CocurrentCollections1
{
    public class StockController
    {
        ConcurrentDictionary<string, int> _stock = new ConcurrentDictionary<string, int>();
        int _totalQuantityBought;
        int _totalQuantitySold;

        internal void DisplayStatus()
        {
            Console.WriteLine();
            Console.WriteLine("Total quantity bought " + _totalQuantityBought);
            Console.WriteLine();
            Console.WriteLine("The total quantity sold " + _totalQuantitySold);
        }

        public void BuyStock(string itemName, int quantity)
        {
            _stock.AddOrUpdate(itemName, quantity, (key, oldValue) => oldValue + quantity);
            Interlocked.Add(ref _totalQuantityBought, quantity);
        }

        public bool TrySellItem(string item)
        {
            bool success = false;
            int newStockLevel = _stock.AddOrUpdate(item, (itemName) => { success = false; return 0; }, (itemName, oldValue) => 
            {
                if (oldValue == 0)
                {
                    success = false;
                    return 0;
                }
                else
                {
                    success = true;
                    return oldValue - 1;
                }
            });

            if (success)
            {
                Interlocked.Increment(ref _totalQuantitySold);
            }
            return success;
        }
    }
}
