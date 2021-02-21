using System.ComponentModel.DataAnnotations;
using Career.Models.Entities;

namespace Career.Models.DataContracts
{
	public class CompanyDataContract
	{
		[Required]
		public string name { get; set; }

		[Required]
		public string address { get; set; }

		public static implicit operator Company(CompanyDataContract company)
		{

			return new Company {
				name = company.name,
				address = company.address
			};
		}
	}
}

