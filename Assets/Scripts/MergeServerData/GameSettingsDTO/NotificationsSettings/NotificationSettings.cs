using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.NotificationsSettings
{
    public class NotificationsSettings
    {
        public Dictionary<int, NotificationExtraData> NotificationExtraData { get; set; }
        public int CloseToNextLeagueAmount { get; set; }
        public int CloseToNextRankAmount { get; set; }
    }
}