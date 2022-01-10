using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronLandscape
{
  public  class Topology
    {
        //Входящие нейроны
        public int InputCont { get; }
        //Исходящие нейроны
        public int OutputCount { get; }

        //коэфициент learningRated
        public double LearningRated { get; }

        //скрытые-промежуточные слои c количеством нейронов

        public List<int> HiddenLayersCounts { get; }

        //для первого теста TestsWithoutLeaning
        //public Topology(int inputCont, int outputcount,  params int[] layers)
        //{
        //    InputCont = inputCont;
        //    OutputCount = outputcount;
        //    HiddenLayersCounts = new List<int>();
        //    HiddenLayersCounts.AddRange(layers);
        //}

        public Topology(int inputCont, int outputcount,  double learningRated, params int[] layers)
        {
            InputCont = inputCont;
            OutputCount = outputcount;
            LearningRated = learningRated;
            HiddenLayersCounts = new List<int>();
            HiddenLayersCounts.AddRange(layers);
        }
    }
}
