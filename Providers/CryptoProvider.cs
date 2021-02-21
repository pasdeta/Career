using BCrypt.Net;

namespace Career.Providers
{
	public sealed class CryptoProvider
	{
		public static string HashPassword(string password)
		{
			
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public static bool Verify(string password, string hashedPassword)
		{
			
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}
	}
}