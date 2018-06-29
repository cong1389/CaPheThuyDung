using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Orders;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Orders;
using App.Infra.Data.UOW.Interfaces;
using App.Service.Common;

namespace App.Service.Orders
{
    public class ShoppingCartItemService : BaseService<ShoppingCartItem>, IShoppingCartItemService
    {
        private const string CacheShoppingcartitemKey = "db.ShoppingCartItem.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        private readonly IWorkContext _workContext;

        private readonly IOrderTotalCalculationService _orderTotalCalculationService;

        public ShoppingCartItemService(IUnitOfWork unitOfWork, IShoppingCartItemRepository shoppingCartItemRepository
            , IWorkContext workContext
             , IOrderTotalCalculationService orderTotalCalculationService
            , ICacheManager cacheManager)
            : base(unitOfWork, shoppingCartItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _workContext = workContext;
            _orderTotalCalculationService = orderTotalCalculationService;
            _cacheManager = cacheManager;
        }

        public void AddToCart(AddToCartContext ctx)
        {
            try
            {
                var customer = ctx.Customers ?? _workContext.CurrentCustomer;
                var storeId = 1;

                IEnumerable<ShoppingCartItem> ieShoppingCart = _workContext.CurrentCustomer.ShoppingCartItems;

                var objNew = new ShoppingCartItem
                {
                    PostId = ctx.Post.Id,
                    CustomerId = customer.Id,
                    CustomerEnteredPrice = ctx.Price,
                    StoreId = storeId
                };

                //Customer chua co trong ShoppingCartItem
                if (ieShoppingCart == null || !ieShoppingCart.Any())
                {
                    objNew.Quantity = ctx.Quantity <= 0 ? 1 : ctx.Quantity;
                    objNew.CustomerEnteredPrice = ctx.Price;

                    //Create cart
                    Create(objNew);
                }
                //Customer da co trong ShoppingCartItem
                else
                {
                    //Kiem tra PostId cua customer nay co trong ShoppingCartItem chua
                    var objOld = ieShoppingCart.FirstOrDefault(x => x.PostId == ctx.Post.Id);

                    // int quantityOld = ieShoppingCart.Where(x => x.PostId == ctx.Post.Id && x.CustomerId == customer.Id).FirstOrDefault().Quantity;
                    if (objOld != null)
                    {
                        //Update và cộng dồn số lượng
                        objOld.Quantity = ctx.Quantity;
                        objOld.CustomerEnteredPrice = ctx.Price;

                        //objOld.Quantity = objOld.Quantity + ctx.Quantity;
                        Update(objOld);
                    }
                    //Cutomer, postId da co trong ShoppingCartItem
                    else
                    {
                        objNew.Quantity = ctx.Quantity <= 0 ? 1 : ctx.Quantity;
                        objNew.CustomerEnteredPrice = ctx.Price;

                        //Create cart
                        Create(objNew);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public ShoppingCartItem GetById(int id, bool isCache = true)
        {
            ShoppingCartItem shoppingCartItem;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheShoppingcartitemKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                shoppingCartItem = _cacheManager.Get<ShoppingCartItem>(key);
                if (shoppingCartItem == null)
                {
                    shoppingCartItem = _shoppingCartItemRepository.GetById(id);
                    _cacheManager.Put(key, shoppingCartItem);
                }
            }
            else
            {
                shoppingCartItem = _shoppingCartItemRepository.GetById(id);
            }

            return shoppingCartItem;
        }

        public IEnumerable<ShoppingCartItem> GetByPostId(int postId, bool isCache = true)
        {
            IEnumerable<ShoppingCartItem> shoppingCartItem;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheShoppingcartitemKey, "GetByPostId");
                sbKey.Append(postId);

                var key = sbKey.ToString();
                shoppingCartItem = _cacheManager.GetCollection<ShoppingCartItem>(key);
                if (shoppingCartItem == null)
                {
                    shoppingCartItem = _shoppingCartItemRepository.FindBy(x => x.PostId == postId);
                    _cacheManager.Put(key, shoppingCartItem);
                }
            }
            else
            {
                shoppingCartItem = _shoppingCartItemRepository.FindBy(x => x.PostId == postId);
            }

            return shoppingCartItem;
        }

        public IEnumerable<ShoppingCartItem> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _shoppingCartItemRepository.PagedSearchList(sortbuBuilder, page);
        }

        public decimal GetCurrentCartSubTotal(IOrderedEnumerable<ShoppingCartItem> cart)
        {
            return _orderTotalCalculationService.GetShoppingCartSubTotal(cart);
        }

        public virtual void DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem, bool resetCheckoutData = true,
            bool ensureOnlyActiveCheckoutAttributes = false, bool deleteChildCartItems = true)
        {
            if (shoppingCartItem == null)
            {
                throw new ArgumentNullException("shoppingCartItem");
            }

            Delete(shoppingCartItem);
        }
    }
}