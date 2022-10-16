namespace MergeServerData.GameSettingsDTO.QuestsSettings
{
    public class QuestLevelData
    {
        public double GemToCoinsRatio { get; set; }
        public UpgradeQuestDefinitionData[][] QuestBatches { get; set; }
        public int? QuestLevel { get; set; }
        public bool? ShowRateUsPopup { get; set; }
    }
}