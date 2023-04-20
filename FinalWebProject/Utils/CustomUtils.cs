using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace FinalWebProject.Utils
{
	public static class CustomUtils
	{
		public static async Task<Uri> UploadFile(IFormFile file)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			Account account = new Account("dhpxifsfm", "357415218746838", "faZtm3aJ8BAxoJ-ZG6psQCbqD7E");
			Cloudinary cloudinary = new Cloudinary(account);
			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(filePath),
			};
			ImageUploadResult res = cloudinary.Upload(uploadParams);
			System.IO.File.Delete(filePath);
			return res.Url;
		}
	}
}
