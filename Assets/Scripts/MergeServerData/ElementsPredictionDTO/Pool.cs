using System.Collections.Generic;

namespace MergeServerData.ElementsPredictionDTO
{
    public class Pool
    {
        public string PoolId { get; set; }
        public string ConsumptionElementType { get; set; }
        public List<NextElement> NextElements { get; set; }
    }
}