using AiBrain;
using Newtonsoft.Json;
using NUnit.Framework;
using Snake;
using UnityEngine;

namespace SnakeTests
{
    public class SnakeBodyTests
    {
        [Test]
        public void CreateNewSnakeBodyController_WillBeWithSizeOf1()
        {
            var snakeController = new SnakeBodyController();

            Assert.AreEqual(snakeController.Size, 0);
        }

        [Test]
        public void SnakeBodyController_Eat_WillHaveASnakeWithSizeOf2()
        {
            var snakeController = new SnakeBodyController();
        
            snakeController.Eat();
        
            Assert.AreEqual(snakeController.Size, 1);
        }

        [Test]
        public void SnakeBodyController_EatAndThenEatAgain_WillHaveASnakeWithSizeOf3()
        {
            var snakeController = new SnakeBodyController();
        
            snakeController.Eat();
            snakeController.Eat();
        
            Assert.AreEqual(snakeController.Size, 2);
        }

        [Theory]
        public void SerializeNeuralNetwork()
        {
            var network = new NeuralNetwork(0, new[] { 4, 6, 4 });
            NeuralNetwork.FeedForward(new[] { 0.5f, 0.1f, 0f, 0.2f }, network);
            
            NeuralNetwork.Mutate(network, 0.1f);

            var s = JsonConvert.SerializeObject(network);

            var copiedNetwork = JsonConvert.DeserializeObject<NeuralNetwork>(s);
            
            Assert.NotNull(copiedNetwork);
        }
    }
}