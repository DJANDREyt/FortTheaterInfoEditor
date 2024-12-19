using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;


public class FortTheaterRuntimeData
{
    public FortTheaterType TheaterType { get; set; }

    public GameplayTagContainer TheaterTags { get; set; }
    public FortRequirementsInfo TheaterVisibilityRequirements { get; set; }
    public FortRequirementsInfo Requirements { get; set; }
    public bool OnlyMatchLinkedQuestsToTiles { get; set; }
    public string WorldMapPinClass { get; set; }
    public string TheaterImage { get; set; }
    public FortMultiSizeBrush TheaterImages { get; set; }
    public FortTheaterColorInfo TheaterColorInfo { get; set; }
    public string Socket { get; set; }
    public FortRequirementsInfo MissionAlertRequirements { get; set; }
    public List<FortMissionAlertRuntimeData> MissionAlertCategoryRequirements { get; set; } = new List<FortMissionAlertRuntimeData>();
    public float HighestDifficulty { get; set; }
}
