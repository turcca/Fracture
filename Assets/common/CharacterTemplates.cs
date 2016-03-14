using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CharacterTemplates
{
    static Dictionary<string, Character> templateNames = new Dictionary<string, Character>();

    static CharacterTemplates()
    {
        compileTemplates();
    }

    public static Character getCharacter(string templateName = null)
    {
        if (templateName == null) return new Character();

        // find template
        if (templateNames.ContainsKey(templateName))
        {
            return new Character(templateNames[templateName]);
        }

        Debug.LogError("WARNING: template not found: '" + templateName + "'");
        return new Character();
    }


    // --------------- TEMPLATES -----------------
    static void compileTemplates()
    {
        string name = "";


        // Template ------------------->
        Character character = new Character();
        character.clearStats();
        name = "starting captain";
        character.isMale = true;

        // portrait tag: captain

        // assigned stats
        character.setStat(Character.Stat.age, 40f);
        character.setStat(Character.Stat.insanity, 0f);
        character.setStat(Character.Stat.leadership, 90f);
        character.setStat(Character.Stat.loyalty, 100f);

        if (!templateNames.ContainsKey(name)) templateNames.Add(name, character);
        else Debug.LogError("Character templateNames already include template named: '" + name + "'");
        character = null;
        // <------------------------------------------------ end template



        // Template ------------------->
        character = new Character();
        character.clearStats();
        name = "starting navigator";
        character.isMale = true;
        // portrait tag: navigator
        // character.navigator = true; //? from old system
        // character.psycher = true; //? from old system

        // assigned stats
        character.ideology = Faction.IdeologyID.navigators;
        character.setStat(Character.Stat.age, 62f);
        character.setStat(Character.Stat.navigation, 50f);
        character.setStat(Character.Stat.insanity, 10f);
        character.setStat(Character.Stat.loyalty, 92f);

        if (!templateNames.ContainsKey(name)) templateNames.Add(name, character);
        else Debug.LogError("Character templateNames already include template named: '" + name + "'");
        character = null;
        // <------------------------------------------------ end template



        // Template ------------------->
        character = new Character();
        character.clearStats();
        name = "brotherhood";
        character.isMale = false;

        // assigned stats
        character.ideology = Faction.IdeologyID.brotherhood;
        character.setStat(Character.Stat.insanity, Mathf.Pow(Random.Range(0f, 3.16f), 4f));

        if (!templateNames.ContainsKey(name)) templateNames.Add(name, character);
        else Debug.LogError("Character templateNames already include template named: '" + name + "'");
        character = null;
        // <------------------------------------------------ end template



        // Template ------------------->
        character = new Character();
        character.clearStats();
        name = "demo engineer";
        character.isMale = true;

        // assigned stats

        if (!templateNames.ContainsKey(name)) templateNames.Add(name, character);
        else Debug.LogError("Character templateNames already include template named: '" + name + "'");
        character = null;
        // <------------------------------------------------ end template



        // Template ------------------->
        character = new Character();
        character.clearStats();
        name = "demo security";
        character.isMale = true;

        // assigned stats


        if (!templateNames.ContainsKey(name)) templateNames.Add(name, character);
        else Debug.LogError("Character templateNames already include template named: '" + name + "'");
        character = null;
        // <------------------------------------------------ end template



        // Template ------------------->
        character = new Character();
        character.clearStats();
        name = "demo quartermaster";
        character.isMale = true;

        // assigned stats


        if (!templateNames.ContainsKey(name)) templateNames.Add(name, character);
        else Debug.LogError("Character templateNames already include template named: '" + name + "'");
        character = null;
        // <------------------------------------------------ end template


        //// Template ------------------->
        //character = new Character();
        //character.clearStats();
        //name = "";

        //// assigned stats

        ////character.characterTrait =

        //if (!templateNames.ContainsKey(name)) templateNames.Add(name, character);
        //else Debug.LogError("Character templateNames already include template named: '" + name + "'");
        //character = null;
        //// <------------------------------------------------ end template


    }
}
