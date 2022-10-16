using System.Collections.Generic;

namespace MergeServerData.ElementsPredictionDTO
{
    public class EnergyButton
    {
        public string ButtonId { get; set; }
        public bool IsTutorialFinished { get; set; }
        public List<Pool> Pools { get; set; }
        public List<GridDTO.Element> ScriptedEnergy { get; set; }
    }
}