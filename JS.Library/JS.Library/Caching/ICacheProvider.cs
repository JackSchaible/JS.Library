namespace JS.Library.Caching
{
    /// <summary>
    /// An interface for storing and retrieving short term information from a caching mechanism.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Sets an item in a caching mechanism.
        /// </summary>
        /// <typeparam name="T">The type of the item to set.</typeparam>
        /// <param name="item">The item to set.</param>
        /// <param name="id">A unique identifier for the item.</param>
        void Set<T>(T item, string id);

        /// <summary>
        /// Retrieves an item from the cache.
        /// </summary>
        /// <typeparam name="T">The type of the item to retrieve.</typeparam>
        /// <param name="id">The unique identifier for the item.</param>
        /// <returns>The item from the cache if found, or the default value for the <paramref name="T"/> parameter if the cache returns null.</returns>
        T Get<T>(string id);
    }
}
