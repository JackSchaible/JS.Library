using System;
using System.Collections.Generic;
using System.Web;
namespace JS.Library.DataAccess
{
    /// <summary>
    /// The base access class for any object accessing the database. Provides default caching functions for get operations.
    /// </summary>
    /// <typeparam name="T">The type of the object being handled from the database.</typeparam>
    public abstract class AccessClass<T>
    {
        #region Properties
        /// <summary>
        /// Whether or not to use the ApplicationLogic's caching mechanism.
        /// </summary>
        protected bool UseCache { get; set; }
        /// <summary>
        /// The name to use when retrieving "List" operation values from the cache.
        /// </summary>
        protected string ListCacheName
        {
            get { return typeof (T).Name + "ListCache"; }
        }
        /// <summary>
        /// Retrieves the cache name for an object's "Get" operation result.
        /// </summary>
        /// <param name="queryIdentifier">Whatever parameter was used in the "Get" operation.</param>
        /// <returns>A string representing the cache name for the "Get" operation.</returns>
        protected string GetCacheName(string queryIdentifier)
        {
            return typeof (T).Name + String.Format("GetCustom{0}Cache", queryIdentifier);
        }
        #endregion

        #region ctor
        /// <summary>
        /// Initializes the access class.
        /// </summary>
        /// <param name="useCache">Whether or not to use caching for the get operations used by this class.</param>
        protected AccessClass(bool useCache)
        {
            UseCache = useCache;
        }
        #endregion

        #region Methods
        #region Get from Cache
        /// <summary>
        /// Retrieves a list of values from the cache.
        /// </summary>
        /// <returns>A List of <typeparamref name="T"/> values.</returns>
        public List<T> ListFromCache()
        {
            return ApplicationLogic.Current.CacheProvider.Get<List<T>>(ListCacheName);
        }
        /// <summary>
        /// Retrieves a list of values from the cache, using the specified identifier.
        /// </summary>
        /// <param name="queryIdentifier">The identifier to use when retrieving cache results.</param>
        /// <returns>A List of <typeparamref name="T"/> values.</returns>
        public List<T> ListFromCache(string queryIdentifier)
        {
            return ApplicationLogic.Current.CacheProvider.Get<List<T>>(ListCacheName + queryIdentifier);
        }
        /// <summary>
        /// Retrieves a single value from the cache, using the specified identifier.
        /// </summary>
        /// <param name="queryIdentifier">The identifier to use when retrieving cache results.</param>
        /// <returns>A single item, of type <typeparamref name="T"/>.</returns>
        public T GetFromCache(string queryIdentifier)
        {
            return ApplicationLogic.Current.CacheProvider.Get<T>(GetCacheName(queryIdentifier));
        }
        #endregion

        #region Add to Cache
        /// <summary>
        /// Adds a list of items to the cache, using the specified name.
        /// </summary>
        /// <param name="items">A List of items to add to the cache.</param>
        /// <param name="name">The name to use when adding the list to the cache.</param>
        public void AddListToCache(List<T> items, string name)
        {
            ApplicationLogic.Current.CacheProvider.Set(items, ListCacheName + name);
        }
        /// <summary>
        /// Adds a list of items to the cache, using the specified name, and an additional query identifier.
        /// </summary>
        /// <param name="items">A List of items to add to the cache.</param>
        /// <param name="name">The name to use when adding the list to the cache.</param>
        /// <param name="queryIdentifier">An additional identifier to use.</param>
        public void AddListToCache(List<T> items, string name, string queryIdentifier)
        {
            ApplicationLogic.Current.CacheProvider.Set(items, name + ListCacheName + queryIdentifier);
        }
        /// <summary>
        /// Adds a single item to the cache, using the specified name, and an additional query identifier.
        /// </summary>
        /// <param name="items">A List of items to add to the cache.</param>
        /// <param name="name">The name to use when adding the list to the cache.</param>
        /// <param name="queryIdentifier">An additional identifier to use.</param>
        public void AddGetToCache(T item, string name, string queryIdentifier)
        {
            ApplicationLogic.Current.CacheProvider.Set(item, GetCacheName(queryIdentifier + name));
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Gets a list of objects from the database.
        /// </summary>
        /// <returns>A List of objects.</returns>
        public abstract List<T> List();
        /// <summary>
        /// Gets a single object from the database, using the identifier provided.
        /// </summary>
        /// <param name="id">The unique identifier to use.</param>
        /// <returns>An object representing a row from the database.</returns>
        public abstract T Get(int id);
        /// <summary>
        /// Inserts an item into the database.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        /// <param name="exception">Any exception that is thrown during the database operation.</param>
        /// <returns>A bool value indiciated whether the insert was successful or not.</returns>
        public abstract bool Insert(T item, out Exception exception);
        /// <summary>
        /// Updates an item in the database.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <param name="exception">Any exception that is thrown during the database operation.</param>
        /// <returns>A bool value indiciated whether the update was successful or not.</returns>
        public abstract bool Update(T item, out Exception exception);
        /// <summary>
        /// Deletes a item from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        /// <param name="exception">Any exception that is thrown during the database operation.</param>
        /// <returns>A bool value indiciated whether the deletion was successful or not.</returns>
        public abstract bool Delete(int id, out Exception exception);
        #endregion
        #endregion
    }
}
