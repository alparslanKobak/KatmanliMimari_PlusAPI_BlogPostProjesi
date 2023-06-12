namespace P013KatmanliBlog.MVCUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath= "/wwwroot/Img/")
        {
            string fileName = "";

            fileName = formFile.FileName;

            string directory = Directory.GetCurrentDirectory() + filePath + fileName;

            using var stream = new FileStream(directory, FileMode.Create);

            await formFile.CopyToAsync(stream);

            return fileName;
        }

        public static bool FileRemover(string fileName, string filePath ="/wwwroot/Img/")
        {

            string directory = Directory.GetCurrentDirectory() + filePath + fileName;

            if (File.Exists(directory)) // Dosya var mı? Kontrol
            {
                File.Delete(directory); // Dosyayı siler

                return true; // Eğer true ise işlem başarılı
            }

            return false; // işlem başarısız ise false dön

        }

        internal static Task<string?> FileLoaderAsync(string v)
        {
            throw new NotImplementedException();
        }
    }
}
