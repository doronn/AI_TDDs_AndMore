using System;
using System.Collections.Generic;

namespace AiBrain
{
    [Serializable]
    public class NeuralNetwork
    {
        public BrainLevel[] BrainLevels;
        private readonly int _currentRandomSeed;
        
        private readonly Random _random;

        public NeuralNetwork(params int[] neuronCounts)
        {
            _currentRandomSeed = Guid.NewGuid().GetHashCode();
            _random = new Random(_currentRandomSeed);
            BrainLevels = new BrainLevel[neuronCounts.Length - 1];

            for (var i = 0; i < neuronCounts.Length - 1; i++)
            {
                BrainLevels[i] = new BrainLevel(neuronCounts[i], neuronCounts[i + 1], _random);
            }
        }

        public NeuralNetwork()
        {
            _currentRandomSeed = Guid.NewGuid().GetHashCode();
            _random = new Random(_currentRandomSeed);
        }

        public static float[] FeedForward(float[] givenInputs, NeuralNetwork network)
        {
            var outputs = BrainLevel.FeedForward(givenInputs, network.BrainLevels[0]);
            for (var i = 1; i < network.BrainLevels.Length; i++)
            {
                outputs = BrainLevel.FeedForward(outputs, network.BrainLevels[i]);
            }

            return outputs;
        }

        public static void Mutate(NeuralNetwork network, float amount = 1)
        {
            foreach (var level in network.BrainLevels)
            {
                for (var i = 0; i < level.Biases.Length; i++)
                {
                    level.Biases[i] = AiUtils.Lerp(
                        level.Biases[i],
                        network._random.GetNextRandom(),
                        amount
                    );
                }

                for (var i = 0; i < level.Weights.Length; i++)
                {
                    for (var j = 0; j < level.Weights[i].Length; j++)
                    {
                        level.Weights[i][j] = AiUtils.Lerp(
                            level.Weights[i][j],
                            network._random.GetNextRandom(),
                            amount
                        );
                    }
                }
            }
        }
    }
}