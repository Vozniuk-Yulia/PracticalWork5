using System;
using System.Collections.Concurrent;

namespace Task1
{
    internal class Program
    {
        static BlockingCollection<int> buffer = new BlockingCollection<int>();
        static void Producer()
        {
            var random = new Random();
            while (true)
            {
                int value = random.Next(100);
                buffer.Add(value);
                Console.WriteLine("Produced: " + value);
                Thread.Sleep(100);
            }
            buffer.CompleteAdding();

        }
        static void Consumer()
        {
            while (true)
            {
                foreach (var item in buffer.GetConsumingEnumerable())
                {
                    Console.WriteLine("Consumed: " + item);
                    Thread.Sleep(250);
                }

            }
        }
        static void Main(string[] args)
        {
            var producerThread = new Thread(Producer);
            var consumerThread = new Thread(Consumer);
            producerThread.Start();
            consumerThread.Start();
            producerThread.Join();
            consumerThread.Join();

        }
    }
}