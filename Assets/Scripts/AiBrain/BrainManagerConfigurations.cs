using UnityEngine;

namespace AiBrain
{
    [CreateAssetMenu(fileName = "BrainConfig", menuName = "AI Brain/Create new configuration", order = 0)]
    public class BrainManagerConfigurations : ScriptableObject
    {
        [field: SerializeField] public string BrainId { get; private set; } = "bestBrainKey6";

        [field: SerializeField] public bool AutoLearn { get; private set; } = false;

        [field: SerializeField] public float IterationTime { get; private set; } = 30f;

        [field: SerializeField] public int SqrtTotalSimulationsAmount { get; private set; } = 10;

        [field: SerializeField] public GameObject SimulationPrefab { get; private set; }

        [field: SerializeField] public float MutationAmount { get; private set; } = 0.2f;

        [field: SerializeField] public int MapSizeX { get; private set; } = 100;

        [field: SerializeField] public int MapSizeZ { get; private set; } = 100;
    }
}