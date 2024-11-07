using System;
using System.Diagnostics;

public class PerfomanceTest
{
    public static void RunTests()
    {
        int[] smallVector = new int[1000]; 
        int[] mediumVector = new int[100000];
        int[] bigVector = new int[10000000];

        for (int i = 0; i < bigVector.Length; i++)
        {
            if (i < smallVector.Length)
                smallVector[i] = i + 1;
            if (i < mediumVector.Length)
                mediumVector[i] = i + 1;
            bigVector[i] = i + 1;
        }

        Console.WriteLine("Однопотокова версія:");
        double singleTimeSmall = TestSingleThread(smallVector, "Small Vector");
        double singleTimeMedium = TestSingleThread(mediumVector, "Medium Vector");
        double singleTimeBig = TestSingleThread(bigVector, "Big Vector");

        Console.WriteLine("Багатопотокова версія");
        int[] threadCounts = {2, 4, 8, 16};
        foreach (int threadCount in threadCounts)
        {
            Console.WriteLine($"\nКількість потоків: {threadCount}");
            double multiTimeSmall = TestMultiThread(smallVector, "Small Vector", threadCount);
            double multiTimeMedium = TestMultiThread(mediumVector, "Medium Vector", threadCount);
            double multiTimeBig = TestMultiThread(bigVector, "Big Vector", threadCount);

            Console.WriteLine($"Прискорення для Small Vector: {singleTimeSmall / multiTimeSmall}");
            Console.WriteLine($"Прискорення для Medium Vector: {singleTimeMedium / multiTimeMedium}");
            Console.WriteLine($"Прискорення для Big Vector: {singleTimeBig / multiTimeBig}");
        }
    }

    static double TestSingleThread(int[] vector, string vectorName)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        SingleThread.CalculateMean(vector);
        stopwatch.Stop();
        Console.WriteLine($"{vectorName}: Час виконання (однопотоковий) = {stopwatch.ElapsedTicks} ticks");
        return stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1_000_000_000;
    }

    static double TestMultiThread(int[] vector, string vectorName, int threadCount)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        MultiThread.CalculateMean(vector, threadCount);
        stopwatch.Stop();
        Console.WriteLine($"{vectorName}: Час виконання (багатопотоковий) = {stopwatch.ElapsedTicks} ticks");
        return stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1_000_000_000;
    }
}