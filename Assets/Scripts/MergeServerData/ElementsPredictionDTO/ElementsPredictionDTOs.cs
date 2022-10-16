using System.Collections.Generic;

namespace MergeServerData.ElementsPredictionDTO
{
    public class Root
    {
        public List<NextElementsForMergeOutput> NextElementsForMergeOutput { get; set; }
        public List<EventPrediction> EventPredictions { get; set; }
        public List<EnergyButton> EnergyButtons { get; set; }
    }
}