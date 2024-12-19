using FortTheaterInfoEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Utils
{
    public static string GetTheaterNameById(string Id)
    {
        if (EditorWindow.TheaterInfo != null)
        {
            foreach (FortTheaterMapData theater in EditorWindow.TheaterInfo.Theaters)
            {
                if (theater.UniqueId == Id) return theater.DisplayName;
            }
        }

        return "Undefined";
    }
}
