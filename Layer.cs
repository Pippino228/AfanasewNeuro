using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afanasew
{
    public class Layer
    {
        public List<Neuron> Neurons { get; private set; }
        public int Count => Neurons?.Count ?? 0;

        public Layer(List<Neuron> neurons, NeuronType Type)
        {
            foreach (var neuron in neurons)
            {
                if (neuron.NeuronType != Type)
                    throw new ArgumentException(nameof(neuron));
            }
            Neurons = neurons;
        }
        public List<double> GetSignals()
        {
            List<double> signals = new List<double>();
            foreach (var neuron in Neurons)
            {
                signals.Add(neuron.Output);
            }
            return signals;
        }
    }
}
