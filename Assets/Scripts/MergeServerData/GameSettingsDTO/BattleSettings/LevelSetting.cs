using System.Collections.Generic;
using MergeServerData.CommonDTO;

namespace MergeServerData.GameSettingsDTO.BattleSettings
{
    public class LevelSetting
    {
        public List<Coordinates> Formation { get; set; }
        public int? Level { get; set; }
    }
}