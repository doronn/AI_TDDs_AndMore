using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.UISettings
{
    public class AreaSettings
    {
        public List<TooltipDefinition> TooltipDefinitions { get; set; }
        public Dictionary<string, TooltipDefinition> WorldTooltipDefinitions { get; set; }
    }
}