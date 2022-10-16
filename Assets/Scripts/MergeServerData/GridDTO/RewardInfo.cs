namespace MergeServerData.GridDTO
{
    public class RewardInfo
    {
        public RewardType Reward { get; set; }
        public long? Amount { get; set; }
        public string ElementType { get; set; }
        public int? Level { get; set; }
        public string ElementId { get; set; }
        public bool ShowAsBarrel { get; set; }
    }
}