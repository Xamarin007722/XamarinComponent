using System;
namespace UtilityClasses.Utility
{
    public class BaseDisposable : IDisposable
    {
        private bool disposed = false;
        public string Welcome(string name)
        {
            return name;
        }

        ~BaseDisposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

