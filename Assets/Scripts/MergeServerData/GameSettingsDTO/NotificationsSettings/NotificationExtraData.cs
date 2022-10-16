namespace MergeServerData.GameSettingsDTO.NotificationsSettings
{
    public class NotificationExtraData
    {
        public bool Recurring { get; set; }
        public bool IsTimeOfDay { get; set; }
        public long Schedule { get; set; }
        public int Cooldown { get; set; }
    }
}