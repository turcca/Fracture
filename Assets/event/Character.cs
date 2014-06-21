using UnityEngine;
using System.Collections;

public class Character
{
    public int leadership = 0;
    public int diplomat = 0;
    public int corruption = 0;

    private string portraitId;

    static public string[] getSkillNames()
    {
        return new string[]
        {
            "diplomat",
            "etc"
        };
    }

    static public string[] getAdvisorPositionNames()
    {
        return new string[]
        {
            "captain",
            "navigator",
            "engineer",
            "security",
            "quartermaster",
            "psycher"
        };
    }

    public Character()
    {
        portraitId = "portrait_003a";
    }

    public string getPortraitId()
    {
        return portraitId;
    }

    public string getStats()
    {
        return "Return stats here.";
    }
}
