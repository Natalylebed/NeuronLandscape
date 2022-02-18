using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronLandscape
{
    public class ConvertPicture
    {
        public int level { get; set; } = 128;

        public int Height { get; set; }

        public int Width { get; set; }

        //public int Groupsize { get; set; } = 10;

        public List<double> ConvertInPixel(string path)
        {
            var result = new List<double>();

            var image = new Bitmap(path);

            var resizeImage = new Bitmap(image, new Size(30, 30));

            Height = resizeImage.Width;
            Width= resizeImage.Height;
            
            for (int x = 0; x < resizeImage.Width; x++)
            {              
                for (int y = 0; y < resizeImage.Height; y++)
                {
                   
                    var pixelR = resizeImage.GetPixel(x, y).R;
                    var pixelG = resizeImage.GetPixel(x, y).G;
                    var pixelB = resizeImage.GetPixel(x, y).B;
                    var value = Brightness(pixelR,pixelG,pixelB);
                    result.Add(value);
                }
            }
            return result;
        }
       
        private double Brightness(byte R,byte G,byte B)
        {
            var result = 0.299 *R + 0.587 * G + 0.114 * B;
            return result < level ? 0 : 1;
        }
        public void Save(string path,List<double> pixel)
        {
           
            var image = new Bitmap(Width, Height);

             for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var color = pixel[y * Width + x] == 1 ? Color.White : Color.Black;
                    image.SetPixel(x, y, color);

                }
            }
            image.Save(path);
        }
    }
}
