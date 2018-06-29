using System;

namespace App.Infra.Data.DbFactory
{
	public class Disposable : IDisposable
	{
		private bool _isDisposed;

		public Disposable()
		{
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!_isDisposed & disposing)
			{
				DisposeCore();
			}
			_isDisposed = true;
		}

		protected virtual void DisposeCore()
		{
		}

		~Disposable()
		{
			Dispose(false);
		}
	}
}