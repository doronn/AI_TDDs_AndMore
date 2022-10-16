using System.Collections.Generic;
using UnityEngine;

namespace Sensory
{
    public interface ISensorListener
    {
        float FleeAvoidOrAttack { get; set; }
        float[] SensoryData { get; set; }
    }
}