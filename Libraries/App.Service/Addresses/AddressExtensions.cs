using System.Text;
using App.Core.Extensions;
using App.Domain.Common;

namespace App.Service.Addresses
{
    public static class AddressExtensions
    {
		public static string GetFullName(this Address address)
        {
            if (address == null)
            {
                return null;
            }

            var sb = new StringBuilder(address.FirstName);

            sb.AppendFormat("{0} ", address.LastName);

            if (address.Company.HasValue())
            {
                sb.AppendFormat("({0}) ", address.Company);
            }
            return sb.ToString().Trim();
        }

	    public static bool IsPostalDataEqual(this Address address, Address other)
	    {
		    if (address != null && other != null)
		    {
			    if (address.FirstName.IsCaseInsensitiveEqual(other.FirstName) &&
			        address.LastName.IsCaseInsensitiveEqual(other.LastName) &&
			        address.Company.IsCaseInsensitiveEqual(other.Company) &&
			        address.Address1.IsCaseInsensitiveEqual(other.Address1) &&
			        address.Address2.IsCaseInsensitiveEqual(other.Address2) &&
			        address.ZipPostalCode.IsCaseInsensitiveEqual(other.ZipPostalCode) &&
			        address.City.IsCaseInsensitiveEqual(other.City) &&
			        address.StateProvinceId == other.StateProvinceId &&
			        address.CountryId == other.CountryId)
			    {
				    return true;
			    }
		    }

		    return false;
	    }
	}
}
