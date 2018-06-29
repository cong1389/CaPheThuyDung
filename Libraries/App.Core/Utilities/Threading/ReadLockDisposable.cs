using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Core.Utilities.Threading
{
    public sealed class ReadLockDisposable : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;

        public ReadLockDisposable(ReaderWriterLockSlim rwLock)
        {
            this._rwLock = rwLock;
        }

        void IDisposable.Dispose()
        {
            this._rwLock.ExitReadLock();
        }
    }
}
