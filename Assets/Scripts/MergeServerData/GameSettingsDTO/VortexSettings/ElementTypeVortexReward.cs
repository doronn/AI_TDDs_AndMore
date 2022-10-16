using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.VortexSettings
{
    public class ElementTypeVortexReward
    {
        public string ElementTypeName { get; set; }
        public List<VortexPerLevelReward> VortexPerLevelRewards { get; set; }
    }
}