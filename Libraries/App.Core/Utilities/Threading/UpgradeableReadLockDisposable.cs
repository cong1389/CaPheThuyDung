using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Core.Utilities.Threading
{
    public sealed class UpgradeableReadLockDisposable : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;

        public UpgradeableReadLockDisposable(ReaderWriterLockSlim rwLock)
        {
            this._rwLock = rwLock;
        }

        void IDisposable.Dispose()
        {
            this._rwLock.ExitUpgradeableReadLock();
        }

    }
}
