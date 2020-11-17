using Core.NeuralNetworkImplementation;
using Core.Repositories.Contracts;
using Core.Services.Contracts;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ClassificationService : IClassificationService
    {
        private const string _alphabet = "абвгдеєжзиійїклинопрстуфхцчшщьюя123456789";
        private NeuralNetwork _neuralNetwork;
        public ClassificationService()
        {
            if (File.Exists("Network.bin"))
            {
                _neuralNetwork = NeuralNetwork.DeSerialize();
            }
            else
            {
                _neuralNetwork = new NeuralNetwork(1);
                _neuralNetwork.AddInputLayer(400)
                    .AddHiddenLayer(128)
                    .AddHiddenLayer(64)
                    .AddHiddenLayer(41)
                    .AddOutputLayer(41);
                _neuralNetwork.Train(100);
                _neuralNetwork.Serialize();
            }
        }
        public async Task<string> ClassifyImageCharacters(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = File.Create(filePath))
            {
                stream.Position = 0;
                stream.Flush();
                await file.CopyToAsync(stream);
            }
            List<Matrix<double>> inputs = GetSignalsFromFile(filePath);
            StringBuilder stringBuilder =
                new StringBuilder();
            foreach(var input in inputs)
            {
                int predictedClass = _neuralNetwork.Classify(input);
                stringBuilder.Append(
                    _alphabet[ predictedClass]
                );
            }
            return stringBuilder.ToString();
        }

        private List<Matrix<double>> GetSignalsFromFile(string filePath)
        {
            List<Matrix<double>> inputs = new List<Matrix<Double>>();
            Bitmap bmp = new Bitmap(filePath);
            for (int sector = 0; sector < bmp.Width; sector += 20)
            {
                inputs.Add(Matrix<double>.Build.Dense(1, 400));
                for (int i = sector; i < sector +  20; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        if (bmp.GetPixel(i, j).ToArgb() == -1)
                        {
                            inputs[sector/20][0, i % 20 * 20 + j] = 0;
                        }
                        else
                        {
                            inputs[sector/20][0, i % 20 * 20 + j] = 1;
                        }
                    }
                }
            }
            return inputs;
        }
    }
}
