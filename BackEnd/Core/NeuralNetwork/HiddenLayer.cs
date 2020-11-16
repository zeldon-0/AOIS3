using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Core.NeuralNetwork
{
    [Serializable]
    public class HiddenLayer
    {
        private Matrix<double> _weights;
        
        public HiddenLayer(double[,] array)
        {
            _weights = Matrix<double>.Build.DenseOfArray(array);
        }
        public HiddenLayer(int prevLayerSize, int curLayerSize)
        {
            Random random = new Random();
            _weights = Matrix<double>.Build.Dense(prevLayerSize, curLayerSize,
                    (i, j) => 2 * random.NextDouble() - 1);

        }
        public Matrix<double> FeedForward(Matrix<double> input)
        {
            Matrix<double> output = input * _weights;

            return output;
        }

        public void UpdateWeights(Matrix<double> error, double learningRate)
        {
            _weights = _weights + (error * learningRate);
        }

        public int GetSize()
        {
            return _weights.ColumnCount;
        }

        public Matrix<double> GetWeights()
        {
            return _weights;
        }
    }
}
