using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.ElementSettings
{
    public class LevelSetting
    {
        public int Health { get; set; }
        public float[] HealthMilestones { get; set; }
        public AttackStats AttackStats { get; set; }
        public MergeRewards MergeRewards { get; set; }
        public List<string> ElementsToUnlock { get; set; }
        public int? Level { get; set; }
        public double? CoinValue { get; set; }
        public int? Quantity { get; set; }
        public TraitSettings TraitSettings { get; set; }
        public int? DefensePartyCapacity { get; set; }
    }
}