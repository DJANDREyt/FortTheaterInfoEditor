using System;

public class FortAvailableMissionData
{
    public string MissionGuid { get; set; }
    public McpLootResult MissionRewards { get; set; }
    public McpLootResult BonusMissionRewards { get; set; }
    public string MissionGenerator { get; set; }  // SoftClassPtr<UClass>
    public DataTableRowHandle MissionDifficultyInfo { get; set; }
    public int TileIndex { get; set; }
    public DateTime AvailableUntil { get; set; }
}
