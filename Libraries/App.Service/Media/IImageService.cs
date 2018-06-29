using System.Drawing;
using System.Web;

namespace App.Service.Media
{
    public interface IImageService
    {
        void CropAndResizeImage(HttpPostedFileBase imageFile, string outPutFilePath, string outPuthFileName, int width, int height, bool pngFormat = false);
        void CropAndResizeImage(string inPutFilePath, string outPutFilePath, string outPuthFileName, int width, int height, bool pngFormat = false);
        void CropAndResizeImage(Image image, string outPutFilePath, string outPuthFileName, int width, int height, bool pngFormat = false);
    }
}