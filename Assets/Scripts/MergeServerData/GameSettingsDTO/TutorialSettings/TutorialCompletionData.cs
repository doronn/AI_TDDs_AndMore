using System.Collections.Generic;

namespace MergeServerData.GameSettingsDTO.TutorialSettings
{
    public class TutorialCompletionData
    {
        public int Type { get; set; }
        public List<CompletionPrize> CompletionPrizes { get; set; }
        public AdditionalProperties AdditionalProperties { get; set; }
        public List<ElementsToRemoveOnCompletion> ElementsToRemoveOnCompletion { get; set; }
    }
}