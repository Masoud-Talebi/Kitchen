namespace Kitchen.api.Application.Tools
{
    public static class FileSaver
    {
        public static async Task<string> FileSavers(IFormFile files, string Adress, int Id)
        {
            // var fileExtension = Path.GetExtension(files.FileName);
            var fileName = $"{Id:0000000}_0.jpg";
            //Photo Convert to Array Byte
            byte[] pictureBinary = null;
            using (var fileStream = files.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    pictureBinary = ms.ToArray();
                }
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Adress, fileName);
            await File.WriteAllBytesAsync(filePath, pictureBinary);
            string file = "/" + Adress + "/" + fileName;
            return file;
        }

    }
}