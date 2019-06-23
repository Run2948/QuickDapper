using System;

namespace Quick.Common.Systems
{
    /// <summary>
    /// Disposable
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// 终结器
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            Dispose(true);
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        public abstract void Dispose(bool disposing);
    }
}