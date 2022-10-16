using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.BattleSettings
{
    public class BattleSettings
    {
        public int MaxBattleTimeSeconds { get; set; }
        public int MaxDeploymentSelectionTimeSeconds { get; set; }
        public int MinimumAmountOfElements { get; set; }
        public List<double> SpeedFactors { get; set; }
        public CommandFlagSettings CommandFlagSettings { get; set; }
        public List<CastleLevelSetting> CastleLevelSettings { get; set; }
        public List<ElementSetting> ElementSettings { get; set; }
        public List<TypeSetting> TypeSettings { get; set; }
    }
}