using System;
using System.Collections.Generic;

public class FortAvailableMissionAlertData
{
    public string CategoryName { get; set; }
    public string SpreadDataName { get; set; }
    public string MissionAlertGuid { get; set; }
    public int TileIndex { get; set; }
    public DateTime AvailableUntil { get; set; }
    public DateTime RefreshSpreadAt { get; set; }
    public McpLootResult MissionAlertRewards { get; set; }
    public McpLootResult MissionAlertModifiers { get; set; }
    public List<string> ItemDefinitionRefCache { get; set; } = new List<string>();
}
