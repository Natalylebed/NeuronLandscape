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
    public class NeuronNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var database=new List<Tuple<double,double[]>>
            {
                // Результат - Пациент болен - 1
                //             Пациент Здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                                                         //T  A  S  F
               new Tuple<double,double[]>(0, new double[]{ 0, 0, 0, 0 }),
               new Tuple<double,double[]>(0, new double[]{ 0, 0, 0, 1 }),
               new Tuple<double,double[]>(1, new double[]{ 0, 0, 1, 0 }),
               new Tuple<double,double[]>(0, new double[]{ 0, 0, 1, 1 }),
               new Tuple<double,double[]>(0, new double[]{ 0, 1, 0, 0 }),
               new Tuple<double,double[]>(0, new double[]{ 0, 1, 0, 1 }),
               new Tuple<double,double[]>(1, new double[]{ 0, 1, 1, 0 }),
               new Tuple<double,double[]>(0, new double[]{ 0, 1, 1, 1 }),
               new Tuple<double,double[]>(1, new double[]{ 1, 0, 0, 0 }),
               new Tuple<double,double[]>(1, new double[]{ 1, 0, 0, 1 }),
               new Tuple<double,double[]>(1, new double[]{ 1, 0, 1, 0 }),
               new Tuple<double,double[]>(1, new double[]{ 1, 0, 1, 1 }),
               new Tuple<double,double[]>(1, new double[]{ 1, 1, 0, 0 }),
               new Tuple<double,double[]>(0, new double[]{ 1, 1, 0, 1 }),
               new Tuple<double,double[]>(1, new double[]{ 1, 1, 1, 0 }),
               new Tuple<double,double[]>(1, new double[]{ 1, 1, 1, 1 })
            };
           
            
            var topology = new Topology(4, 1, 0,1, 2);
            var neuronNetwork = new NeuronNetwork(topology);

            var difference = neuronNetwork.Lean(database,100000);

            var result = new List<double>();

            foreach(var data in database)
            {
               var res= neuronNetwork.FeedForward(data.Item2).Output;
                result.Add(res);
            }

            for(int i = 0; i < result.Count; i++)
            {
                var expected = Math.Round(database[i].Item1, 3);
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
