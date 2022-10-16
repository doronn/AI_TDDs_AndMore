using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.RVSettings
{
    public class RVSettings
    {
        public int Capacity { get; set; }
        public List<PlacementSetting> PlacementSettings { get; set; }
    }
}