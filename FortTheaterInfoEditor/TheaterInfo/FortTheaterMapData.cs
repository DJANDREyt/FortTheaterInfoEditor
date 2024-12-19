
using System.Collections.Generic;


public class FortTheaterMapData
{
    public string DisplayName { get; set; }
    public string UniqueId { get; set; }
    public int TheaterSlot { get; set; }
    public bool IsTestTheater { get; set; }
    public string RequiredEventFlag { get; set; }
    public string Description { get; set; }
    public string ThreatDisplayName { get; set; }
    public FortTheaterRuntimeData RuntimeInfo { get; set; }
    public List<FortTheaterMapTileData> Tiles { get; set; } = new List<FortTheaterMapTileData>();
    public List<FortTheaterMapRegionData> Regions { get; set; } = new List<FortTheaterMapRegionData>();
}
