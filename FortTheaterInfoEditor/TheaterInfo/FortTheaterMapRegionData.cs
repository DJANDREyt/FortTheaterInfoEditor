using System.Collections.Generic;

public class FortTheaterMapRegionData
{
    public string DisplayName { get; set; }
    public GameplayTagContainer RegionTags { get; set; }
    public List<int> TileIndices { get; set; } = new List<int>();
    public string RegionThemeIcon { get; set; }
    public FortTheaterMapMissionData MissionData { get; set; }
    public FortRequirementsInfo Requirements { get; set; }
}