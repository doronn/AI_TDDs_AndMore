using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.RecurringGatesSettings
{
    public class Setting
    {
        public string QuestName { get; set; }
        public string AnchorElementType { get; set; }
        public int AnchorElementLevel { get; set; }
        public int QuestCooldownMinutes { get; set; }
        public string QuestBlockingElementType { get; set; }
        public List<string> ElementsToUnlock { get; set; }
        public string MinimumSupportedVersion { get; set; }
        public int? QuestDurationMinutes { get; set; }
    }
}