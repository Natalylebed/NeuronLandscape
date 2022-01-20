using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuronLandscape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronLandscape.Tests
{
    [TestClass()]
    public class ConvertPictureTests
    {
        
        [TestMethod()]
        public void SaveTest()
        {
            var convertet = new ConvertPicture();
            var ress = convertet.ConvertInPixel(@"E:\Neuron Landscape\TestsWithoutLeaning\image\Parasitized.png");
           
            convertet.Save("e:\\ress.png", ress);
        }
    }
}