using System.Collections.Generic;
using MergeServerData.GameSettingsDTO.EventQuestSettings.Base;

namespace MergeServerData.GameSettingsDTO.EventQuestSettings.Linear
{
    public class LinearEventQuestSetting : EventQuestSettingBase
    {
        public List<QuestLevelCopyData> LevelSettings { get; set; }
    }
}