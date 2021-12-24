using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronLandscape
{
    public class Layer
    {
        public List<Neuron> Neurons {get;}

        public int Count => Neurons?.Count ?? 0;

        //В одном слое должны быть нейроны только одного типа
        public Layer(List<Neuron> neurons,NeuronType type=NeuronType.Normal)
        {
            foreach(var neuron in neurons)
            {
                if(neuron._neuronType! != type)
                {
                    throw new ArgumentNullException("Нейроны разных типов в одном слое");
                }
                else
                {
                    Neurons = neurons;
                }
            }

        }

        public List<double> GetSignal()
        {
            var result = new List<double>();

            foreach (var neuron in Neurons)
            {
                result.Add(neuron.Output);
            }
            return result;
        }
































            
    }
}
