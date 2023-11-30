using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afanasew
{
    public class Neuron
    {
        public List<double> Weight { get; }
        public NeuronType NeuronType { get; }
        public double Output { get; private set; }
        public List<double> Inputs { get; }
        public double Delta { get; private set; }
        public Neuron(int InputCount, NeuronType Type = NeuronType.Normal)
        {
            NeuronType = Type;
            Weight = new List<double>();
            Inputs = new List<double>();
            InitWrightsRandomValue(InputCount);

        }
        private void InitWrightsRandomValue(int inputCount)
        {
            Random rnd = new Random();
            for (int i = 0; i < inputCount; i++)
            {
                if (NeuronType == NeuronType.Input)
                    Weight.Add(1);
                else
                    Weight.Add(rnd.NextDouble());
                Inputs.Add(1);
            }
        }
        public double FeedForward(List<double> Input)
        {
            var sum = 0.0;
            for (int i = 0; i < Input.Count; i++)
            {
                sum += Input[i] * Weight[i];
            }
            if (NeuronType == NeuronType.Input)
                Output = sum;
            else
                Output = Sigmoid(sum);

            return Output;

        }
        public double Sigmoid(double x) => 1 / (1 + Math.Pow(Math.E, -x));
        public double SigmoidDerivative(double x) => (x) / (1 - Sigmoid(x));
        public override string ToString()
        {
            return Output.ToString();
        }
        public void SetWeights(params double[] weights)
        {
            if (weights.Length != Weight.Count)
                throw new ArgumentException("Количество весов не равно количеству входных значений.");
            for (int i = 0; i < weights.Length; i++)
            {
                Weight[i] = weights[i];
            }
        }
        public void Learn(double Error, double LerningrRate)
        {
            if (NeuronType == NeuronType.Input)
                return;
            Delta = Error * SigmoidDerivative(Output);
            for (int i = 0; i < Weight.Count; i++)
            {
                Weight[i] = Weight[i] - Inputs[i] * Delta * LerningrRate;
            }
        }
    }
}

