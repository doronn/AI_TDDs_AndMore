using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.RecurringGatesSettings
{
    public class RecurringGatesSettings
    {
        public string MinimumSupportedVersion { get; set; }
        public List<Setting> Settings { get; set; }
    }
}