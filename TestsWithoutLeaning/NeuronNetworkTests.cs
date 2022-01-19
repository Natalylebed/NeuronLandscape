using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuronLandscape;
using System;
using System.Collections.Generic;
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
    }
}

//var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
//var inputs = new double[,]
//{
//                // Результат - Пациент болен - 1
//                //             Пациент Здоров - 0

//                // Неправильная температура T
//                // Хороший возраст A
//                // Курит S
//                // Правильно питается F
//                //T  A  S  F
//                { 0, 0, 0, 0 },
//                { 0, 0, 0, 1 },
//                { 0, 0, 1, 0 },
//                { 0, 0, 1, 1 },
//                { 0, 1, 0, 0 },
//                { 0, 1, 0, 1 },
//                { 0, 1, 1, 0 },
//                { 0, 1, 1, 1 },
//                { 1, 0, 0, 0 },
//                { 1, 0, 0, 1 },
//                { 1, 0, 1, 0 },
//                { 1, 0, 1, 1 },
//                { 1, 1, 0, 0 },
//                { 1, 1, 0, 1 },
//                { 1, 1, 1, 0 },
//                { 1, 1, 1, 1 }
//};
