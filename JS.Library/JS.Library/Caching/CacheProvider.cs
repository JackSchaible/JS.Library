using System;
using System.Runtime.Caching;

namespace JS.Library.Caching
{
    /// <summary>
    /// Provides a default method for storing information in a cache.
    /// <seealso cref="System.Runtime.Caching"/>
    /// </summary>
    public class CacheProvider : ICacheProvider
    {
        #region Properties
        private ObjectCache ObjectCache { get; set; }
        private CacheItemPolicy Policy { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        /// The default constructor.
        /// </summary>
        public CacheProvider()
        {
            ObjectCache = MemoryCache.Default;
            Policy = new CacheItemPolicy();
            Policy.SlidingExpiration = new TimeSpan(0, 30, 0);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the item in the cache using the specified ID and the default cache policy (a 30-minute sliding expiration).
        /// </summary>
        /// <typeparam name="T">The type of the item to set.</typeparam>
        /// <param name="item">The item to set.</param>
        /// <param name="id">A unique identifier for the item.</param>
        public void Set<T>(T item, string id)
        {
            ObjectCache.Add(id, item, Policy);
        }

        /// <summary>
        /// Retrieves an item from the cache.
        /// </summary>
        /// <typeparam name="T">The type of the item to retrieve.</typeparam>
        /// <param name="id">The unique identifier for the item.</param>
        /// <returns>The item from the cache if found, or the default value for the <paramref name="T"/> parameter if the cache returns null.</returns>
        public T Get<T>(string id)
        {
            var item = ObjectCache.Get(id);

            if (item == null)
                return default(T);
            else
                return (T)item;
        }
        #endregion
    }
}
