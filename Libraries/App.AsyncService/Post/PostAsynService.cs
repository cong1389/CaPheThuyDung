using App.Infra.Data.Common;
using App.Infra.Data.RepositoryAsync.Post;
using App.Infra.Data.UOW.Interfaces;

namespace App.AsyncService.Post
{
    public class PostAsynService : BaseAsyncService<Domain.Entities.Data.Post>, IPostAsynService
	{
	    public PostAsynService(IPostRepositoryAsync postRepository, IUnitOfWorkAsync unitOfWork) : base(postRepository, unitOfWork)
		{
		}
	}
}