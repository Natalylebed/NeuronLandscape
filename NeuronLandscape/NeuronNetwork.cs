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
        //посылаем сигналы на первый  слой, потом по остальным слоям и считаем сигнал на выходе
        public Neuron FeedForward(params double[] inputSignal)
        {
            SendSighnaltoInputNeurons(inputSignal);
            FeedFoewardAllLayersAfterInput();
            //если на выходе один нейрон
            if (Topology.OutputCount == 1)
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
        public void SendSighnaltoInputNeurons(params double[] inputSignal)
        {
            for (int i = 0; i < inputSignal.Length; i++)
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
                    var neuron = new Neuron(lastLayer.NeuronCount);

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
                var neuron = new Neuron(lastLayer.NeuronCount, NeuronType.Outpur);

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
        //epoch -это эпоха ...один прогон по сети-одна эпоха
        public double Lean(List<Tuple<double, double[]>> dataset, int epoch)
        {
            var error = 0.0;

            for (int i = 0; i < epoch; i++)
            {
                foreach (var data in dataset)
                {
                    error += Backpropagation(data.Item1, data.Item2);

                }

            }

            var result = error / epoch;
            return result;
        }

        //Метод для прохождения по нейронам в обратном порядкес выхода на вход(справо налево)(метод обратного распространения ошибки)-входящие нейроны и ожидаемое значение
        private double Backpropagation(double expected, params double[] input)
        {

            var actual = FeedForward(input).Output;

            var differrens = actual - expected;

            //для последнего выходного output слоя -исправляем коэфициенты
            foreach (var neoron in layers.Last().Neurons)
            {
                neoron.Lean(differrens, Topology.LearningRated);
            }
            //-2 потомучто с 0 нумерация это 1, -1 еще на последний выходной слой который не учитываем

            //этот цикл для перебора слоев  с право налево
            for (int i = layers.Count - 2; i >= 0; i--)
            {
                var layer = layers[i];
                var previouslayer = layers[i + 1];

                //цикл для  сигналов на расматриваемом слое(если смотреть справо налево)              
                for (int j = 0; j < layer.NeuronCount; j++)
                {
                    var neuron = layer.Neurons[j];
                    //это цикл для исправления веса с предыдущего слоя 
                    //(если смотреть справо налево поэтому previouslayer = layers[i + 1])
                    //-кол-во входящих с предыдущего слоя сигнилов равно кол-ву сигналов на предыдущем слое
                    for (int k = 0; k < previouslayer.NeuronCount; k++)
                    {
                        var previousneuron = previouslayer.Neurons[k];

                        //это формула №4 для подсчета скаректированных весов в hidden слоях;_weights[j] -вес вошедщий в j нейрон с предыдущего нейрона 
                        var error = previousneuron._weights[j] * previousneuron.Delta;

                        //исправляем коэфициенты -обучаем текущий нейрон j
                        neuron.Lean(error, Topology.LearningRated);

                    }


                }
            }
            var result = differrens * differrens;

            return result;

        }

    }
}
