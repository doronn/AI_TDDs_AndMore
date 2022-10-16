using System.Collections.Generic;

namespace MergeServerData.PlayerDTO
{
    public class PlayerData
    {
        public string Id { get; set; }
        public long LastPlayerUpdateTime { get; set; }
        public string PublicId { get; set; }
        public string SessionId { get; set; }
        public string ClientVersion { get; set; }
        public int Coins { get; set; }
        public int Gems { get; set; }
        public int Energy { get; set; }
        public int XP { get; set; }
        public List<Resource> Resources { get; set; }
        public int QuestLevel { get; set; }
        public int CampLevel { get; set; }
        public int MatchLevel { get; set; }
        public string FtueStep { get; set; }
        public string Name { get; set; }
        public int BattleCounter { get; set; }
        public int DifficultyVector { get; set; }
        public int FinishedTutorials { get; set; }
        public int EnergyOCBPackagesLeftCount { get; set; }
        public int MergeCounter { get; set; }
        public string MatchMakingOverrideId { get; set; }
    }
}