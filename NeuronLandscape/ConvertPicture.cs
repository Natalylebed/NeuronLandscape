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
        public List<double> ConvertInPixel(string path)
        {
            var result = new List<double>();

            var image = new Bitmap(path);

            Height = image.Width;
            Width= image.Height;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    var value = Brightness(pixel);
                    result.Add(value);

                }
            }
            return result;
        }
        //формула по которой темно-серое становиться черным, а светлое белым.
        private double Brightness(Color pixel)
        {
            var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
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
