namespace SyncAsyncComparison.Interfaces
{
    /// <summary>
    /// Контракт для алгоритму сортування масиву int[].
    /// </summary>
    public interface ISortAlgorithm
    {
        /// <summary>
        /// Сортує масив data “in-place” за зростанням.
        /// </summary>
        /// <param name="data">Масив int[], який потрібно відсортувати.</param>
        void Sort(int[] data, int numberOfThreads);
    }
}
