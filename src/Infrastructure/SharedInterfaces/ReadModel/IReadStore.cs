namespace BudgetFirst.SharedInterfaces.ReadModel
{
    using System;

    /// <summary>
    /// (Global) read store for read models.
    /// </summary>
    public interface IReadStore
    {
        /// <summary>
        /// Store an object for a specific id. Separate object types with same ID are stored and retrieved separately.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="id">Id of the object</param>
        /// <param name="obj">Object to store (overwrites existing)</param>
        void Store<T>(Guid id, T obj);

        /// <summary>
        /// Try to retrieve an object for a specific id.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="id">Object id</param>
        /// <param name="result">Is set when found</param>
        /// <returns><c>true</c> if the object could be found (and <see cref="result"/> is set).</returns>
        bool TryRetrieve<T>(Guid id, out T result);

        /// <summary>
        /// Retrieve an object for a specific id.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="id">Object id</param>
        /// <returns>Object, if the object could be found - default value otherwise.</returns>
        T Retrieve<T>(Guid id);

        /// <summary>
        /// Store an object. Separate object types are stored and retrieved separately.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="obj">Object to store (overwrites existing)</param>
        void StoreSingleton<T>(T obj);

        /// <summary>
        /// Try to retrieve an object
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="result">Is set when found</param>
        /// <returns><c>true</c> if the object could be found (and <see cref="result"/> is set).</returns>
        bool TryRetrieveSingleton<T>(out T result);

        /// <summary>
        /// Retrieve an object (or default if not found)
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <returns>Object, if the object could be found - default value otherwise.</returns>
        T RetrieveSingleton<T>();
    }
}