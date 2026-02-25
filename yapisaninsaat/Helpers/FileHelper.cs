namespace yapisaninsaat.Helpers
{
    public static class FileHelper
    {
        public static async Task<string?> UploadImageAsync(IFormFile? file, IWebHostEnvironment env, string folder)
        {
            if (file == null || file.Length == 0)
  return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg", ".ico" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

         if (!allowedExtensions.Contains(ext))
              return null;

var uploadsDir = Path.Combine(env.WebRootPath, "uploads", folder);
        if (!Directory.Exists(uploadsDir))
             Directory.CreateDirectory(uploadsDir);

            var fileName = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
  {
     await file.CopyToAsync(stream);
        }

    return $"/uploads/{folder}/{fileName}";
        }

        public static void DeleteImage(IWebHostEnvironment env, string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            return;

      var filePath = Path.Combine(env.WebRootPath, imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(filePath))
         File.Delete(filePath);
        }
    }
}
