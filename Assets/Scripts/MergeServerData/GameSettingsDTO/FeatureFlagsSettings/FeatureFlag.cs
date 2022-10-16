using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.FeatureFlagsSettings
{
    public class FeatureFlags
    {
        public int EnergyCapacityStrategy { get; set; }
        public int SwapElementsStrategy { get; set; }
        public int SoulsBattleProviderStrategy { get; set; }
        public bool WriteDebugLogs { get; set; }
        public int EnableBuyQuestRequirementWithGemsFromQuestLevel { get; set; }
        public List<string> DisabledIslandTypes { get; set; }
    }
}