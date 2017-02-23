using System;
using System.Security.Cryptography;

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



	}
}
