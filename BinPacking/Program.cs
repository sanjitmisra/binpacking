using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinPacking
{
    class Program
    {
        static List<Bin> binBox = new List<Bin>();
        static void Main(string[] args)
        {
            Random r = new Random();
            List<int> binPile = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                binPile.Add(r.Next(2, 9));
            }


            int totalPileSize = 0;
            foreach (int item in binPile)
            {
                totalPileSize += item;
            }
            Console.WriteLine("Total Pile Size: {0}", totalPileSize);
            Console.WriteLine("-------------------------------------");

            BinPacker(binPile, 30);
        }

        static void BinPacker(List<int> binPile, int binSize)
        {
            
            foreach (int x in binPile)
            {
                if (binBox.Count == 0)
                {
                    BinAdder(binSize, x);
                }
                else
                {
                    bool isItemAdded = false;
                    foreach (Bin y in binBox)
                    {
                        if (y.availableCapacity() > x)
                        {
                            y.addItem(new BinItem { Size = x });
                            isItemAdded = true;
                            break;
                        }
                    }
                    if (!isItemAdded)
                    {
                        BinAdder(binSize, x);
                    }
                }
            }

            Console.WriteLine("Total Number of Bins: {0}", binBox.Count);
            Console.WriteLine("-------------------------------------");

            int i = 1;
            foreach (Bin bin in binBox)
            {
                Console.WriteLine("Size of Bin {0} is {1}", i++, binSize - bin.availableCapacity());
            }
            Console.Read();
        }

        private static void BinAdder(int binSize, int x)
        {
            Bin bin = new Bin(binSize);
            BinItem binItem = new BinItem();

            binItem.Size = x;
            bin.addItem(binItem);

            binBox.Add(bin);
        }
    }

    
    public class Bin
    {
        private List<BinItem> contents;
        private int binSize;

        public Bin(int size)
        {
            this.binSize = size;
            contents = new List<BinItem>();
        }
        public int availableCapacity()
        {
            int usedCapacity = 0;
            foreach (BinItem x in contents)
            {
                usedCapacity += x.Size;
            }
            return binSize - usedCapacity;
        }
        public void addItem(BinItem item)
        {
            if (availableCapacity() > item.Size)
                contents.Add(item);
            else
                throw new OverflowException("Bin capacity exceeded");
        }
    }

    public class BinItem
    {
        private int size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}
