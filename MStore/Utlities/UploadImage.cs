namespace MStore.Utlities
{
    public static class UploadImage
    {
        public static string SaveImage(IFormFile file,string? oldFileName = null, string folderPath = "products")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid image file");

            var fileName = Path.GetFileName(file.FileName);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/"+folderPath, fileName);

            //delete previous photo
            if (!string.IsNullOrEmpty(oldFileName))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images/"+folderPath,oldFileName);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            //save a new photo
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }
    }
}
