using System;
using Career.Models.Entities;

namespace Career.Models.ViewModels
{
	public class UserApplicationViewModel
	{
		public int id { get; set; }

		public DateTime applied_at { get; set; }

		public JobViewModel job { get; set; }

		public static explicit operator UserApplicationViewModel(Application application)
		{

			return new UserApplicationViewModel {
				id = application.id,
				applied_at = application.applied_at,
				job = application.job
			};
		}
	}
}