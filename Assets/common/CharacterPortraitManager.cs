using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CharacterPortraitManager
{
    public class PortraitEntry
    {
        public string file = "";
        public string tag = "";     // specially tagged portraits, such as "navigator", can be reserved for those characters
        public int id = 0;

        public PortraitEntry(string _file, string _tag, int _id)
        {
            file = _file;
            tag = _tag;
            id = _id;
        }
    }

    private Dictionary<int, PortraitEntry> portraits = new Dictionary<int, PortraitEntry>();

    public CharacterPortraitManager()
    {
        string[] portraitFiles = Directory.GetFiles("Assets/Resources/ui/portraits", "portrait*.png");
        int id = 0;
        foreach (string file in portraitFiles)
        {
            ++id;
            portraits.Add(id, new PortraitEntry(Path.GetFileNameWithoutExtension(file), "", id));
        }
    }

    public Character.Portrait getPortrait(string tag)
    {
        Dictionary<int, int> takenIds = new Dictionary<int,int>();
        foreach (Character c in Game.getUniverse().player.getCharacters())
        {
            int tryId = c.getPortrait().id;
            if (takenIds.ContainsKey(tryId))
            {
                takenIds[tryId] += 1;
            }
            else
            {
                takenIds.Add(tryId, 1);
            }
        }

        Dictionary<int, int> allIds = new Dictionary<int, int>();
        foreach (PortraitEntry entry in portraits.Values)
        {
            allIds.Add(entry.id, 0);
            if (takenIds.ContainsKey(entry.id))
            {
                allIds[entry.id] += takenIds[entry.id];
            }
        }

        var sortedByUsage = from pair in allIds
                            orderby pair.Value ascending
                            select pair;

        int id = 0;
        foreach (KeyValuePair<int, int> entry in sortedByUsage)
        {
            if (tag == portraits[entry.Key].tag)
            {
                id = entry.Key;
                break;
            }
        }

        return new Character.Portrait(id, 0);
    }

    public string getPortraitImage(int id)
    {
        return portraits[id].file;
    }

    public Texture getPortraitTexture(int id)
    {
        return Resources.Load<Texture2D>("ui/portraits/" + portraits[id].file);
    }
}
