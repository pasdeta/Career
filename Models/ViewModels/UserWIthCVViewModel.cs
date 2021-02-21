using System;
using Career.Models.Entities;

namespace Career.Models.ViewModels
{
	public class UserWithCVViewModel : UserViewModel
	{
		public CVViewModel cv { get; set; }

		public static implicit operator UserWithCVViewModel(User user)
		{

			return new UserWithCVViewModel {
				id = user.id,
				username = user.username,
				email = user.email,
				cv = user.cv
			};
		}
	}
}