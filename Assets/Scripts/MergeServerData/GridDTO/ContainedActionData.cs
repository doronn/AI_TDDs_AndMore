namespace MergeServerData.GridDTO
{
    public class ContainedActionData
    {
        public QuestGameActionType Action { get; set; }
        public long Quantity { get; set; }
        public string ElementType { get; set; }
        public int? ElementLevel { get; set; }
    }
}