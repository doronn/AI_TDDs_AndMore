using System.Collections.Generic;

namespace MergeServerData.ExchangePredictionDTO
{
    public class ExchangePredictionData
    {
        public int Level { get; set; }
        public List<PredictionData> Predictions { get; set; }
    }
}