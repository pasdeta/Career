using System;
using Career.Models.Entities;

namespace Career.Models.ViewModels
{
	public class CompanyViewModel
	{
		public int id { get; set; }

		public string name { get; set; }

		public string address { get; set; }

		public static implicit operator CompanyViewModel(Company company)
		{

			return new CompanyViewModel {
				id = company.id,
				name = company.name,
				address = company.address
			};
		}
	}
}