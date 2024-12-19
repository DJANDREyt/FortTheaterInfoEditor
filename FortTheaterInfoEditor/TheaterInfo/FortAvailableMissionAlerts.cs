using System;
using System.Collections.Generic;

public class FortAvailableMissionAlerts
{
    public string TheaterId { get; set; }
    public List<FortAvailableMissionAlertData> AvailableMissionAlerts { get; set; } = new List<FortAvailableMissionAlertData>();
    public DateTime NextRefresh { get; set; }
}
