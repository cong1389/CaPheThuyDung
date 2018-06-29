using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.Data.UOW.Interfaces
{
	public interface IUnitOfWorkAsync
	{
		Task<int> CommitAsync();

		Task<int> CommitAsync(CancellationToken cancellationToken);
	}
}