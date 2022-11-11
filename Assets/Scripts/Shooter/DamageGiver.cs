using System;
using UnityEngine;

namespace Shooter
{
    [RequireComponent(typeof(Collider))]
    public class DamageGiver : MonoBehaviour, IDamageGiver, IIdableActor
    {
        public int OwnerId { get; private set; }
        
        [field: SerializeField]
        public float Damage { get; private set; } = 1;

        private Action<int> _onHitTarget = null;
        private Action _afterCollision = null;

        internal void Init(int ownerId, Action<int> onHitTarget, Action afterCollision)
        {
            OwnerId = ownerId;
            _onHitTarget = onHitTarget;
            _afterCollision = afterCollision;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger == false && other.TryGetComponent<IHitTarget>(out var hitTarget))
            {
                var receiveHit = hitTarget.ReceiveHit(this);
                if (receiveHit == 0)
                {
                    return;
                }
                
                _onHitTarget?.Invoke(receiveHit);

                _afterCollision?.Invoke();
            }
        }

        public int Id => OwnerId;
    }
}