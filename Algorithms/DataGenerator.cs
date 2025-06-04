using SyncAsyncComparison.Interfaces;

namespace SyncAsyncComparison.Algorithms
{
    /// <summary>
    /// Генерує масив випадкових int у зазначеному діапазоні.
    /// </summary>
    public class DataGenerator : IDataGenerator
    {
        private readonly int _minValue;
        private readonly int _maxValue;
        private readonly Random _random;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="minValue">Мінімальне значення (включно).</param>
        /// <param name="maxValue">Максимальне значення (не включно).</param>
        public DataGenerator(int minValue = 0, int maxValue = 1_000_000)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _random = new Random();
        }
        /// <summary>
        /// Функція генерує псевдовипадковий масив int[]
        /// </summary>
        /// <param name="size"></param>
        /// <returns>int[] array</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int[] Generate(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Розмір масиву не може бути від’ємним.");

            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = _random.Next(_minValue, _maxValue);
            }
            return result;
        }
    }
}
