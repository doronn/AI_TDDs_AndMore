using Snake;

namespace SnakeTests.Extensions
{
    public static class MovableCharacterExtensions
    {
        public static void TurnToCardinal(this IMovableCharacter movableCharacter, CardinalDirection cardinalDirection)
        {
            switch (cardinalDirection)
            {
                case CardinalDirection.North:
                    movableCharacter.TurnNorth();
                    break;
                case CardinalDirection.East:
                    movableCharacter.TurnEast();
                    break;
                case CardinalDirection.South:
                    movableCharacter.TurnSouth();
                    break;
                case CardinalDirection.West:
                    movableCharacter.TurnWest();
                    break;
            }
        }
    }
}