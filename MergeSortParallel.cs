namespace SyncAsyncComparison
{
    public static class MergeSortParallel
    {
        /// <summary>
        /// Максимальна глибина рекурсії для створення нових задач (Task).
        /// </summary>
        private static int _maxDepth;

        /// <summary>
        /// Головний метод: сортує масив паралельно, використовуючи до maxDegreeOfParallelism потоків.
        /// </summary>
        /// <param name="array">Масив, який треба відсортувати.</param>
        /// <param name="maxDegreeOfParallelism">Максимальна кількість потоків, що можуть працювати одночасно.</param>
        public static void Sort(int[] array, int maxDegreeOfParallelism)
        {
            if (array == null || array.Length < 2)
                return;

            // Визначаємо глибину, до якої створюємо паралельні завдання.
            // Наприклад, log2(maxDegreeOfParallelism) + 1.
            _maxDepth = (int)Math.Log(maxDegreeOfParallelism, 2) + 1;

            // Створюємо допоміжний масив для злиття
            int[] aux = new int[array.Length];
            SortInternal(array, aux, 0, array.Length - 1, 0, maxDegreeOfParallelism);
        }

        /// <summary>
        /// Внутрішній рекурсивний метод. Якщо depth < _maxDepth — розбиваємо на паралельні завдання,
        /// інакше — викликаємо послідовну сортировку.
        /// </summary>
        private static void SortInternal(int[] a, int[] aux, int left, int right, int depth, int maxThreads)
        {
            if (left >= right)
                return;

            // Якщо досягли максимальної глибини паралелізації — виконуємо послідовний MergeSort
            if (depth >= _maxDepth)
            {
                SeqSort(a, aux, left, right);
                return;
            }

            int mid = left + (right - left) / 2;
            var po = new ParallelOptions { MaxDegreeOfParallelism = maxThreads };

            // Паралельно сортуємо ліву та праву половини
            Parallel.Invoke(
                po,
                () => SortInternal(a, aux, left, mid, depth + 1, maxThreads),
                () => SortInternal(a, aux, mid + 1, right, depth + 1, maxThreads)
            );

            // Після повернення — зливаємо дві відсортовані половини
            Merge(a, aux, left, mid, right);
        }

        /// <summary>
        /// Послідовна версія MergeSort для сегмента [left..right].
        /// </summary>
        private static void SeqSort(int[] a, int[] aux, int left, int right)
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
        private static void Merge(int[] a, int[] aux, int left, int mid, int right)
        {
            // Копіюємо у допоміжний масив
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

            // Додаємо залишки з лівої половини
            while (iLeft <= mid)
            {
                a[current++] = aux[iLeft++];
            }

            // Якщо у правій половині залишились елементи, їх додавати не потрібно,
            // бо вони вже скопійовані у a під час попередніх ітерацій
        }
    }
}
