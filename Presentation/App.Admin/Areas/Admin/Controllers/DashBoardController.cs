using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Aplication.Extensions;
using App.Domain.Entities.Data;
using App.Service.News;
using App.Service.Post;

namespace App.Admin.Controllers
{
    public class DashBoardController : BaseAdminController
    {
        private readonly IPostService _postService;

        private readonly INewsService _newsService;

        public DashBoardController(IPostService postService, INewsService newsService)
        {
            _postService = postService;
            _newsService = newsService;
        }

        public async Task<JsonResult> GetNewRealtime()
        {
            Expression<Func<News, bool>> status = x => x.Status >= 0;

            var top = _newsService.GetTop(50, status, x => x.CreatedDate);

            var news = await Task.FromResult(
                from x in top
                orderby x.CreatedDate descending
                select x);

            var jsonResult = Json(new {list = this.RenderRazorViewToString("_DashBoardNews", news)},
                JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        public async Task<JsonResult> GetPostRealtime()
        {
            Expression<Func<Post, bool>> status = x => x.Status >= 0;

            var top = _postService.GetTop(50, status, x => x.CreatedDate);

            var posts = await Task.FromResult(
                from x in top
                orderby x.CreatedDate descending
                select x);
            
            var jsonResult = Json(new {success = true, list = this.RenderRazorViewToString("_DashBoardPost", posts)},
                JsonRequestBehavior.AllowGet);

            return jsonResult;
        }
    }
}