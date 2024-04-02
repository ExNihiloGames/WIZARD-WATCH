using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIDHelper : MonoBehaviour
{
    public Dictionary<string, string> GenerateIDScroll()
    {
        Dictionary<string, string> idScrollDict = new Dictionary<string, string>()
        {
            {"FIRSTNAME", "JON" },
            {"LASTNAME", "HATAN" },
            {"TITLE", "GRAND MAITRE" },
            {"AGE", "23" },
            {"FACTION" ,"EMPIRE GALACTIQUE"}
        };

        return idScrollDict;
    }

    public Dictionary<string, int> GenerateHeroStats(HeroDifficulty heroDifficulty) 
    {
        Dictionary<string, int> heroStatsDict = new Dictionary<string, int>()
        {
            {"MAXHP", 10},
            {"MAXSPEED", 5},
            {"MANAWHENSLAIN", 20}
        };

        return heroStatsDict;
    }
}
