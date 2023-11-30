using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shatunov;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AfanasewTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var topology = new Topology(4, 1, 2);
            var network = new NeuralNetwork(topology);
            network.Layers[1].Neurons[0].SetWeights(0.5, -0.1, 0.3, -0.1);
            network.Layers[1].Neurons[1].SetWeights(0.1, -0.3, 0.7, -0.3);
            network.Layers[2].Neurons[0].SetWeights(1.2, 0.8);

            var result = network.FeedForward(new List<double> { 1, 0, 0, 0 });
            Assert.AreEqual(0.762590473872541, Math.Round(result.Output, 15));
        }
    }
    [TestClass]
        public class UnitTest2
        {
            [TestMethod]
            public void TestMethod2()
            {
                var dataset = new List<Tuple<double, double[]>>()
            {
                //Результат - Пациент болен - 1
                //            Пациент здоров - 0
                //            
                //Неправильная температура T
                //Хороший возраст A
                //Курит S
                //Правильно питается F
                //                                            T  A  S  F
                new Tuple<double, double[]>(0, new double[] { 0, 0, 0, 0}),
                new Tuple<double, double[]>(0, new double[] { 0, 0, 0, 1}),
                new Tuple<double, double[]>(0, new double[] { 0, 0, 1, 0}),
                new Tuple<double, double[]>(0, new double[] { 0, 0, 1, 1}),
                new Tuple<double, double[]>(0, new double[] { 0, 1, 0, 0}),
                new Tuple<double, double[]>(0, new double[] { 0, 1, 0, 1}),
                new Tuple<double, double[]>(0, new double[] { 0, 1, 1, 0}),
                new Tuple<double, double[]>(0, new double[] { 0, 1, 1, 1}),
                new Tuple<double, double[]>(1, new double[] { 1, 0, 0, 0}),
                new Tuple<double, double[]>(1, new double[] { 1, 0, 0, 1}),
                new Tuple<double, double[]>(1, new double[] { 1, 0, 1, 0}),
                new Tuple<double, double[]>(1, new double[] { 1, 0, 1, 1}),
                new Tuple<double, double[]>(1, new double[] { 1, 1, 0, 0}),
                new Tuple<double, double[]>(1, new double[] { 1, 1, 0, 1}),
                new Tuple<double, double[]>(1, new double[] { 1, 1, 1, 0}),
                new Tuple<double, double[]>(1, new double[] { 1, 1, 1, 1}),
            };

                var topology = new Topology(4, 1, 0.1, 2);
                var neuralNetwork = new NeuralNetwork(topology);
                var difference = neuralNetwork.Learn(dataset, 1000000);

                var results = new List<double>();
                foreach (var data in dataset)
                {
                    var res = neuralNetwork.FeedForward(data.Item2.ToList()).Output;
                    results.Add(res);
                }

                for (int i = 0; i < results.Count; i++)
                {
                    var expected = Math.Round(dataset[i].Item1, 4);
                    var actual = Math.Round(results[i], 4);
                    if (expected != actual)
                        Assert.AreEqual(expected, actual);
                }
                Assert.IsTrue
                    (true);
            }
        }
}
