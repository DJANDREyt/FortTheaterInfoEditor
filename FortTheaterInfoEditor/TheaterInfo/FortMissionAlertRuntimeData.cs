public class FortMissionAlertRuntimeData
{
    public FortMissionAlertCategory MissionAlertCategory { get; set; }
    public bool RespectTileRequirements { get; set; }
    public bool AllowQuickplay { get; set; }
}

public enum FortMissionAlertCategory
{
    General = 0,
    Storm = 1,
    Horde = 2,
    StormLow = 3,
    Halloween = 4,
    Total = 5
}
