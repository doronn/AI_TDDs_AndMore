using System;
using System.Collections.Generic;
using Sensory;
using UnityEngine;

namespace AiBrain
{
    [Serializable]
    public class SensorListener : ISensorListener
    {
        public float FleeAvoidOrAttack { get; set; }
        public float[] SensoryData { get; set; }
    }
}