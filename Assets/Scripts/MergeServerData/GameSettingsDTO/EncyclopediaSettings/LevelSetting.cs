using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.EncyclopediaSettings
{
    public class LevelSetting
    {
        public int Level { get; set; }
        public List<RevealPrizeElement> RevealPrizeElements { get; set; }
        public string DescriptionLocalizationKey { get; set; }
        public RevealPrizes RevealPrizes { get; set; }
        public string Description { get; set; }
    }
}