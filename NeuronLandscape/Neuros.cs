using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuronLandscape
{
    public class Neuron
    {
        //вес нейронов приходящих в данный нейрон
        public List<double> _weights { get; }
       
        //получившийся тип нейрона
        public NeuronType _neuronType { get; }

        //коллекция значений входящих на нейрон сигналов (значения с предыдущих нейронов)
        public List<double> _input { get; }

        //выход -текущее значение нейрона на данном слое
        public double Output { get; private set; }

        //дельта -ошбка разница между выходом получившимся и ожидаемом при моделирование с извесным выходом
        public double Delta { get; private set; }

        //создаем нейрон (кол-во связей, тип нейрона по умолчанию normal)
        public Neuron(int inputCount, NeuronType neuronType=NeuronType.Normal)
        {
            if (Enum.IsDefined(typeof(NeuronType), _neuronType))

            { _neuronType = neuronType; }

            else

            { throw new ArgumentNullException("Нет такого типа нейрона", nameof(_neuronType)); }

            _weights = new List<double>();
            _input = new List<double>();

            InputWeightRandonValue(inputCount);
        }

        //Задаем рандомные веса -будем исправлять метобом обратной ошибки

        private void InputWeightRandonValue(int inputCount)
        {
            var ran = new Random ();

            //чтобы рандом был рандомный
            Thread.Sleep(100);

            for (int i = 0; i < inputCount; i++)
            {
                if (_neuronType == NeuronType.Input)
                {
                    _weights.Add(1);
                }
                else
                { 
                   _weights.Add(ran.NextDouble());
                }

                _input.Add(0);
            }
        }

        //FeedForward -это переборга значений слева направо (если хоотично то это называется рекурсия)
        //перемножаем вес на входящий значение

        public double FeedForward(List<double> inputs)
        {
            for(int i = 0; i < inputs.Count; i++)
            {
                _input[i]=inputs[i] ;
            }

            var sum = 0.0;

            for(int i = 0; i < inputs.Count; i++)
            {
                sum +=  inputs[i] * _weights[i];
            }
           

            if (_neuronType != NeuronType.Input)
            {
                Output = Sigmoid(sum);
            }
            else
            {
                Output = sum;
            }
            return Output;
        }
        //Сигмоида это такой график используемый  для плавного перехода одного значения в другое f(x)=1\1+(e в степени -x)
        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Pow(Math.E, -x));

            return result;
        }
        //Значение для подсчета дельты
        private double Sigmoidxd(double x)
        {
            var sigm = Sigmoid(x);
            //у автора ошибка в формуле
            //var result = sigm / 1 - sigm;

            var result = sigm *( 1 - sigm);

            return result;
        }
        //public void SetWigths(params double[] weights)
        //{
        //    //TODO сделать проверку что кол-во Unput равно весам
        //    //TODO удалить этот код после написания возможности обучения сети
        //    for (int i = 0; i < weights.Length; i++)
        //    {
        //        _weights[i] = weights[i];
        //    }

            
        //}

        //Обучаем систему-присваеваем новый текущий вес нейрона  по формуле - обратное распределение ошибки. Error-разница между ткущим значением и ожидаемым. leanRate-коэфициент устанавливается опытным путем
        public void Lean(double error, double leanRate)
        {
            if (_neuronType == NeuronType.Input)
            {
                return;

            }
            
            Delta = error * Sigmoidxd(Output);

             for (int i = 0; i < _weights.Count; i++)
            {
                var weight = _weights[i];

                var Input = _input[i];

                var newWeght =weight - Input * Delta * leanRate;

                _weights[i] = newWeght;

            }
            
        }

        public override string ToString()
        {
            return Output.ToString();
        }

    }
}
