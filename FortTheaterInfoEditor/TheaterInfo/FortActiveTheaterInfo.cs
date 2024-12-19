using System;
using System.Collections.Generic;

public class FortActiveTheaterInfo
{
    public List<FortTheaterMapData> Theaters { get; set; } = new List<FortTheaterMapData>();
    public List<FortAvailableTheaterMissions> Missions { get; set; } = new List<FortAvailableTheaterMissions>();
    public List<FortAvailableMissionAlerts> MissionAlerts { get; set; } = new List<FortAvailableMissionAlerts>();
}