using NUnit.Framework;
using Snake;
using SnakeTests.Extensions;
using UnityEngine;

namespace SnakeTests
{
    public class SnakePartControllerTests
    {
        [Test]
        public void CreateNewSnakePartController_WillBeFacingEast()
        {
            var snakeController = new SnakePartController();

            Assert.AreEqual(snakeController.FaceDirection, CardinalDirection.East);
        }

        [TestCase(CardinalDirection.North, ExpectedResult = CardinalDirection.North)]
        [TestCase(CardinalDirection.East, ExpectedResult = CardinalDirection.East)]
        [TestCase(CardinalDirection.South, ExpectedResult = CardinalDirection.South)]
        [TestCase(CardinalDirection.West, ExpectedResult = CardinalDirection.East)]
        [TestCase(CardinalDirection.North, CardinalDirection.West, ExpectedResult = CardinalDirection.North)]
        [TestCase(CardinalDirection.South, CardinalDirection.West, ExpectedResult = CardinalDirection.South)]
        [TestCase(CardinalDirection.South, CardinalDirection.North, ExpectedResult = CardinalDirection.North)]
        [TestCase(CardinalDirection.North, CardinalDirection.South, ExpectedResult = CardinalDirection.South)]
        [TestCase(CardinalDirection.North, CardinalDirection.West, CardinalDirection.South, ExpectedResult = CardinalDirection.South)]
        public CardinalDirection NewSnake_TurnTo(params CardinalDirection[] directions)
        {
            var snakeController = new SnakePartController();
            foreach (var direction in directions)
            {
                snakeController.TurnToCardinal(direction);
            }

            return snakeController.FaceDirection;
        }

        [Test]
        public void CreateNewSnakePartController_WillBePlacedIn_0_0()
        {
            var snakeController = new SnakePartController();

            Assert.AreEqual(snakeController.Position, Vector2Int.zero);
        }

        [Test]
        public void SnakePartController_PerformStep_WillBePlacedIn_1_0()
        {
            var snakeController = new SnakePartController();

            snakeController.PerformStep();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(1, 0));
        }

        [Test]
        public void SnakePartController_PerformStepTwice_WillBePlacedIn_2_0()
        {
            var snakeController = new SnakePartController();

            snakeController.PerformStep();
            snakeController.PerformStep();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(2, 0));
        }

        [Test]
        public void SnakePartController_WhileTurningNorth_PerformStep_WillBePlacedIn_0_1()
        {
            var snakeController = new SnakePartController();

            snakeController.TurnNorth();
            snakeController.PerformStep();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(0, 1));
        }

        [Test]
        public void PerformStep_WhileTryingToGoNorth_ThanWest_WillGoNorth_0_1()
        {
            var snakeController = new SnakePartController();

            snakeController.TurnNorth();
            snakeController.TurnWest();
            snakeController.PerformStep();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(0, 1));
        }
    }
}