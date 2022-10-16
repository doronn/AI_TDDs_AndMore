using UnityEngine;

namespace Snake
{
    public interface IMovableCharacter
    {
        CardinalDirection FaceDirection { get; }
        Vector2Int Position { get; }
        void TurnNorth();
        void TurnSouth();
        void TurnWest();
        void TurnEast();
        void PerformStep();
    }
}