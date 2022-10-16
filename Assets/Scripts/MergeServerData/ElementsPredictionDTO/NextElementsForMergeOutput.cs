namespace MergeServerData.ElementsPredictionDTO
{
    public class NextElementsForMergeOutput
    {
        public GridDTO.Element ElementToReceive { get; set; }
        public int MergeOpsRequired { get; set; }
    }
}