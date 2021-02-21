using System;
using Career.Models.Entities;

namespace Career.Models.ViewModels
{
	public class UserViewModel
	{
		public int id { get; set; }

		public string username { get; set; }

		public string email { get; set; }

		public static implicit operator UserViewModel(User user)
		{

			return new UserViewModel {
				id = user.id,
				username = user.username,
				email = user.email
			};
		}
	}
}