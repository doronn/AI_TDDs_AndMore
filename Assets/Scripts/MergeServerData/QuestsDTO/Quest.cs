using System.Collections.Generic;
using MergeServerData.CommonDTO;

namespace MergeServerData.QuestsDTO
{
    public class Quest
    {
        public int QuestType { get; set; }
        public int StateType { get; set; }
        public string LinkedElementId { get; set; }
        public bool RemoveElementOnCompletion { get; set; }
        public List<Collectible> Collectibles { get; set; }
        public List<Price> Price { get; set; }
        public List<RequiredAction> Actions { get; set; }
        public List<RewardData> Rewards { get; set; }
        public List<string> ElementsToUnlock { get; set; }
        public long StartTimeStamp { get; set; }
        public long EndTimeStamp { get; set; }
        public List<Collectible> AcceptableElements { get; set; }
        public string QuestName { get; set; }
        public Coordinates InfluenceTile { get; set; }
        public int? Order { get; set; }
    }
}