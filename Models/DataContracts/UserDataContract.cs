using System.ComponentModel.DataAnnotations;
using Career.Models.Entities;
using Career.Providers;

namespace Career.Models.DataContracts
{
	public class UserDataContract
	{
		[Required]
		public string username { get; set; }


		[Required]
		[EmailAddress]
		public string email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(password))]
		public string password_confirm { get; set; }

		public static implicit operator User(UserDataContract user)
		{

			return new User {
				username = user.username,
				password = CryptoProvider.HashPassword(user.password),
				email = user.email
			};
		}
	}
}

