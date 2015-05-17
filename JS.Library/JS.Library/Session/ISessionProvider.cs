namespace JS.Library.Session
{
    /// <summary>
    /// An interface for retrieving and storing values in the application's session state.
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// Sets an item in the application's session state.
        /// </summary>
        /// <typeparam name="T">The type of the item to set.</typeparam>
        /// <param name="id">A unique identifier to use when storing the item.</param>
        /// <param name="value">The item to set.</param>
        void Set<T>(string id, T value);

        /// <summary>
        /// Retrieves an item from the application's session state.
        /// </summary>
        /// <typeparam name="T">The type of the item to retrieve.</typeparam>
        /// <param name="id">A unique identifier to use when retrieving the item.</param>
        /// <returns>The item stored in the application's session state, or the default value for the <typeparamref name="T"/> type.</returns>
        T Get<T>(string id);
    }
}
