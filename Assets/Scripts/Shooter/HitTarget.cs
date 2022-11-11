using System;
using UnityEngine;

namespace Shooter
{
    [RequireComponent(typeof(Collider))]
    public class HitTarget : MonoBehaviour, IHitTarget
    {
        [field: SerializeField]
        public Character Owner { get; private set; }

        [SerializeField]
        private LayerMask _badLayersToCollideWith;
        
        public int ReceiveHit(IDamageGiver damageGiver)
        {
            if (Owner == null)
            {
                throw new Exception("HitTarget has now owner");
            }

            if (damageGiver.OwnerId == Owner.Id)
            {
                return 0;
            }
            
            Owner.ReceiveDamage(damageGiver.Damage);
            return Owner.Health <= 0 ? 2 : 1;
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            var collidedWithAWall = (collisionInfo.gameObject.layer & _badLayersToCollideWith.value) > 0;
            if (collidedWithAWall)
            {
                Owner.ReportGotBadCollision();
            }
        }
    }
}