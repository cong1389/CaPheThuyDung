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

        public void CropAndResizeImage(string inPutFilePath, string outPutFilePath, string outPuthFileName, int width, int height, bool pngFormat = false)
        {
            try
            {
                var image = Image.FromFile(HttpContext.Current.Server.MapPath(string.Concat("~/", inPutFilePath)));
                //if (!width.HasValue)
                //{
                //	width = new int?(image.Width);
                //}
                //if (!height.HasValue)
                //{
                //	height = new int?(image.Height);
                //}
                var kalikoImage = new KalikoImage(image);
                var kalikoImage1 = kalikoImage.Scale(new PadScaling(width, height, Color.Transparent));
                //KalikoImage kalikoImage1 = kalikoImage.Scale(new FitScaling(width.Value, height.Value));
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath)));
                }
                var str = HttpContext.Current.Server.MapPath(string.Concat("~/", Path.Combine(outPutFilePath, outPuthFileName)));
                if (!pngFormat)
                {
                    kalikoImage1.SaveJpg(str, 99);
                }
                else
                {
                    kalikoImage1.SavePng(str);
                }
                kalikoImage1.Dispose();
                kalikoImage.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void CropAndResizeImage(Image image, string outPutFilePath, string outPuthFileName, int width, int height, bool pngFormat = false)
        {
            try
            {
                //if (!width.HasValue)
                //{
                //	width = new int?(image.Width);
                //}
                //if (!height.HasValue)
                //{
                //	height = new int?(image.Height);
                //}
                var kalikoImage = new KalikoImage(image);

                kalikoImage.Resize(width, height);
                //KalikoImage kalikoImage1 = kalikoImage.Scale(new FitScaling(width, height));
                //KalikoImage kalikoImage1 = kalikoImage.Scale(new FitScaling(width.Value, height.Value));
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", outPutFilePath)));
                }
                var str = HttpContext.Current.Server.MapPath(string.Concat("~/", Path.Combine(outPutFilePath, outPuthFileName)));
                if (!pngFormat)
                {
                    kalikoImage.SaveJpg(str, 99);
                }
                else
                {
                    kalikoImage.SavePng(str);
                }
                kalikoImage.Dispose();
                kalikoImage.Dispose();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
