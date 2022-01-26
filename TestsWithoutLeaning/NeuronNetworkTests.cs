using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuronLandscape;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronLandscape.Tests
{   /// <summary>
/// с обучением тест неправильно называется
/// </summary>
    [TestClass()]
    public class NeuronNetworkTests
    {
        [TestMethod()]
        //тест с обучением
        public void FeedForwardTest()
        {
            var output=new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            var input = new double[,]
            {
                // Результат-то что должно быть на выходе, заранее продумали - Пациент болен - 1
                //                                                             Пациент Здоров - 0
                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                                                         //T  A  S  F
               { 0, 0, 0, 0 },
               { 0, 0, 0, 1 },
               { 0, 0, 1, 0 },
               { 0, 0, 1, 1 },
               { 0, 1, 0, 0 },
               { 0, 1, 0, 1 },
               { 0, 1, 1, 0 },
               { 0, 1, 1, 1 },
               { 1, 0, 0, 0 },
               { 1, 0, 0, 1 },
               { 1, 0, 1, 0 },
               { 1, 0, 1, 1 },
               { 1, 1, 0, 0 },
               { 1, 1, 0, 1 },
               { 1, 1, 1, 0 },
               { 1, 1, 1, 1 }
            };
           
            
            var topology = new Topology(4, 1, 0.1, 2);
            var neuronNetwork = new NeuronNetwork(topology);
           
            //обучили сеть методом обратной ошибки

            var difference = neuronNetwork.Lean(output, input, 1000000);

            var result = new List<double>();
            //сдемали прогон по сети с новами коэфициентами после обучения
            for(int i = 0; i< output.Length; i++)
            {
                var row = neuronNetwork.GetRow(input,i);
                var res = neuronNetwork.FeedForward(row).Output;
                result.Add(res);
            }
           
            //сравниваем то что должно получиться -заранее псчитали и наш результат
            for(int i = 0; i <result.Count; i++)
            {
                var expected =Math.Round(output[i],3);
                var actual = Math.Round(result[i], 3);
                Assert.AreEqual(expected, actual);
            }
           
            //Assert.Fail();
        }
        [TestMethod()]
        //тест с обучением и маштобированием доделать
        public void Database()
        {
            var outputs = new List<double>();
            var inputs = new List<double[]>();
            using (var sr = new StreamReader("heart.csv"))
            {
                var header = sr.ReadLine();
                //парсим heart.csv
                while (!sr.EndOfStream)
                { 
                var row = sr.ReadLine();
                var value = row.Split(',').Select(v => Convert.ToDouble(v.Replace(".",","))).ToList();
                var output = value.Last();
                var input = value.Take(value.Count - 1).ToArray();

                    outputs.Add(output);
                    inputs.Add(input);//зубчатый массив
                }
                
            }
            //обучили сеть методом обратной ошибки
         
            var inputsignal = new double[inputs.Count, inputs[0].Length];

            for (int i = 0; i < inputsignal.GetLength(0); i++)
            {
                for (int j = 0; j < inputsignal.GetLength(1); j++)
                { 
                inputsignal[i,j] = inputs[i][j];
                }
            }

            var topology = new Topology(outputs.Count, 1, 1, outputs.Count / 2);
            var neuronNetwork = new NeuronNetwork(topology);

            var difference = neuronNetwork.Lean(outputs.ToArray(), inputsignal , 10);

            //сдемали прогон по сети с новами коэфициентами после обучения
            var result = new List<double>();
            for (int i = 0; i < outputs.Count; i++)
            {
                var res = neuronNetwork.FeedForward(inputs[i]).Output;
                result.Add(res);
            }

            //сравниваем то что должно получиться -заранее псчитали и наш результат
            for (int i = 0; i < result.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(result[i], 2);
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod()]
        //тест с c изображениями малярии
        public void RecognazedImage()
        {
            var pathParasitized = @"E:\NeuronDataset\Parasitized\";
            //var pathParasitized = @"Parasitized\";
            var pathUnparasitized = @"E:\NeuronDataset\ninfected\";

            var convert = new ConvertPicture();
            
        var testimagparasit = @"E:\Neuron Landscape\TestsWithoutLeaning\image\Parasitized.png";
        var converttestimageparasit = convert.ConvertInPixel(testimagparasit);

            var testimagunparasit = @"E:\Neuron Landscape\TestsWithoutLeaning\image\Unparasitized.png";
            var converttestimageUnparasit = convert.ConvertInPixel(testimagunparasit);

            var topology = new Topology(converttestimageparasit.Count, 1, 0.1, converttestimageparasit.Count / 2);
            var newnetwork = new NeuronNetwork(topology);


            double[,] inputsignalparasit = GetDataParasit(pathParasitized, convert, converttestimageparasit,100);
            newnetwork.Lean(new double[] { 1.0 }, inputsignalparasit,1);

            double[,] inputsignalUnParasit = GetDataUnParasit(pathUnparasitized, convert, converttestimageparasit,100);
            newnetwork.Lean(new double[] { 0.0 }, inputsignalUnParasit, 1);

            var actualparasit = newnetwork.FeedForward(converttestimageparasit.ToArray()).Output;
            Assert.AreEqual(1, Math.Round(actualparasit,2));

            var actualunparasit = newnetwork.FeedForward(converttestimageUnparasit.ToArray()).Output;
            Assert.AreEqual(0, Math.Round(actualunparasit,2));
        }
      
        private static double[,] GetDataParasit(string pathParasitized, ConvertPicture convert, List<double> converttestimage, int size)
        {
            var parasitizenimage = Directory.GetFiles(pathParasitized);
            
            var inputsignal = new double[size, converttestimage.Count];
            for (int i = 0; i < size; i++)
            {
                var convertinblack = convert.ConvertInPixel(parasitizenimage[i]);
                for (int j = 0; j < convertinblack.Count; j++)
                {
                    inputsignal[i, j] = convertinblack[j];
                }

            }

            return inputsignal;
        }

        private static double[,] GetDataUnParasit(string pathUnparasitized, ConvertPicture convert, List<double> converttestimage,int size)
        {
            var unParasitizenimage = Directory.GetFiles(pathUnparasitized);
           
            var inputsignal = new double[ size, converttestimage.Count];
            for (int i = 0; i < size; i++)
            {
                var convertinblack = convert.ConvertInPixel(unParasitizenimage[i]);
                for (int j = 0; j < convertinblack.Count; j++)
                {
                    inputsignal[i, j] = convertinblack[j];
                }

            }

            return inputsignal;
        }
    }
}

