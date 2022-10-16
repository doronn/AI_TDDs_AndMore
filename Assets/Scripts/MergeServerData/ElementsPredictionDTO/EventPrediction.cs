using System.Collections.Generic;

namespace MergeServerData.ElementsPredictionDTO
{
    public class EventPrediction
    {
        public string EventName { get; set; }
        public long EndTimeStamp { get; set; }
        public int MergeCounter { get; set; }
        public List<NextElementsForMergeOutput> NextElementsForMergeOutput { get; set; }
    }
}