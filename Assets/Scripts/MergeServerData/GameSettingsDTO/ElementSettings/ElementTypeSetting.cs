using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.ElementSettings
{
    public class ElementTypeSetting
    {
        public string ElementTypeName { get; set; }
        public int Family { get; set; }
        public string Name { get; set; }
        public string NameLocalizationKey { get; set; }
        public string Description { get; set; }
        public string DescriptionLocalizationKey { get; set; }
        public string BarrelType { get; set; }
        public bool HasLevel { get; set; }
        public int MaxLevel { get; set; }
        public int ElementSize { get; set; }
        public bool IsParticipatingInBattle { get; set; }
        public List<LevelSetting> LevelSettings { get; set; }
        public List<int> Classes { get; set; }
        public bool? IsSelfMergeable { get; set; }
        public int? Rarity { get; set; }
        public bool? IsSeasonal { get; set; }
        public bool? HasStaticLocation { get; set; }
        public bool? IsBattleNeutral { get; set; }
    }
}