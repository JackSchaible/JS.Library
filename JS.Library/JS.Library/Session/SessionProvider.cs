using System.Web;

namespace JS.Library.Session
{
    /// <summary>
    /// A default method for storing information in the application's session state.
    /// </summary>
    public class SessionProvider : ISessionProvider
    {
        /// <summary>
        /// Sets an item in the application's session state.
        /// </summary>
        /// <typeparam name="T">The type of the item to set.</typeparam>
        /// <param name="id">A unique identifier to use when storing the item.</param>
        /// <param name="value">The item to set.</param>
        public void Set<T>(string id, T value)
        {
            if (HttpContext.Current == null)
                return;

            HttpContext.Current.Session[id] = value;
        }

        /// <summary>
        /// Retrieves an item from the application's session state.
        /// </summary>
        /// <typeparam name="T">The type of the item to retrieve.</typeparam>
        /// <param name="id">A unique identifier to use when retrieving the item.</param>
        /// <returns>The item stored in the application's session state, or the default value for the <typeparamref name="T"/> type.</returns>
        public T Get<T>(string id)
        {
            if (HttpContext.Current == null)
                return default(T);

            var result = HttpContext.Current.Session[id];

            if (result == null)
                return default(T);
            else
                return (T)result;
        }
    }
}
