using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class SnakeController : IMovableCharacter
    {
        private readonly int _moveStepLength;
        private readonly SnakePartController _head;
        private readonly SnakeBodyController _body;
        private readonly Queue<Vector2Int> _tailPartsPositions = new();

        public int BodySize => _body.Size + 1;

        public CardinalDirection FaceDirection => _head.FaceDirection;
        public Vector2Int Position => _head.Position;

        public SnakeController(int moveStepLength = 1)
        {
            _moveStepLength = moveStepLength;
            _head = new SnakePartController();
            _body = new SnakeBodyController();
        }

        public void Eat()
        {
            _body.Eat();
            _tailPartsPositions.Enqueue(Position);
        }

        public Vector2Int TailPartPosition(int partIndex)
        {
            if (partIndex >= _body.Size || partIndex < 0)
            {
                throw new IndexOutOfRangeException(
                    $"The body size is {_body.Size} but tried to get tail part with index {partIndex}");
            }

            var iterator = 0;
            foreach (var tailPartsPosition in _tailPartsPositions)
            {
                if (iterator == partIndex)
                {
                    return tailPartsPosition;
                }

                iterator++;
            }

            throw new IndexOutOfRangeException(
                $"The body size is {_body.Size} and tried to get tail part with index {partIndex} but queue length is {_tailPartsPositions.Count}");
        }

        public void TurnNorth()
        {
            _head.TurnNorth();
        }

        public void TurnSouth()
        {
            _head.TurnSouth();
        }

        public void TurnWest()
        {
            _head.TurnWest();
        }

        public void TurnEast()
        {
            _head.TurnEast();
        }

        public void PerformStep()
        {
            for (var i = 0; i < _moveStepLength; i++)
            {
                _tailPartsPositions.Dequeue();
                _tailPartsPositions.Enqueue(Position);

                _head.PerformStep();
            }
        }
    }
}