using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronLandscape
{
    public class NeuronNetwork
    {
        public Topology Topology { get; }

        public List<Layer> layers { get; }

        public NeuronNetwork(Topology topology)
        {
            Topology = topology;
            layers = new List<Layer>();
            CreateInputLayer();
            CreateHiddenLayer();
            CreateOutputLayer();
        }
        //посылаем сигналы на следующие слои
        public Neuron FeedForward(List<double> inputSignal)
        {
            SendSighnaltoInputNeurons(inputSignal);
            FeedFoewardAllLayersAfterInput();
           //если на выходе один нейрон
            if(Topology.OutputCount == 1)
            {
                return layers.Last().Neurons[0];
            }
            //если на выходе несколько нейронов
            else
            {
                return layers.Last().Neurons.OrderByDescending(n => n.Output).First();
            }

        }
        //метод выполняющий прогон всех вошедших с первого слоя сигналов через все слои с нейронами
        public void FeedFoewardAllLayersAfterInput()
        {
            for (int i = 1; i < layers.Count; i++)
            {
                var layer = layers[i];

                var previousLayerSighnal = layers[i - 1].GetSignal();

                foreach (var neuron in layer.Neurons)
                {
                    neuron.FeedForward(previousLayerSighnal);
                }
            }
        }

        //посылаем сигналы на первый слой
        public void SendSighnaltoInputNeurons(List<double> inputSignal)
        {
            for(int i = 0; i < inputSignal.Count; i++)
            {
                //создаем колекцию из одного элемента
                var signal = new List<double>() { inputSignal[i] };
               
                var neuron = layers[0].Neurons[i];
               
                neuron.FeedForward(signal);
            }
        }
        private void CreateHiddenLayer()
        {
           
            for (int i = 0; i < Topology.HiddenLayersCounts.Count; i++)
            {
                
                var hiddenNeurons = new List<Neuron>();
               
                var lastLayer = layers.Last();

                for (int j = 0; j < Topology.HiddenLayersCounts[i]; j++)
            {
                    
           //Normal по умолчанию
                var neuron = new Neuron(lastLayer.Count);
                 
                    hiddenNeurons.Add(neuron);
            }

                var hiddentlayer = new Layer(hiddenNeurons);
              
                layers.Add(hiddentlayer);
            }
     
        }

        private void CreateOutputLayer()
        {
            var outputneurons = new List<Neuron>();

            var lastLayer = layers.Last();

            for (int i = 0; i < Topology.OutputCount; i++)
            {
                var neuron = new Neuron(lastLayer.Count, NeuronType.Outpur);

                outputneurons.Add(neuron);
            }
            var outputlayer = new Layer(outputneurons, NeuronType.Outpur);

            layers.Add(outputlayer);
        }

        private void CreateInputLayer()
        {
            var inputneurons = new List<Neuron>();

            for (int i = 0; i < Topology.InputCont; i++)
            {
                var neuron = new Neuron(1, NeuronType.Input);

                inputneurons.Add(neuron);
            }
            var inputlayer = new Layer(inputneurons, NeuronType.Input);

            layers.Add(inputlayer);
        }
    }
}
