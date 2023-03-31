using System;
namespace Task4
{
    internal class Program
    {
        static int threads = 4;
        static Semaphore semaphore = new Semaphore(threads, threads);
        static object lockObj = new object();
        static void Main(string[] args)
        {
            int[,] matrixA = new int[4, 4];
            int[,] matrixB = new int[4, 4];
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrixA[i, j] = rand.Next(10);
                    matrixB[i, j] = rand.Next(10);
                }
            }
            int[,] result = new int[matrixA.GetLength(0), matrixB.GetLength(1)];
            Thread[] workers = new Thread[threads];
            for (int i = 0; i < threads; i++)
            {
                workers[i] = new Thread(() => Multiply(matrixA, matrixB, result));
                workers[i].Start();
            }
            foreach (Thread worker in workers)
            {
                worker.Join();
            }
            Console.WriteLine("Multiply matrix:");
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Console.Write(result[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        static void Multiply(int[,] matrixA, int[,] matrixB, int[,] result)
        {
            semaphore.WaitOne();
            try
            {
                for (int i = 0; i < matrixA.GetLength(0); i++)
                {
                    for (int j = 0; j < matrixB.GetLength(1); j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < matrixB.GetLength(0); k++)
                        {
                            sum += matrixA[i, k] * matrixB[k, j];
                        }
                        lock (lockObj)
                        {
                            result[i, j] += sum;
                        }
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}