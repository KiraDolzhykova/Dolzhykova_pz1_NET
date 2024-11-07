using System;

public class SingleThread
{
    public static void CalculateMean(int[] vector)
    {
        int sum = 0;
        foreach (int element in vector) 
            sum += element;
        double mean = (double)sum / vector.Length;
        Console.WriteLine($"Середнє арифметичне (однопотоковий варіант): {mean}");
    }
}
