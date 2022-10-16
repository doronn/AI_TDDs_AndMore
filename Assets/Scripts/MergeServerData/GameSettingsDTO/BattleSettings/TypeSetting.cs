using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.BattleSettings
{
    public class TypeSetting
    {
        public LinkedElement LinkedElement { get; set; }
        public Dictionary<int, CapacityData> BattleCapacities { get; set; }
        public List<ElementSetting> ElementSettings { get; set; }
        public int? Type { get; set; }
        public int? MaxBattleTimeSeconds { get; set; }
    }
}