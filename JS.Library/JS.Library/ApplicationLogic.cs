using JS.Library.Caching;
using JS.Library.DataAccess;
using JS.Library.Session;
using JS.Library.Settings;
using System;

namespace JS.Library
{
    /// <summary>
    /// Provides a single encapsulating class for all your application logic. Requires a public static Instance variable on the derived class.
    /// </summary>
    public class ApplicationLogic
    {
        public static ApplicationLogic Current
        {
            get
            {
                if (_instance == null)
                    _instance = new ApplicationLogic();

                return _instance;
            }
        }
        private static ApplicationLogic _instance;

        #region Ctor
        /// <summary>
        /// The default constructor. The <code>Initialize</code> method must be called before this class can be used.
        /// </summary>
        public ApplicationLogic()
        {
            IsInitialized = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether or not the Application Logic class has been initialized
        /// </summary>
        public bool IsInitialized { get; set; }

        /// <summary>
        /// Used for storing information in an application's Session State.
        /// </summary>
        public ISessionProvider SessionProvider
        {
            get
            {
                if (IsInitialized)
                    return _sessionProvider;
                else
                    throw new InvalidOperationException("Application Logic needs to be initialized before accessing.");
            }
        }
        private ISessionProvider _sessionProvider;

        /// <summary>
        /// Used for storing short-term information using the System Cache.
        /// </summary>
        public ICacheProvider CacheProvider
        {
            get
            {
                if (!IsInitialized)
                    return _cacheProvider;
                else
                    throw new InvalidOperationException("Application Logic needs to be initialized before accessing.");
            }
        }
        private ICacheProvider _cacheProvider;

        /// <summary>
        /// Used for accessing data stored in an external physical data store.
        /// </summary>
        public IDatabaseProvider DatabaseProvider
        {
            get
            {
                if (!IsInitialized)
                    return _databaseProvider;
                else
                    throw new InvalidOperationException("Application Logic needs to be initialized before accessing.");
            }
        }
        private IDatabaseProvider _databaseProvider;

        /// <summary>
        /// Used to store user, domain, and application-specific settings.
        /// </summary>
        public ISettingsProvider SettingsProvider
        {
            get
            {
                if (!IsInitialized)
                    return _settingsProvider;
                else
                    throw new InvalidOperationException("Application Logic needs to be initialized before accessing.");
            }
        }
        private ISettingsProvider _settingsProvider;
        #endregion

        #region Methods
        /// <summary>
        /// The default initialize method. Initializes the Application Logic using the configured providers.
        /// </summary>
        /// <param name="sessionProvider">Any class that implements <code>ISessionProvider</code>. Leave as <code>null</code> to use the default provider.</param>
        /// <param name="cacheProvider">Any class that implements <code>ICacheProvider</code>. Leave as <code>null</code> to use the default provider.</param>
        /// <param name="databaseProvider">Any class that implements <code>IDatabaseProvider</code>.</param>
        /// <param name="settingsProvider">Any class that implements <code>ISettingsProvider</code>.</param>
        public virtual void Initialize(ISessionProvider sessionProvider, ICacheProvider cacheProvider, IDatabaseProvider databaseProvider, ISettingsProvider settingsProvider)
        {
            if (sessionProvider == null)
                _sessionProvider = new SessionProvider();
            else
                _sessionProvider = sessionProvider;

            if (cacheProvider == null)
                _cacheProvider = new CacheProvider();
            else
                _cacheProvider = cacheProvider;

            _databaseProvider = databaseProvider;
            _settingsProvider = settingsProvider;

            IsInitialized = true;
        }
        #endregion
    }
}
