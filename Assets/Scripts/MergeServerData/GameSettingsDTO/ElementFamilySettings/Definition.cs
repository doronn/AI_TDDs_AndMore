namespace MergeServerData.GameSettingsDTO.ElementFamilySettings
{
    public class Definition
    {
        public int ElementFamily { get; set; }
        public bool IsCollectible { get; set; }
        public bool IsTrashable { get; set; }
        public bool? IsElementContainer { get; set; }
        public bool? HasHealth { get; set; }
        public bool? IsExcludedFromPremiumIslands { get; set; }
        public bool? IsShippable { get; set; }
    }
}