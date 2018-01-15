using System;
namespace UtilityClasses.Utility
{
    /// <summary>
    /// BaseDisposable class Implements IDisposable INterface
    /// </summary>
    public class BaseDisposable : IDisposable
    {
        private bool disposed = false;
        public string Welcome(string name)
        {
            return name;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:UtilityClasses.Utility.BaseDisposable"/> is reclaimed by garbage collection.
        /// </summary>
        ~BaseDisposable()
        {
            Dispose(false);
        }
        /// <summary>
        /// Releases all resource used by the <see cref="T:UtilityClasses.Utility.BaseDisposable"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="T:UtilityClasses.Utility.BaseDisposable"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="T:UtilityClasses.Utility.BaseDisposable"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:UtilityClasses.Utility.BaseDisposable"/> so the garbage collector can reclaim the memory that
        /// the <see cref="T:UtilityClasses.Utility.BaseDisposable"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <returns>The dispose.</returns>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // free managed resources
                }
                // free native/unmanaged resources if there are any.
                disposed = true;
            }
        }

    }
}

