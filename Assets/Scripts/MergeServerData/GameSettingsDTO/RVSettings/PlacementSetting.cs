using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.RVSettings
{
    public class PlacementSetting
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<PrizeData> Prizes { get; set; }
        public AdditionalProperties AdditionalProperties { get; set; }
    }
}