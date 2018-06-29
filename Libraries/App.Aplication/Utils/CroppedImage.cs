using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace App.Aplication
{
    public static class CroppedImage
    {
        public static string SaveCroppedImage(HttpPostedFileBase imageFile, string filePath, string fileName, int? width = null, int? height = null)
        {
            Image image = Image.FromStream(imageFile.InputStream);
            Guid guid = ImageFormat.Jpeg.Guid;
            string lower = Path.GetExtension(imageFile.FileName)?.ToLower();
            if (lower != null && (lower.Equals(".jpeg") || lower.Equals(".jpg")))
            {
                guid = ImageFormat.Jpeg.Guid;
            }
            if (lower != null && lower.Equals(".png"))
            {
                guid = ImageFormat.Png.Guid;
            }
            if (lower != null && lower.Equals(".gif"))
            {
                guid = ImageFormat.Gif.Guid;
            }
            if (lower != null && lower.Equals(".bmp"))
            {
                guid = ImageFormat.Bmp.Guid;
            }
            if (lower != null && lower.Equals(".tiff"))
            {
                guid = ImageFormat.Tiff.Guid;
            }
            ImageCodecInfo imageCodecInfo = ImageCodecInfo.GetImageEncoders().First(codecInfo => codecInfo.FormatID == guid);
            
            string result;
            try
            {
                int num = 0;
                int num1 = 0;
                var heightOrigin = height.Value;
                var widthOrigin = width.Value;

                double heightOfWidth = heightOrigin / (double)widthOrigin;
                double widthOfHeight = widthOrigin / (double)heightOrigin;

                if (image.Width <= image.Height)
                {
                    heightOrigin = (int)Math.Round(image.Width * heightOfWidth);
                    if (heightOrigin >= image.Height)
                    {
                        widthOrigin = (int)Math.Round(image.Width * (image.Height / (double)heightOrigin));
                        heightOrigin = image.Height;
                        num = (image.Width - widthOrigin) / 2;
                    }
                    else
                    {
                        widthOrigin = image.Width;
                        num1 = (image.Height - heightOrigin) / 2;
                    }
                }
                else
                {
                    widthOrigin = (int)Math.Round(image.Height * widthOfHeight);
                    if (widthOrigin >= image.Width)
                    {
                        heightOrigin = (int)Math.Round(image.Height * (image.Width / (double)widthOrigin));
                        widthOrigin = image.Width;
                        num1 = (image.Height - heightOrigin) / 2;
                    }
                    else
                    {
                        heightOrigin = image.Height;
                        num = (image.Width - widthOrigin) / 2;
                    }
                }

                Bitmap bitmap = new Bitmap(widthOrigin, heightOrigin);
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphic.CompositingQuality = CompositingQuality.HighQuality;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(num, num1, widthOrigin, heightOrigin), GraphicsUnit.Pixel);
                }
                Image image1 = image;
                image1 = bitmap;
                using (EncoderParameters encoderParameter = new EncoderParameters(1))
                {
                    encoderParameter.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);
                    string empty = string.Empty;
                    empty = (!string.IsNullOrEmpty(fileName) ? $"{filePath}{string.Concat(fileName, lower)}" : $"{filePath}{string.Concat(Utils.GetTime(), lower)}"
                        );
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", filePath))))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", filePath)));
                    }
                    image1.Save(HttpContext.Current.Server.MapPath(string.Concat("~/", empty)), imageCodecInfo, encoderParameter);
                    if (bitmap != null)
                    {
                        bitmap.Dispose();
                    }
                    result = empty;
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(string.Concat("Error First: ", ex.Message));
            }

            return result;
        }
    }
}