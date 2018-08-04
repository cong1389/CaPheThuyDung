using System.Drawing;
using System.Web;

namespace App.Service.Media
{
    public interface IImageService
    {
        void CropAndResizeImage(HttpPostedFileBase imageFile, string outPutFilePath, string outPuthFileName, int width, int height, bool pngFormat = false);

        void ChangeExtension(HttpPostedFileBase imageFile, string outPutFilePath, string outPuthFileName, bool pngFormat = false);
    }
}