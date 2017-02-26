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

		public static bool ComparePassword(string storedPassword, byte[] salt, string enteredPassword)
		{
			byte[] hashBytes = Convert.FromBase64String(storedPassword);
			Array.Copy(hashBytes, 0, salt, 0, 16);
			var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);

			for (int i = 0; i < 20; i++)
			{
				if (hashBytes[i + 16] != hash[i])
				{
					return false;
				}
			}

			return true;
		}

	}

}
