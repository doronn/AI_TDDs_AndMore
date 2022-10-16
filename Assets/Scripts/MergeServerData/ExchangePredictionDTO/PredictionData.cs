using System.Collections.Generic;

namespace MergeServerData.ExchangePredictionDTO
{
    public class PredictionData
    {
        public string Id { get; set; }
        public List<GridDTO.Element> Elements { get; set; }
    }
}