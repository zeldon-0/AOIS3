using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Core.NeuralNetworkImplementation
{
    [Serializable]
    public class NeuralNetwork
    {
        private double _learningRate;
        private Matrix<double> _input;
        private Matrix<double> _output;
        private List<Matrix<double>> _layerOutputs;
        private List<Matrix<double>> _activatedOutputs;
        private List<HiddenLayer> _hiddenLayers = new List<HiddenLayer>();
        public double LearningRate 
        {
            get => _learningRate;
            set => _learningRate = value;
        }

        public List<double[,]> HiddenLayers
        {
            get => _hiddenLayers.Select(m => m.GetWeights().ToArray()).ToList();
            set => _hiddenLayers = value.Select(v => new HiddenLayer(v)).ToList();
        }
        public NeuralNetwork() { }
        public NeuralNetwork(List<HiddenLayer> layers) 
        {
            _hiddenLayers = layers;
        }
        public NeuralNetwork(double learningRate) 
        {
            _learningRate = learningRate;
        }
        public NeuralNetwork AddInputLayer(int size)
        {
            if (_input != null)
                throw new ApplicationException("Input layer already exists.");

            _input = Matrix<double>.Build.Dense(1, size); ;
            return this;
        }
        public NeuralNetwork AddHiddenLayer(int outputSize)
        {
            if(_input == null)
                throw new ApplicationException("you should first add the input layer.");
            if (_hiddenLayers.Any())
            {
                _hiddenLayers.Add(
                    new HiddenLayer(_hiddenLayers.Last().GetSize(), outputSize)
                );
            }
            else
            {
                _hiddenLayers.Add(
                    new HiddenLayer(_input.ColumnCount, outputSize)
                );
            }
            return this;
        }
        public NeuralNetwork AddOutputLayer(int size)
        {
            if (_output != null)
                throw new ApplicationException("Ouput layer already exists.");
            if (!_hiddenLayers.Any() || 
                _hiddenLayers.Last().GetSize() != size)
            {
                throw new ApplicationException("Ouput layer already exists.");
            }
            _output = Matrix<double>.Build.Dense(1, size);

            return this;
        }

        public void Train(int epochs)
        {
            (List<Matrix<double>> inputs, List<Matrix<double>> outputs) = LoadData();
            for (int i = 0; i < epochs; i ++)
            {
                _layerOutputs = new List<Matrix<double>> ();
                _activatedOutputs = new List<Matrix<double>>();

                for(int j = 0; j < inputs.Count; j++)
                {
                    //Console.WriteLine($"Current dataline: {i}");
                    ForwardPass(inputs[j]);
                    List<Matrix<double>> weightChanges = BackwardPass(outputs[j]);
                    UpdateWeights(weightChanges);
                }
            }
        }
        public int Classify(Matrix<double> input)
        {
            ForwardPass(input);
            Matrix<double> output = _activatedOutputs.Last();
            double max = 0;
            int predictedClass = 0;
            for (int i = 0; i < 41; i++)
            {
                if (output[0, i] > max)
                {
                    max = output[0, i];
                    predictedClass = i;
                }
            }
            /*for (int i = 0; i < 41; i ++)
            {
                if (output[0, i] == max)
                {
                    Console.WriteLine($"Predicted class:{i}");
                }
            }*/
            return predictedClass;
        }

        private void UpdateWeights(List<Matrix<double>> weightChanges)
        {
            for (int i = 0; i < _hiddenLayers.Count; i ++)
            {
                _hiddenLayers[i].UpdateWeights(
                    weightChanges[i],
                    _learningRate
               );
            }
        }
        private void ForwardPass(Matrix<double> input) 
        {
            _activatedOutputs.Add(input);
            for (int i = 0; i < _hiddenLayers.Count; i ++)
            {
                input = Sigmoid(_hiddenLayers[i].FeedForward(input));
                _activatedOutputs.Add(input);

//                _activatedOutputs.Add(Sigmoid(_hiddenLayers[i].FeedForward(_activatedOutputs[i])));
            }
        }

        private List<Matrix<double>> BackwardPass(Matrix<double> output) 
        {

            List<Matrix<double>> weightChanges = new List<Matrix<double>>();
            for (int i = 0; i < _hiddenLayers.Count; i++)
            {
                weightChanges.Add(null);
            }
            Matrix<double> outputError = output - _activatedOutputs.Last();
            Matrix<double> outputDelta = Matrix<double>.op_DotMultiply(outputError, SigmoidDerivative(_activatedOutputs.Last()));
            weightChanges[weightChanges.Count - 1] =
                _activatedOutputs[_activatedOutputs.Count - 2]
                .Transpose() * (outputDelta);
            Matrix<double> prevDelta = (Matrix<double>)outputDelta.Clone();
            for (int i = _hiddenLayers.Count - 2; i >= 0; i--)
            {
                Matrix<double> curError = prevDelta
                    * (_hiddenLayers[i + 1]
                    .GetWeights()
                    .Transpose());
                Matrix<double> curDelta = Matrix<double>.op_DotMultiply(curError, SigmoidDerivative(
                    _activatedOutputs[i + 1]));
                weightChanges[i] =
                    _activatedOutputs[i]
                    .Transpose()
                    * (curDelta);
                prevDelta = curDelta;
            }

            return weightChanges;

        }

        private Matrix<double> Sigmoid(Matrix<double> matrix)
        {
            matrix = matrix.Map(m => Sigmoid(m));
            return matrix;
        }
        private double Sigmoid(double value)
        {
            // return 1 / (1 + Math.Exp(-value));
            var exp = Math.Exp(value);
            return exp /(1 + exp);
        }
        private double SigmoidDerivative(double value)
        {
            //return Sigmoid(value) * (1 - Sigmoid(value));
            return value * (1 - value);
        }

        private Matrix<double> SigmoidDerivative(Matrix<double> matrix)
        {
            matrix = matrix.Map(m => SigmoidDerivative(m));
            return matrix;
        }

     
        private (List<Matrix<double>>, List<Matrix<double>>) LoadData()
        {
            List<Matrix<double>> inputs = new List<Matrix<double>>();
            List<Matrix<double>> outputs = new List<Matrix<double>>();

            using (var reader = new StreamReader(@"E:\AOIS\AOIS3.Prep\AOIS3.Prep\Cyrillic\data.csv"))// @"E:\AOIS\AOIS3.Prep\AOIS3.Prep\mnist_train.csv"))
            {
                List<string> stringValues = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Matrix<double> outputValues = Matrix<double>.Build.Dense(1, 41);
                    int a = Int32.Parse(values[0]);
                    outputValues[0, Int32.Parse(values[0]) - 1] = 1;
                    Matrix<double> inputValues = Matrix<double>.Build.Dense(1, 400);
                    for (int i = 0; i < 400; i++)
                    {

                        double value = Double.Parse(values[i + 1]); /// 255;
                        inputValues[0, i] = value;
                    }
                    inputs.Add(inputValues);
                    outputs.Add(outputValues);
                }
            }
            return (inputs, outputs);
        }

        public void Serialize()
        {
            /*string jsonString = JsonSerializer.Serialize(HiddenLayers);
            File.WriteAllText("network.json", jsonString);*/
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Network.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static NeuralNetwork DeSerialize()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Network.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            NeuralNetwork obj = (NeuralNetwork)formatter.Deserialize(stream);
            stream.Close();
            return obj;
        }
    }
}
