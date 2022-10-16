using System;
using UnityEngine;

namespace Shooter
{
    [RequireComponent(typeof(Collider))]
    public class HitTarget : MonoBehaviour, IHitTarget
    {
        [field: SerializeField]
        public Character Owner { get; private set; }
        
        public bool ReceiveHit(IDamageGiver damageGiver)
        {
            if (Owner == null)
            {
                throw new Exception("HitTarget has now owner");
            }

            if (damageGiver.OwnerId == Owner.Id)
            {
                return false;
            }
            
            Owner.ReceiveDamage(damageGiver.Damage);
            return true;
        }
    }
}