using System;
using System.Numerics;
using System.Threading;

class Program
{
    static void Main(){
        int[] vector = {6, 84, 65, 35, 91, 12, 9, 218, 64, 81};
        int threadCount = Environment.ProcessorCount;
        
        //SingleThread.CalculateMean(vector);
        //MultiThread.CalculateMean(vector, threadCount);

        PerfomanceTest.RunTests();
    }
}