using CUE4Parse.UE4.Versions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Settings
{
    public string LastOpenedLoadDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public string LastOpenedSaveDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public bool DarkMode { get; set; } = false;
    public EGame UEVersion { get; set; } = EGame.GAME_UE4_16;
    public string PaksDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public string Culture { get; set; } = "en";
    public bool LoadLastLoadedJsonOnLoad { get; set; } = false;
    public string LastLoadedJson { get; set; } = "none";
    public string AESKey { get; set; } = "0x0";
}
