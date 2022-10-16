using System;
using System.Collections;
using NUnit.Framework;
using Snake;
using UnityEngine;
using UnityEngine.TestTools;

namespace SnakeTests
{
    public class SnakeControllerTests
    {
        [Test]
        public void NewSnakeController_HasOneSnakePartInOrigin()
        {
            var snakeController = new SnakeController();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(0, 0));
            Assert.AreEqual(snakeController.BodySize, 1);
        }
        
        [Test]
        public void SnakeController_EatsWillHaveBodySizeOf2()
        {
            var snakeController = new SnakeController();

            snakeController.Eat();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(0, 0));
            Assert.AreEqual(snakeController.BodySize, 2);
        }
    
        [Test]
        public void SnakeController_WithBodySize2_MoveEast_HeadPositionIs_1_0_BodyPartPositionIsAtOrigin()
        {
            var snakeController = new SnakeController();

            snakeController.Eat();
            snakeController.TurnEast();
            snakeController.PerformStep();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(1, 0));
            Assert.AreEqual(snakeController.TailPartPosition(0), new Vector2Int(0, 0));
        
            Assert.AreEqual(snakeController.BodySize, 2);
        }
    
        [Test]
        public void SnakeController_WithBodySize2_MoveEastTwice_HeadPositionIs_2_0_BodyPartPositionIsAt_1_0()
        {
            var snakeController = new SnakeController();

            snakeController.Eat();
            snakeController.TurnEast();
            snakeController.PerformStep();
            snakeController.PerformStep();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(2, 0));
            Assert.AreEqual(snakeController.TailPartPosition(0), new Vector2Int(1, 0));
        
            Assert.AreEqual(snakeController.BodySize, 2);
        }
    
        [Test]
        public void SnakeController_TailPartPosition_WithIndexOutOfRange_ThrowsIndexOutOfRangeException()
        {
            var snakeController = new SnakeController();

            snakeController.Eat();
            snakeController.TurnEast();
            snakeController.PerformStep();
            snakeController.PerformStep();

            void Actual()
            {
                var overflowIndex = 1;
                snakeController.TailPartPosition(overflowIndex);
            }

            Assert.Throws<IndexOutOfRangeException>(Actual);
        }
    
        [Theory]
        public void SnakeController_WithMoveStepOf_2_WithBodySize2_MoveEastTwice_HeadPositionIs_4_0_BodyPartPositionIsAt_3_0()
        {
            var snakeController = new SnakeController(2);

            snakeController.Eat();
            snakeController.TurnEast();
            snakeController.PerformStep();
            snakeController.PerformStep();
        
            Assert.AreEqual(snakeController.Position, new Vector2Int(4, 0));
            Assert.AreEqual(snakeController.TailPartPosition(0), new Vector2Int(3, 0));
        
            Assert.AreEqual(snakeController.BodySize, 2);
        }
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator SnakeControllerTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}