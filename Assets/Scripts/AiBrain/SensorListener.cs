using System;
using Sensory;

namespace AiBrain
{
    [Serializable]
    public class SensorListener : ISensorListener
    {
        private float[] _sensoryData;
        private ISensoryMetaData _sensoryMetaData;
        
        public float[] SensoryData
        {
            get
            {
                if (_sensoryMetaData == null)
                {
                    throw new Exception("Missing sensoryMetaData implementation");
                }

                var sensoryData = _sensoryMetaData.OnRequestSensoryData(_sensoryData);
                WasMainSensorHit = sensoryData[_sensoryMetaData.GetForwardSensorIndex()] > 0;
                WasAnySensorHit = _sensoryMetaData.GetAnySensorHit();

                return sensoryData;
            }
        }
        
        public bool WasMainSensorHit { get; private set; }
        public bool WasAnySensorHit { get; private set; }

        public void Init(int sensoryDataSize, ISensoryMetaData sensoryMetaData)
        {
            _sensoryData = new float[sensoryDataSize];
            _sensoryMetaData = sensoryMetaData;
        }
    }
}