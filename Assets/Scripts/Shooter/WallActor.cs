using UnityEngine;

namespace Shooter
{
    public class WallActor : MonoBehaviour, IObstacle, IdableActor, IHitTarget
    {
        public int Id { get; } = -1;
        public bool ReceiveHit(IDamageGiver damageGiver)
        {
            return false;
        }
    }
}