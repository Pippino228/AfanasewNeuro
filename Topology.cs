using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afanasew
{
    public class Topology
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public List<int> HiddenLayers { get; }
        public double LerningRate { get; }

        public Topology(int inputCount, int outputCount, double lerningRate, params int[] layers)
        {
            if (inputCount < 0 && outputCount < 0)
            throw new ArgumentException(nameof(inputCount));
            InputCount = inputCount;
            OutputCount = outputCount;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
            LerningRate = lerningRate;
        }
    }
}
