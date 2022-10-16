
using UnityEngine;

namespace Snake
{
    public class SnakePartController : IMovableCharacter
    {
        private const int STEP_SIZE_IS_ONE = 1;
        public SnakePartController()
        {
            _lastStepFaceDirection = CardinalDirection.East;
            FaceDirection = CardinalDirection.East;
        }

        private CardinalDirection _lastStepFaceDirection;
        public CardinalDirection FaceDirection { get; private set; }
        public Vector2Int Position { get; private set; }

        public void TurnNorth()
        {
            if (_lastStepFaceDirection == CardinalDirection.South)
            {
                return;
            }
            FaceDirection = CardinalDirection.North;
        }

        public void TurnSouth()
        {
            if (_lastStepFaceDirection == CardinalDirection.North)
            {
                return;
            }
            FaceDirection = CardinalDirection.South;
        }

        public void TurnWest()
        {
            if (_lastStepFaceDirection == CardinalDirection.East)
            {
                return;
            }
            FaceDirection = CardinalDirection.West;
        }

        public void TurnEast()
        {
            if (_lastStepFaceDirection == CardinalDirection.West)
            {
                return;
            }
            FaceDirection = CardinalDirection.East;
        }

        public void PerformStep()
        {
            var offsetVector = GetMovementOffsetForCurrentDirection();
            _lastStepFaceDirection = FaceDirection;
            Position += offsetVector;
        }

        private Vector2Int GetMovementOffsetForCurrentDirection()
        {
            var movementStepX = FaceDirection switch
            {
                CardinalDirection.East => STEP_SIZE_IS_ONE,
                CardinalDirection.West => -STEP_SIZE_IS_ONE,
                _ => 0
            };
            var movementStepY = FaceDirection switch
            {
                CardinalDirection.North => STEP_SIZE_IS_ONE,
                CardinalDirection.South => -STEP_SIZE_IS_ONE,
                _ => 0
            };

            var offsetVector = new Vector2Int(movementStepX, movementStepY);
            return offsetVector;
        }
    }
}