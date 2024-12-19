using System.Collections.Generic;

public class FortTheaterMapMissionData
{
    public List<FortTheaterMissionWeight> MissionWeights { get; set; }
    public List<FortTheaterDifficultyWeight> DifficultyWeights { get; set; }
    public int NumMissionsAvailable { get; set; }
    public int NumMissionsToChange { get; set; }
    public float MissionChangeFrequency { get; set; }
}
