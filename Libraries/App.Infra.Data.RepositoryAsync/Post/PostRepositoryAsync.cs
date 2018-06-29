using System.Linq;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.RepositoryAsync.Post
{
    public class PostRepositoryAsync : RepositoryBaseAsync<Domain.Entities.Data.Post>, IPostRepositoryAsync
	{
		public PostRepositoryAsync(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		protected override IOrderedQueryable<Domain.Entities.Data.Post> GetDefaultOrder(IQueryable<Domain.Entities.Data.Post> query)
		{
			return 
				from p in query
				orderby p.Id
				select p;
		}
	}
}