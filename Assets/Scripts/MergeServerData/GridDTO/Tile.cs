using MergeServerData.CommonDTO;

namespace MergeServerData.GridDTO
{
    public class Tile
    {
        public int TileState { get; set; }
        public Coordinates Coordinates { get; set; }
        public int? IslandType { get; set; }
        public int? THLevel { get; set; }
        public int? Order { get; set; }
    }
}