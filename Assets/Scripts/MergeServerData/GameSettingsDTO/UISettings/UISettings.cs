namespace MergeServerData.GameSettingsDTO.UISettings
{
    public class UISettings
    {
        public VortexSettings VortexSettings { get; set; }
        public LeaderboardSettings LeaderboardSettings { get; set; }
        public AreaSettings AreaSettings { get; set; }
        public int IdleTimeSeconds { get; set; }
    }
}