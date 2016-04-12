using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CharacterPortraitManager
{
    public class PortraitItem
    {
        public string file = "";
        public string tag = "";     // specially tagged portraits, such as "navigator", can be reserved for those characters
        public int id = -1;

        public PortraitItem(Sprite sprite, int id)//string _file, string _tag, int _id)
        {
            file = sprite.name;
            //@todo parse tags from file name
            Debug.Log("todo: parse tags from " + file);
            //tag = _tag;
            this.id = id;
        }
    }

    private Dictionary<int, PortraitItem> portraits = new Dictionary<int, PortraitItem>();

    public CharacterPortraitManager()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("/ui/portraits");
        //string[] portraitFiles = Directory.GetFiles("Assets/Resources/ui/portraits", "portrait*.png");
        int id = 0;
        //foreach (string file in portraitFiles)
        //{
        //    ++id;
        //    portraits.Add(id, new PortraitItem(Path.GetFileNameWithoutExtension(file), "", id));
        //}
        foreach (Sprite sprite in sprites)
        {
            ++id;
            portraits.Add(id, new PortraitItem(sprite, id));
        }
    }

    public Character.Portrait getPortrait(string tag)
    {
        Dictionary<int, int> takenIds = new Dictionary<int,int>();
        foreach (Character c in Root.game.player.getCharacters())
        {
            int tryId = c.portrait.id;
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
        foreach (PortraitItem entry in portraits.Values)
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

    //public string getPortraitImage(int id)
    //{
    //    if (portraits.ContainsKey(id))
    //        return portraits[id].file;
    //    else
    //    {
    //        Debug.LogError("invalid portrait id requested: " + id);
    //        return portraits.First().Value.file;
    //    }
    //}

    //public Texture getPortraitTexture(int id)
    //{
    //    if (portraits.ContainsKey(id))
    //        return Resources.Load<Texture2D>("ui/portraits/" + portraits[id].file);
    //    else
    //    {
    //        Debug.LogError("invalid portrait id requested: " + id);
    //        return Resources.Load<Texture2D>("ui/portraits/" + portraits.First().Value.file);
    //    }
    //}

    public Sprite getPortraitSprite(int id)
    {
        if (portraits.ContainsKey(id))
            return Resources.Load<Sprite>("ui/portraits/" + portraits[id].file);
        else
        {
            Debug.LogError("invalid portrait id requested: " + id);
            return Resources.Load<Sprite>("ui/portraits/" + portraits.First().Value.file);
        }
    }
}
