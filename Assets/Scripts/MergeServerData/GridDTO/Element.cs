using System.Collections.Generic;

namespace MergeServerData.GridDTO
{
    public class Element
    {
        public string Id { get; set; }
        public int ElementType { get; set; }
        public string ElementTypeName { get; set; }
        public int Level { get; set; }
        public List<ContainedElement> ContainedElements { get; set; }
        public List<ContainedActionData> ContainedActions { get; set; }
        public List<RewardInfo> ContainedRewards { get; set; }
        public bool? IsRemovedOnUnlock { get; set; }
        public int? BuildingIndex { get; set; }
        public GeneratedElement GeneratedElement { get; set; }
    }
}