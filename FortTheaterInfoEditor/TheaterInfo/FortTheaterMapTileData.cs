using System.Collections.Generic;

public class FortTheaterMapTileData
{
    public FortTheaterMapTileType TileType { get; set; }
    public string ZoneTheme { get; set; }
    public FortRequirementsInfo Requirements { get; set; }
    public List<FortLinkedQuest> LinkedQuests { get; set; } = new List<FortLinkedQuest>();
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public List<FortTheaterMissionWeight> MissionWeightOverrides { get; set; } = new List<FortTheaterMissionWeight>();
    public List<FortTheaterDifficultyWeight> DifficultyWeightOverrides { get; set; } = new List<FortTheaterDifficultyWeight>();
    public bool CanBeMissionAlert { get; set; }
    public GameplayTagContainer TileTags { get; set; }
}
