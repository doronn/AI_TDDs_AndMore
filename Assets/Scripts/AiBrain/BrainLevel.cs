using System;
using System.Linq;
using Random = System.Random;

namespace AiBrain
{
    [Serializable]
    public class BrainLevel
    {
        public float[] Inputs;
        public float[] Outputs;
        public float[] Biases;
        public float[][] Weights;

        private readonly Random _random;
        public BrainLevel(int inputCount, int outputCount, Random random)
        {
            _random = random;
            Inputs = new float[inputCount];
            Outputs = new float[outputCount];
            Biases = new float[outputCount];

            Weights = new float[inputCount][];
            for (int i = 0; i < inputCount; i++)
            {
                Weights[i] = new float[outputCount];
            }
            
            Randomize(this);
        }

        private static void Randomize(BrainLevel level)
        {
            for (var i = 0; i < level.Inputs.Length; i++)
            {
                for (var j = 0; j < level.Outputs.Length; j++)
                {
                    level.Weights[i][j] = level._random.GetNextRandom();
                }
            }

            for (var i = 0; i < level.Biases.Length; i++)
            {
                level.Biases[i] = level._random.GetNextRandom();
            }
        }

        public static float[] FeedForward(float[] givenInputs, BrainLevel level)
        {
            for (var i = 0; i < level.Inputs.Length; i++)
            {
                try
                {
                    level.Inputs[i] = givenInputs[i];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            for (var i = 0; i < level.Outputs.Length; i++)
            {
                var sum = level.Inputs.Select((t, j) => t * level.Weights[j][i]).Sum();

                if (sum > level.Biases[i])
                {
                    level.Outputs[i] = 1;
                }
                else
                {
                    level.Outputs[i] = 0;
                }
            }

            return level.Outputs;
        }
    }
}