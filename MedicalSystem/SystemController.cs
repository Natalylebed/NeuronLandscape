using NeuronLandscape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystem
{
    public class SystemController
    {
        public NeuronNetwork DataNetwork { get; }
        public NeuronNetwork ImageNetwork { get; set; }

        public NeuronNetwork ExitData { get; set; }

        public SystemController()
        {
            var topology = new Topology(14, 1, 0.1, 7);
            DataNetwork = new NeuronNetwork(topology);

            var imageTopology = new Topology(400, 1, 0.1, 200);
            ImageNetwork = new NeuronNetwork(imageTopology);

        }
       

        //обучили сеть методом обратной ошибки

    //    var difference = neuronNetwork.Lean(output, input, 1000000);

    //    var result = new List<double>();
    //        //сдемали прогон по сети с новами коэфициентами после обучения
    //        for(int i = 0; i<output.Length; i++)
    //        {
    //            var row = neuronNetwork.GetRow(input, i);
    //    var res = neuronNetwork.FeedForward(row).Output;
    //    result.Add(res);
    //        }
           
    //        //сравниваем то что должно получиться -заранее псчитали и наш результат
    //        for(int i = 0; i<result.Count; i++)
    //        {
    //            var expected = Math.Round(output[i], 3);
    //var actual = Math.Round(result[i], 3);
    //Assert.AreEqual(expected, actual);
            }
    }

