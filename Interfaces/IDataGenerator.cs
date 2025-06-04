namespace SyncAsyncComparison.Interfaces
{
    /// <summary>
    /// Контракт для будь-якого генератора колекцій даних.
    /// T — тип елементів (наприклад, int, double, тощо).
    /// </summary>
    /// <typeparam name="T">Тип елементів, які генерує масив</typeparam>
    public interface IDataGenerator
    {
        /// <summary>
        /// Генерує масив розміру size
        /// </summary>
        /// <param name="size">Кількість елементів у масиві</param>
        /// <returns>Колекція T довжини size</returns>
        int[] Generate(int size);
    }
}
