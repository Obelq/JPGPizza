namespace JPGPizza.MVC.Utility
{
    using System;
    using System.IO;
    using System.Web;

    public static class ImageHelper
    {
        public static string GetImageUrl(byte[] imageAsByteArray)
        {
            return "data:image/jpeg;base64," + Convert.ToBase64String(imageAsByteArray);
        }

        public static byte[] GetBytesFromFile(HttpPostedFileBase image)
        {
            string strpath = Path.GetExtension(image.FileName);
            if (strpath != ".jpg" && strpath != ".jpeg" && strpath != ".gif" && strpath != ".png")
            {
                return null;
            }

            MemoryStream memoryStream = new MemoryStream();
            image.InputStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}