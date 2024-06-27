using System.Text;

namespace ECommerceMVC.Helpers
{
	public class MyUtil
	{
		public static string UploadHinh(IFormFile Hinh, string folder)
		{
			try
			{
				//Dinh nghia duong dan toi hinh
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Hinh.FileName);
				using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
				{
					Hinh.CopyTo(myfile);
				}
				return Hinh.FileName;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}
		//Map password
		public static string GenerateRandomKey(int length = 5)
		{
			//Khai bao patter, du lieu lay pattern tu dau
			var pattern = @"qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM!";
			var sb = new StringBuilder(); //Lay chuoi
										  //Dinh nghia ham random
			var rd = new Random();
			for (int i = 0; i < length; i++)
			{
				//Moi lan thi chuoi = chuoi + ky tu o vi tri tu 0 toi max
				//De lay 1 ky tu nao do trong chuoi pattern
				sb.Append(pattern[rd.Next(0, pattern.Length)]);
			}
			return sb.ToString();
		}
	}
}
