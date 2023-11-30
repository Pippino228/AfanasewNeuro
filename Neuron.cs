using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfanasewNeuron
{
    class Neuron
    {
        Decimal weight = 0.5m;
        public decimal Smoothing = 0.00001m;
        public decimal Lasterror;
        public decimal ProcessInputData(decimal input) => input * weight;
        public decimal RestoreInputData(decimal output) => output / weight;

        public void Train(decimal input, decimal expectedResult)
        {
            decimal actualResult = ProcessInputData(input);
            Lasterror = expectedResult - actualResult;
            weight += (Lasterror / actualResult) * Smoothing;

        }
    }
}
