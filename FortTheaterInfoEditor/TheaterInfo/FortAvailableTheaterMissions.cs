using System.Collections.Generic;
using System;

public class FortAvailableTheaterMissions
{
    public string TheaterId { get; set; }
    public List<FortAvailableMissionData> AvailableMissions { get; set; } = new List<FortAvailableMissionData>();
    public DateTime NextRefresh { get; set; }
}
