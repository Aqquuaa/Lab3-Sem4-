using SyncAsyncComparison.Algorithms;
using SyncAsyncComparison.Benchmark;
using SyncAsyncComparison.Interfaces;

namespace SyncAsyncComparison
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataGenerator generator = new DataGenerator();
            ISortAlgorithm algorithm = new MergeSortParallel();
            Benchmarking benchmark = new Benchmarking(generator, algorithm, 1);

            Console.WriteLine("=== Sorting Benchmark ===");
            Console.WriteLine("Введiть exit у будь-який момент, щоб вийти.");

            while (true)
            {
                int threadCount;
                while (true)
                {
                    Console.Write("Введiть кiлькiсть потокiв");
                    string line = Console.ReadLine()?.Trim();

                    if (string.Equals(line, "exit", StringComparison.OrdinalIgnoreCase))
                        return;

                    if (int.TryParse(line, out threadCount) && threadCount > 0)
                    {
                        benchmark.ChangeNumberOfThreads(threadCount);
                        break;
                    }

                    Console.WriteLine("Неправильне значення. Потрiбно ввести додатнє цiле число");
                }

                int elementCount;
                while (true)
                {
                    Console.Write("Введiть кiлькiсть елементiв для сортування");
                    string line = Console.ReadLine()?.Trim();

                    if (string.Equals(line, "exit", StringComparison.OrdinalIgnoreCase))
                        return;

                    if (int.TryParse(line, out elementCount) && elementCount >= 0)
                    {
                        break;
                    }

                    Console.WriteLine("Неправильне значення. Потрiбно ввести невiд’ємне цiле або \"exit\".");
                }

                try
                {
                    double avgTime = benchmark.MeasureAverage(elementCount);
                    Console.WriteLine($"Середнiй час сортування {elementCount} елементiв ({threadCount} потоки): {avgTime:F2} ms");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка пiд час вимiрювання: {ex.Message}");
                }

                Console.WriteLine();
            }
        }
    }
}
