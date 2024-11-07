using System;
using System.Threading;

public class MultiThread
{
    private static int sum = 0;
    private static object lockObj = new object();

    public static void CalculateMean(int[] vector, int threadCount)
    {
        sum = 0;
        Thread[] threads = new Thread[threadCount];
        int elementsPerThread = vector.Length / threadCount;

        for (int i = 0; i < threadCount; i++)
        {
            int start = i * elementsPerThread;
            int end = (i == threadCount - 1) ? vector.Length : start + elementsPerThread;
            threads[i] = new Thread(() => CalculatePartialSum(vector, start, end));
            threads[i].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        double mean = (double)sum / vector.Length;
        Console.WriteLine($"Середнє арифметичне (багатопотоковий варіант): {mean}");
    }

    private static void CalculatePartialSum(int[] vector, int start, int end)
    {
        int localSum = 0;
        for (int i = start; i < end; i++)
        {
            localSum += vector[i];
        }

        lock (lockObj)
        {
            sum += localSum;
        }
    }
}