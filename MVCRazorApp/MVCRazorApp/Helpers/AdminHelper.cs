using System;
using System.Security.Cryptography;
using System.Text;

namespace MVCRazorApp
{
	public static class AdminHelper
	{
		public static byte[] GenerateSalt()
		{
			int max_len = 64;
			byte[] salt = new byte[max_len];

			using (var random = new RNGCryptoServiceProvider())
			{
				random.GetNonZeroBytes(salt);
			}

			return salt;
		}

		public static string HashPassword(string password, byte[] salt)
		{
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);
			                    
			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			string hashedPassword = Convert.ToBase64String(hashBytes);

			return hashedPassword;
		}

	}

}
