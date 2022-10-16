using System;
using UnityEngine;

namespace Shooter
{
    [RequireComponent(typeof(Collider))]
    public class DamageGiver : MonoBehaviour, IDamageGiver, IdableActor
    {
        public int OwnerId { get; private set; }
        
        [field: SerializeField]
        public float Damage { get; private set; } = 1;

        private Action _onHitTarget = null;
        private Action _afterCollision = null;

        internal void Init(int ownerId, Action onHitTarget, Action afterCollision)
        {
            OwnerId = ownerId;
            _onHitTarget = onHitTarget;
            _afterCollision = afterCollision;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger == false && other.TryGetComponent<IHitTarget>(out var hitTarget))
            {
                if (hitTarget.ReceiveHit(this))
                {
                    _onHitTarget?.Invoke();
                }
                
                _afterCollision?.Invoke();
            }
        }

        public int Id => OwnerId;
    }
}