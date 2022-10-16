using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.BattleSettings
{
    public class ElementSetting
    {
        public string ElementTypeName { get; set; }
        public List<LevelSetting> LevelSettings { get; set; }
        public List<int> AttackPriorities { get; set; }
        public double? MomentumFactor { get; set; }
    }
}