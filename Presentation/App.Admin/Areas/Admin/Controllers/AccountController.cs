using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using App.Admin.Helpers;
using App.Core.Utilities;
using App.Domain.Entities.Identity;
using App.FakeEntity.User;
using App.Framework.Utilities;
using App.Service.Account;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Resources;

namespace App.Admin.Controllers
{
    public class AccountController : BaseIdentityController
    {
        private readonly IUserService _userService;

        private readonly RoleManager<IdentityRole, Guid> _roleManager;       

        public AccountController(IUserService userService, UserManager<IdentityUser, Guid> userManager
            , RoleManager<IdentityRole, Guid> roleManager
            ) : base(userManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        [HttpGet]
        [RequiredPermisson(Roles = "CreateEditAccount")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditAccount")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterFormViewModel model, string returnUrl)
        {
            ActionResult action;
            if (!ModelState.IsValid)
            {
                action = View();
            }
            else
            {
                var identityUser = new IdentityUser
                {
                    UserName = model.UserName,
                    Address = model.Address,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Phone = model.Phone,
                    Email = model.Email,
                    City = model.City,
                    State = model.State,
                    IsLockedOut = false,
                    IsSuperAdmin = false,
                    Created = DateTime.UtcNow
                };
                //var identityUser1 = identityUser;
                var identityResult = await UserManager.CreateAsync(identityUser, model.Password);
                //var identityResult1 = identityResult;
                if (identityResult.Succeeded)
                {
                    if (!model.IsSuperAdmin)
                    {
                        var item = Request["roles"];
                        if (!string.IsNullOrEmpty(item))
                        {
                            var strArrays = item.Split(',');
                            await UserManager.AddToRolesAsync(identityUser.Id, strArrays);
                        }
                    }
                    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.Account)));
                    if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                    {
                        action = RedirectToAction("Index");
                    }
                    else
                    {
                        action = Redirect(returnUrl);
                    }
                }
                else
                {
                    AddErrors(identityResult);
                    action = View(model);
                }
            }
            return action;
        }

        [HttpGet]
        [RequiredPermisson(Roles = "CreateEditAccount")]
        public async Task<ActionResult> Edit(string id)
        {
            var guid = GetGuid(id);
            var identityUser = await UserManager.FindByIdAsync(guid);
            return View(Mapper.Map<RegisterFormViewModel>(identityUser));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RegisterFormViewModel model, string returnUrl)
        {
            ActionResult action;
            try
            {
                model.Created = null;
                var identityUser = UserManager.FindById(model.Id);
                identityUser = Mapper.Map(model, identityUser);
                var identityResult = await UserManager.UpdateAsync(identityUser);
                if (identityResult.Succeeded)
                {
                    if (model.IsSuperAdmin)
                    {
                        var roles = UserManager.GetRoles(model.Id);
                        UserManager.RemoveFromRoles(model.Id, roles.ToArray());
                    }
                    else
                    {
                        var item = Request["roles"];
                        if (!string.IsNullOrEmpty(item))
                        {
                            var lstUserRole = UserManager.GetRoles(model.Id);
                            UserManager.RemoveFromRoles(model.Id, lstUserRole.ToArray());
                            var strArrays = item.Split(',');
                            UserManager.AddToRoles(model.Id, strArrays);
                        }
                    }
                    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Account)));
                    if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                    {
                        action = RedirectToAction("Index");
                        return action;
                    }

                    action = Redirect(returnUrl);
                    return action;
                }

                AddErrors(identityResult);
                action = View(model);
                return action;
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ExtentionUtils.Log(string.Concat("Account.Update: ", exception.Message));
            }
            action = View();
            return action;
        }

        [RequiredPermisson(Roles = "DeleteAccount")]
        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    foreach (var id in ids)
                    {
                        var userId = Guid.Parse(id);
                        var objUser = (from user in ids select UserManager.FindById(userId)).FirstOrDefault();
                        
                        //Task<IList<UserLoginInfo>> loginInfo = _userLoginStore.GetLoginsAsync(objUser);
                        //_userLoginStore.RemoveLoginAsync(objUser, loginInfo);

                        var lstUserRole = UserManager.GetRoles(userId);
                        UserManager.RemoveFromRoles(userId, lstUserRole.ToArray());
                        UserManager.Update(objUser);
                        UserManager.Delete(objUser);
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Post.Delete: ", ex.Message));
            }
            return RedirectToAction("Index");
        }

        [RequiredPermisson(Roles = "ViewAccount")]
        public async Task<ActionResult> Index(int page = 1, string keywords = "")
        {
            ViewBag.Keywords = keywords;
            var sortingPagingBuilder = new SortingPagingBuilder
            {
                Keywords = keywords
            };
            var sortBuilder = new SortBuilder
            {
                ColumnName = "UserName",
                ColumnOrder = SortBuilder.SortOrder.Descending
            };
            sortingPagingBuilder.Sorts = sortBuilder;
            var sortingPagingBuilder1 = sortingPagingBuilder;
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };
            var paging1 = paging;
            var users = await _userService.PagedList(sortingPagingBuilder1, paging1);
            if (users != null && users.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging1.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }
            return View(users);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.RouteData.Values["action"].Equals("create"))
            {
                var lstRoles = _roleManager.Roles.ToList();
                ViewBag.Roles = lstRoles;
            }

            else if (filterContext.RouteData.Values["action"].Equals("edit"))
            {
                var lstRoles = _roleManager.Roles.ToList();

                var userId = filterContext.RouteData.Values["id"].ToString();
                var lstRolesByUser = UserManager.GetRoles(Guid.Parse(userId));

                foreach (var item in lstRoles)
                {
                    foreach (var role in lstRolesByUser)
                    {
                        if (item.Name == role)
                        {
                            item.Seleted = true;
                            break;
                        }
                    }
                }

                 ViewBag.Roles = lstRoles;
            }
        }
    }
}