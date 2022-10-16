using System.Collections.Generic;

namespace MergeServerData.PlayerLevelDTO
{
    public class PlayerLevelData
    {
        public int CurrentLevel { get; set; }
        public int StartXP { get; set; }
        public int TargetXP { get; set; }
        public List<LevelUpReward> LevelUpRewards { get; set; }
    }
}