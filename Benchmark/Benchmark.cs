using SyncAsyncComparison.Interfaces;
using System.Diagnostics;

namespace SyncAsyncComparison.Benchmark
{
    /// <summary>
    /// Клас, який вимірює ча роботи заданого алгоритму над заданим масивом
    /// </summary>
    /// <param name="generator"></param>
    /// <param name="algorithm"></param>
    /// <param name="maxNumberOfThreads"></param>
    public class Benchmarking(IDataGenerator generator, ISortAlgorithm algorithm, int maxNumberOfThreads)
    {
        private readonly IDataGenerator generator = generator;
        private readonly ISortAlgorithm algorithm = algorithm;
        private int maxNumberOfThreads = maxNumberOfThreads;

        /// <summary>
        /// Вимірює час роботи алгоритму сортування розміру dataSize
        /// </summary>
        /// <param name="dataSize"></param>
        /// <returns>час роботи алгоритму в ms</returns>
        private long MeasureRun(int dataSize)
        {
            int[] data = generator.Generate(dataSize);
            var clonedData = new int[data.Length];
            Array.Copy(data, clonedData, data.Length);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            algorithm.Sort(clonedData, maxNumberOfThreads);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        /// <summary>
        /// Функція вимірює середній час виконання алгоритму в ms
        /// </summary>
        /// <param name="dataSize"></param>
        /// <param name="numberOfIterations"></param>
        /// <returns>середній час виконання алгоритму</returns>
        public int MeasureAverage(int dataSize, int numberOfIterations = 6)
        {
            MeasureRun(dataSize);
            long totalTime = 0;
            for (int i = 0; i < numberOfIterations; i++)
            {
                long time = MeasureRun(dataSize);
                totalTime += time;
                Console.WriteLine($"{time}ms");
            }
            return (int)totalTime/numberOfIterations;
        }
        /// <summary>
        /// Змінює кількість потоків для виконання алгоритму
        /// </summary>
        /// <param name="numberOfThreads"></param>
        public void ChangeNumberOfThreads(int numberOfThreads)
        {
            maxNumberOfThreads = numberOfThreads;
        }
    }
}
