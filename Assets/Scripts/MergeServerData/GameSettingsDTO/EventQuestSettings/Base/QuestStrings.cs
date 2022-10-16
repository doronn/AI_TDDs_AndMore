using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.EventQuestSettings.Base
{
    public class QuestStrings
    {
        public List<string> Intro { get; set; }
        public List<string> IntroLocalizationKeys { get; set; }
        public List<string> Outro { get; set; }
        public List<string> OutroLocalizationKeys { get; set; }
        public string UiClosed { get; set; }
        public string UiClosedLocalizationKey { get; set; }
        public string UiOpen { get; set; }
        public string UiOpenLocalizationKey { get; set; }
    }
}