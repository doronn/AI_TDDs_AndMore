using System.Collections.Generic;
using MergeServerData.GridDTO;

namespace MergeServerData.GameSettingsDTO.EncyclopediaSettings
{
    public class EncyclopediaSetting
    {
        public string ElementTypeName { get; set; }
        public string Description { get; set; }
        public string DescriptionLocalizationKey { get; set; }
        public int Group { get; set; }
        public string SubGroupName { get; set; }
        public string SubGroupNameLocalizationKey { get; set; }
        public bool IsLowerSubHeader { get; set; }
        public List<Element> RecipeRevealPrizeElements { get; set; }
        public List<LevelSetting> LevelSettings { get; set; }
        public AdditionalProperties AdditionalProperties { get; set; }
        public RecipePrizes RecipePrizes { get; set; }
        public bool? HideIfNotRevealed { get; set; }
    }
}