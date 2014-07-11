using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Glibs.Util
{
    //图片处理的类
    public static class ImageDo
    {

        //按比例缩放图片，生成目标格式的图片
        public static void Resize(Image image, ImageFormat imageFormat, string tagFilePath, int longSide)
        {
            if (image.Width <= longSide && image.Height <= longSide)
            {
                //小于目标尺寸的图片直接保存，不予处理。
                image.Save(tagFilePath, imageFormat);
            }
            else
            {
                //图片原尺寸
                int iw = image.Width;
                int ih = image.Height;

                //计算图片目标尺寸，保持宽高比例。
                int tw = 0;
                int th = 0;

                if (iw > ih)
                {
                    tw = longSide;
                    th = ih * tw / iw;
                }
                else
                {
                    if (iw == ih)
                    {
                        tw = th = longSide;
                    }
                    else
                    {
                        th = longSide;
                        tw = iw * th / ih;
                    }
                }

                Resize(image, imageFormat, tagFilePath, tw, th);
            }
        }

        //按照要求尺寸缩放图片，生成目标格式的图片
        public static void Resize(Image image, ImageFormat imageFormat, string tagFilePath, int width, int height)
        {
            if (image.Width <= width && image.Height <= height)
            {
                //小于目标尺寸的图片直接保存，不予处理。
                image.Save(tagFilePath, imageFormat);
            }
            else
            {
                //图片原尺寸
                int iw = image.Width;
                int ih = image.Height;

                //图片目标尺寸。
                int tw = width;
                int th = height;

                try
                {
                    Image tagImage = new Bitmap(tw, th);
                    Graphics g = Graphics.FromImage(tagImage);

                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    g.DrawImage(image, new Rectangle(0, 0, tw, th), new Rectangle(0, 0, iw, ih), GraphicsUnit.Pixel);

                    g.Dispose();

                    tagImage.Save(tagFilePath, imageFormat);
                }
                catch
                {
                }
            }
        }
    }
}
