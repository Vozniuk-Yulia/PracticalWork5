using System;
namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 34, 3, 23, 81, 5, 0, 29, 59, 7 };
            foreach (int i in arr)
            {
                Console.Write(i + " ");
            }
            QuickSort(arr);
            Console.WriteLine();
            Console.WriteLine("After sort:");
            foreach (int i in arr)
            {
                Console.Write(i + " ");
            }

        }
        static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int nucleusIndex = Overrun(arr, left, right);

                Thread leftThread = new Thread(() => QuickSort(arr, left, nucleusIndex - 1));
                Thread rightThread = new Thread(() => QuickSort(arr, nucleusIndex + 1, right));
                leftThread.Start();
                rightThread.Start();
                leftThread.Join();
                rightThread.Join();
            }
        }

        static int Overrun(int[] arr, int left, int right)
        {
            int nucleus = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] < nucleus)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }

        static void Swap(int[] arr, int i, int j)
        {
            int tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }
    }
}