using System.Diagnostics;

namespace SyncAsyncComparison
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();
            for (int i = 1; i <= 6; i++)
            {
                var randomData = DataGenerator.GenerateIntArray(100_000_000);
                sw.Restart();
                MergeSortParallel.Sort(randomData,12);
                sw.Stop();
                long elapsedMilliseconds = sw.ElapsedMilliseconds;
                Console.WriteLine($"Час виконання ({i} запуск): {elapsedMilliseconds} ms");
            }
        }
    }
}
