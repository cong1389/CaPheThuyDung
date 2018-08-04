using System;
using System.Drawing;
using System.IO;
using System.Web;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;

namespace App.Service.Media
{
    public class ImageService : IImageService
    {
        public void CropAndResizeImage(HttpPostedFileBase imageFile, string outPutFilePath, string outPuthFileName, int width, int height, bool pngFormat = false)
        {
            try
            {
                var image = Image.FromStream(imageFile.InputStream);
                var kalikoImage = new KalikoImage(image);
                var imgCrop = kalikoImage.Scale(new FitScaling(width, height));

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath)));
                }
                var path = HttpContext.Current.Server.MapPath(string.Concat("~/", Path.Combine(outPutFilePath, outPuthFileName)));
                if (!pngFormat)
                {
                    imgCrop.SaveJpg(path, 99, true);
                }
                else
                {
                    imgCrop.SavePng(path);
                }

                kalikoImage.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ChangeExtension(HttpPostedFileBase imageFile, string outPutFilePath, string outPuthFileName,  bool pngFormat = false)
        {
            try
            {
                var image = Image.FromStream(imageFile.InputStream);
                var kalikoImage = new KalikoImage(image);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath)));
                }
                var path = HttpContext.Current.Server.MapPath(string.Concat("~/", Path.Combine(outPutFilePath, outPuthFileName)));
                if (!pngFormat)
                {
                    kalikoImage.SaveJpg(path, 99, true);
                }
                else
                {
                    kalikoImage.SavePng(path);
                }

                kalikoImage.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
