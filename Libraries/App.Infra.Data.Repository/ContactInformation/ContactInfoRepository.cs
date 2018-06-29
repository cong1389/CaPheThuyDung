using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.ContactInformation
{
    public class ContactInfoRepository : RepositoryBase<Domain.Entities.GlobalSetting.ContactInformation>, IContactInfoRepository
    {
        public ContactInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Domain.Entities.GlobalSetting.ContactInformation GetById(int id)
        {
            var contactInformation =
                FindBy(x
                => x.Id == id).FirstOrDefault();

            return contactInformation;
        }

        protected override IOrderedQueryable<Domain.Entities.GlobalSetting.ContactInformation> GetDefaultOrder(
            IQueryable<Domain.Entities.GlobalSetting.ContactInformation> query)
        {
            var contactInformations =
                from p in query
                orderby p.Id
                select p;

            return contactInformations;
        }

        public IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> PagedList(Paging page)
        {
            return GetAllPagedList(page).ToList();
        }

        public IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
        {
            var expression = PredicateBuilder.True<Domain.Entities.GlobalSetting.ContactInformation>();
            if (!string.IsNullOrEmpty(sortBuider.Keywords))
            {
                expression = expression.And(x =>
                    x.Title.ToLower().Contains(sortBuider.Keywords.ToLower()) ||
                    x.Email.ToLower().Contains(sortBuider.Keywords.ToLower()) ||
                    x.Address.ToLower().Contains(sortBuider.Keywords.ToLower()) ||
                    x.MobilePhone.ToLower().Contains(sortBuider.Keywords.ToLower()) ||
                    x.Hotline.ToLower().Contains(sortBuider.Keywords.ToLower()) ||
                    x.Fax.ToLower().Contains(sortBuider.Keywords.ToLower()));
            }
            return FindAndSort(expression, sortBuider.Sorts, page);
        }
    }
}