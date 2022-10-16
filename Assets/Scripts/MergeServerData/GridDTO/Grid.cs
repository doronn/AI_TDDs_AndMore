using System.Collections.Generic;
using MergeServerData.CommonDTO;

namespace MergeServerData.GridDTO
{
    public class Grid
    {
        public List<Tile> Tiles { get; set; }
        public List<Element> Elements { get; set; }
        public List<Element> StoredElements { get; set; }
        public string VisualDataId { get; set; }
        public int Revision { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Level { get; set; }
        public FloatCoordinates ShipCoordinates { get; set; }
        public FloatCoordinates RaftCoordinates { get; set; }
        public FloatCoordinates RvCoordinates { get; set; }
        public Dictionary<string, List<Coordinates>> ElementToCoordinatesMap { get; set; }
    }
}