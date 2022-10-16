using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.ElementCapacitySettings
{
    public class LevelSetting
    {
        public int QuestLevel { get; set; }
        public List<ElementCapacity> ElementCapacities { get; set; }
        public List<FamilyCapacity> FamilyCapacities { get; set; }
    }
}