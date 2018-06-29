using App.Admin.Helpers;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Orders;
using App.FakeEntity.Orders;
using App.Framework.Utilities;
using App.Service.Orders;
using AutoMapper;
using Resources;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class OrderController : BaseAdminController
	{
		private const string Cache = "db.Order";

		private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, ICacheManager cacheManager)
        {
            _orderService = orderService;

	        //Clear cache
	        cacheManager.RemoveByPattern(Cache);
		}

        public ActionResult Edit(int id)
        {
            var order = _orderService.GetById(id);

            if (order == null || order.Deleted)
            {
                return RedirectToAction("Index");
            }

            var modelMap = Mapper.Map<Order, OrderViewModel>(order);
            PrepareOrderDetailsModel(modelMap, order);

            return View(modelMap);
        }

        [NonAction]
        private void PrepareOrderDetailsModel(OrderViewModel model, Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            //Post item
            foreach (var orderItem in order.OrderItems)
            {
                var orderItemModel = new OrderViewModel.OrderItemModel
                {
                    Id = orderItem.Id,
                    PostId = orderItem.PostId,
                    PostName = orderItem.Post.Title,
                    Quantity = orderItem.Quantity,
                    UnitPriceInclTax = orderItem.UnitPriceInclTax,
                    SubTotalInclTax = orderItem.PriceInclTax
                };

                model.Items.Add(orderItemModel);
            }

        }

        [HttpPost]
        [RequiredPermisson(Roles = "ViewOrder")]
        public ActionResult Edit(OrderViewModel orderView, string returnUrl)
        {
            ActionResult action;
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", MessageUI.ErrorMessage);
                    return View(orderView);
                }

                var map = Mapper.Map<OrderViewModel, Order>(orderView);
                _orderService.Update(map);

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Order)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    action = RedirectToAction("Index");
                }
                else
                {
                    action = Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Order.Edit: ", ex.Message));

                return View(orderView);
            }
            return action;
        }

        [RequiredPermisson(Roles = "DeleteOrder")]
        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    var order =
                        from id in ids
                        select _orderService.GetById(int.Parse(id));

                    _orderService.BatchDelete(order);
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Order.Delete: ", ex.Message));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index(int page = 1, string keywords = "")
        {
            ViewBag.Keywords = keywords;
            var sortingPagingBuilder = new SortingPagingBuilder
            {
                Keywords = keywords,
                Sorts = new SortBuilder
                {
                    ColumnName = "CreatedDate",
                    ColumnOrder = SortBuilder.SortOrder.Descending
                }
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };

            var orders = _orderService.PagedList(sortingPagingBuilder, paging);

            var orderViewModels = orders.Select(Mapper.Map<Order, OrderViewModel>).ToList();

            if (orderViewModels.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord,
                    i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }

            return View(orderViewModels);
        }
    }
}