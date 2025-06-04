using SyncAsyncComparison.Interfaces;

namespace SyncAsyncComparison.Algorithms
{
    public class MergeSortParallel : ISortAlgorithm
    {
        /// <summary>
        /// Максимальна глибина рекурсії для створення нових задач (Task).
        /// </summary>
        private int _maxDepth;

        /// <summary>
        /// Головний метод: сортує масив паралельно, використовуючи до maxDegreeOfParallelism потоків.
        /// </summary>
        /// <param name="array">Масив, який треба відсортувати.</param>
        /// <param name="maxDegreeOfParallelism">Максимальна кількість потоків, що можуть працювати одночасно.</param>
        public void Sort(int[] array, int maxDegreeOfParallelism = 1)
        {
            if (array == null || array.Length < 2)
                return;

            _maxDepth = (int)Math.Log(maxDegreeOfParallelism, 2) + 1;

            int[] aux = new int[array.Length];
            SortInternal(array, aux, 0, array.Length - 1, 0, maxDegreeOfParallelism);
        }

        /// <summary>
        /// Внутрішній рекурсивний метод. Якщо depth < _maxDepth — розбиваємо на паралельні завдання,
        /// інакше — викликаємо послідовну сортировку.
        /// </summary>
        private void SortInternal(int[] a, int[] aux, int left, int right, int depth, int maxThreads)
        {
            if (left >= right)
                return;

            if (depth >= _maxDepth)
            {
                SeqSort(a, aux, left, right);
                return;
            }

            int mid = left + (right - left) / 2;
            var po = new ParallelOptions { MaxDegreeOfParallelism = maxThreads };

            Parallel.Invoke(
                po,
                () => SortInternal(a, aux, left, mid, depth + 1, maxThreads),
                () => SortInternal(a, aux, mid + 1, right, depth + 1, maxThreads)
            );

            Merge(a, aux, left, mid, right);
        }

        /// <summary>
        /// Послідовна версія MergeSort для сегмента [left..right].
        /// </summary>
        private void SeqSort(int[] a, int[] aux, int left, int right)
        {
            if (left >= right)
                return;

            int mid = left + (right - left) / 2;
            SeqSort(a, aux, left, mid);
            SeqSort(a, aux, mid + 1, right);
            Merge(a, aux, left, mid, right);
        }

        /// <summary>
        /// Злиття двох відсортованих підмасивів [left..mid] та [mid+1..right] за допомогою aux.
        /// </summary>
        private void Merge(int[] a, int[] aux, int left, int mid, int right)
        {
            for (int i = left; i <= right; i++)
            {
                aux[i] = a[i];
            }

            int iLeft = left;
            int iRight = mid + 1;
            int current = left;

            while (iLeft <= mid && iRight <= right)
            {
                if (aux[iLeft] <= aux[iRight])
                    a[current++] = aux[iLeft++];
                else
                    a[current++] = aux[iRight++];
            }
            while (iLeft <= mid)
            {
                a[current++] = aux[iLeft++];
            }

        }
    }
}
