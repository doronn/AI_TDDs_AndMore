using UnityEngine;

namespace Shooter
{
    public class WallActor : MonoBehaviour, IObstacle, IIdableActor, IHitTarget
    {
        public int Id { get; } = -1;
        public int ReceiveHit(IDamageGiver damageGiver)
        {
            return -1;
        }
    }
}