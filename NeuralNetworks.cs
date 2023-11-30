using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afanasew
{
    public class NeuralNetwork
    {
        public Topology Topology { get; private set; }
        public List<Layer> Layers { get; private set; }
        public NeuralNetwork(Topology topology)
        {
            Topology = topology;
            Layers = new List<Layer>();
            CreateInputLayer();
            CreateHiddentLayers();
            CreateOutputLayer();
        }
        public void CreateInputLayer()
        {

            var inputNeurons = new List<Neuron>();
            for (int i = 0; i < Topology.InputCount; i++)
            {
                inputNeurons.Add(new Neuron(1, NeuronType.Input));
            }
            Layers.Add(new Layer(inputNeurons, NeuronType.Input));
        }
        public void CreateOutputLayer()
        {
            var inputNeurons = new List<Neuron>();
            var LastLayer = Layers.Last();
            for (int i = 0; i < Topology.OutputCount; i++)
            {
                inputNeurons.Add(new Neuron(LastLayer.Count, NeuronType.Output));
            }
            Layers.Add(new Layer(inputNeurons, NeuronType.Output));
        }
        public void CreateHiddentLayers()
        {
            for (int i = 0; i < Topology.HiddenLayers.Count; i++)
            {
                var HiddenNeurons = new List<Neuron>();
                var LastLayer = Layers.Last();
                for (int j = 0; j < Topology.HiddenLayers[i]; j++)
                {
                    HiddenNeurons.Add(new Neuron(LastLayer.Count));
                }
                Layers.Add(new Layer(HiddenNeurons, NeuronType.Normal));
            }
        }
        public Neuron FeedForward(List<double> inputSignals)
        {
            if (Topology.InputCount != inputSignals.Count)
                throw new ArgumentException(nameof(inputSignals));
            SendSignalsToInputNeurons(inputSignals);
            FeedForwardAllLayersAfterInput();
            if (Topology.OutputCount == 1)
                return Layers.Last().Neurons[0];
            else
                return Layers.Last().Neurons.OrderByDescending(o => o.Output).First();
        }
        private void FeedForwardAllLayersAfterInput()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                var previouslayerignals = Layers[i - 1].GetSignals();
                foreach (var neuron in Layers[i].Neurons)
                {
                    neuron.FeedForward(previouslayerignals);
                }
            }
        }
        private void SendSignalsToInputNeurons(List<double> inputSignals)
        {
            for (int i = 0; i < inputSignals.Count; i++)
            {
                Layers[0].Neurons[i].FeedForward(new List<double> { inputSignals[i] });
            }
        }
        public double BackPropagation(double expectedResult, params double[] inputs)
        {
            List<double> inputsList = new List<double>();
            inputsList.AddRange(inputs);
            var actual = FeedForward(inputsList).Output;
            foreach (var neurons in Layers.Last().Neurons)
            {
                neurons.Learn(actual - expectedResult, Topology.LerningRate);
            }
            for (int i = Layers.Count - 2; i > 0; i--)
            {
                var currentLayer = Layers[i];
                var perviousLayer = Layers[i + 1];
                for (int j = 0; j < currentLayer.Count; j++)
                {
                    var neuron = currentLayer.Neurons[i];
                    for (int k = 0; k < perviousLayer.Count; k++)
                    {
                        var perviousNeuron = perviousLayer.Neurons[k];
                        var error = perviousNeuron.Weight[i] * perviousNeuron.Delta;
                        neuron.Learn(error, Topology.LerningRate);
                    }
                }
            }

            return Math.Pow(actual - expectedResult, 2);
        }
        public double Learn(List<Tuple<double, double[]>> Dataset, int Epoch)
        {
            var error = 0.0;
            for (int i = 0; i < Epoch; i++)
            {
                foreach (var data in Dataset)
                {
                    error += BackPropagation(data.Item1, data.Item2);
                }
            }
            return error / Epoch;
        }
    }
}
