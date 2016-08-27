using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc.Async;
using System.Web.Mvc.Filters;
using System.Web.Profile;
using System.Web.Routing;

namespace WebApplication.BaseClassWebApplication
{
    public static class TODO
    {
        public static bool UploadImage(HttpPostedFileBase file, string address)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);

                if (System.IO.File.Exists(Path.Combine(address, fileName)))
                    fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) +
                         Path.GetExtension(fileName);
                file.SaveAs(Path.Combine(address, fileName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string CheckExistFile(string address, string fileName)
        {
            if (File.Exists(Path.Combine(address, fileName)))
                fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + Path.GetExtension(fileName);
            return fileName;
        }

        public static string UploadImage(Image image, Size imageSize, string address, string fileName)
        {
            try
            {
                using (Image newImg = new Bitmap(image, imageSize.Width, imageSize.Height))
                {
                    newImg.Save(Path.Combine(address, fileName));
                }

                return fileName;
            }
            catch
            {
                return null;
            }
        }



        public static void DeleteImage(string address)
        {
            if (File.Exists(address))
                File.Delete(address);
        }
    }
}