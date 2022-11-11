using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Character/Create new configuration", order = 0)]
    public class CharacterConfiguration : ScriptableObject
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }

        [field: SerializeField] public float ForwardSpeed { get; private set; }

        [field: SerializeField] public float BackwardSpeed { get; private set; }

        [field: SerializeField] public float StrafeSpeed { get; private set; }

        [field: SerializeField] public float ShootingDelay { get; private set; }

        [field: SerializeField] public float InitialHealth { get; private set; }
        
        [field: SerializeField] public float HealthBonusForKill { get; private set; }

        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
    }
}