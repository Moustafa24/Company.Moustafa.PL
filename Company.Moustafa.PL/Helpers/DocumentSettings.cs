namespace Company.Moustafa.PL.Helpers
{
    public class DocumentSettings
    {
        // 1.Upload 
        // ImageName 
        public static string UploadFile(IFormFile file , string folderName)
        {
            // 1.Get Folder Location 
            //string folderPath= "D:\\route\\ASP.Net Core MVC\\session 5\\Assignment\\Company.Moustafa.PL\\wwwroot\\file\\" +folderName;

            //var folderPath= Directory.GetCurrentDirectory() + "\\wwwroot\\file\\" + folderName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\file", folderName);

            // 2. File Name And Make I Uniqe

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // file Path 

            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath , FileMode.Create);
           
            file.CopyTo(fileStream);
            return fileName;

        }


        // 2.Delete

        public static void DeleteFile (string fileName , string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\file", folderName , fileName);


            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        internal static void UploadFile(object image, string v)
        {
            throw new NotImplementedException();
        }
    }
}
