using MergeServerData.GridDTO;

namespace MergeServerData.DriftDTO
{
    public class NextDriftData
    {
        public long NextDriftTimestamp { get; set; }
        public long NextDriftExpirationTimestamp { get; set; }
        public long CurrentServerTimestamp { get; set; }
        public Element NextElement { get; set; }
    }
}