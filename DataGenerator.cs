namespace SyncAsyncComparison
{
    public static class DataGenerator
    {
        private static readonly Random _random = new Random();
        public static int[] GenerateIntArray(int size, int minValue = 0, int maxValue = 1_000_000)
        {
            if (size < 0) throw new ArgumentOutOfRangeException("розмір масиву повинен бути додатнім");
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = _random.Next(minValue, maxValue);
            }
            return result;
        }
    }
}
