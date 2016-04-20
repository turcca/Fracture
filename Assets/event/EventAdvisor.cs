using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventAdvisor : MonoBehaviour
{
    public delegate void AdvisorSelectedDelegate(Character.Job job);
    public GameObject advicePanel;
    public Character.Job advisorJob;
    public Text adviceText;
    public GridLayoutGroup skills;
    public Text title;
    public Text characterName;

    private Character character;
    AdvisorSelectedDelegate callback;

    private List<GameObject> infos = new List<GameObject>();

    internal void setCharacter(Character setCharacter) { this.character = setCharacter; }
    public void setup()
    {
        if (this.gameObject.activeSelf != true) Debug.LogWarning("advisor "+advisorJob.ToString()+" is still inactive");
        // set idPanel/info (namePlate, job)
        if (title != null && characterName != null)
        {
            if (character != null)
            {
                title.text = Character.getJobName(advisorJob);
                characterName.text = character.name;
            }
            else Debug.LogError("character not set");
        }
        else Debug.LogError("title/characterName.text not assigned");

        // auto-select captain
        if (advisorJob == Character.Job.captain)
        {
            advicePanel.SetActive(true);
            this.gameObject.transform.parent.gameObject.GetComponent<AdvisorManager>().advisorSelected(this.gameObject);
        }
        else
        {
            advicePanel.SetActive(false);
        }
    }
    public void renameAdvisor(string newName)
    {
        character.name = newName;
        clearStats();
        showStats();
        // BUG: if you rename a character in 'inLocation' -event, the location advisors are loaded underneath, and the advisor's characterName is not changed while in the location
    }

    public void showAdvice(string advice)
    {
        adviceText.text = advice;
        // if >300 characters in advice, strech Text field closer to the edges of the AdviceBox
        if (adviceText.text.Length > 300) adviceText.rectTransform.sizeDelta = new Vector2(-40f, -20);
        else adviceText.rectTransform.sizeDelta = new Vector2(-80f, -20);
        advicePanel.SetActive(true);
        clearStats();
    }
    public void clearStats()
    {
        foreach (GameObject o in infos) Destroy(o);
        infos.Clear();
    }
    public void showStats()
    {
        if (character != null)
        {
            //adviceText.text = character.getCharacterInfo();
            adviceText.text = "";

            // populate infos/skills
            if (infos.Count == 0)
            {
                foreach (var skill in character.getCharacterInfoList())
                {
                    GameObject obj = new GameObject();
                    obj.transform.parent = skills.gameObject.transform;
                    obj.name = skill.Key;
                    // txt
                    Text txt = obj.AddComponent<Text>();
                    txt.text = skill.Key;
                    txt.font = adviceText.font;
                    // colours
                    if (skill.Key.StartsWith("Trait") || skill.Key.StartsWith("Name") || skill.Key.StartsWith("Affiliation") || skill.Key.StartsWith("Age")) txt.color = new Color32(110, 80, 20, 255);
                    else txt.color = new Color32(40, 40, 40, 255);
                    txt.resizeTextForBestFit = true;
                    txt.resizeTextMaxSize = 14;
                    txt.resizeTextMinSize = 10;
                    // tooltip
                    ToolTipScript skillTip = obj.AddComponent<ToolTipScript> ();
                    skillTip.toolTip = skill.Value;

                    infos.Add(obj);
                }
            }
            // adjust grid / text field
            if (infos.Count > 12)
            {
                skills.startAxis = GridLayoutGroup.Axis.Horizontal;
                skills.padding.top = 0; skills.padding.bottom = 0;
                skills.padding.left = 0; skills.padding.right = 0;
                skills.cellSize = new Vector2(180, 20);
                skills.spacing = new Vector2(5, 0);
            }
            else if (infos.Count > 8)
            {
                skills.startAxis = GridLayoutGroup.Axis.Horizontal;
                skills.padding.top = 10; skills.padding.bottom = 10;
                skills.padding.left = 0; skills.padding.right = 0;
                skills.cellSize = new Vector2(180, 20);
                skills.spacing = new Vector2(5, 1);
            }
            else if (infos.Count > 6)
            {
                skills.startAxis = GridLayoutGroup.Axis.Vertical;
                skills.padding.top = 5; skills.padding.bottom = 5;
                skills.padding.left = 40; skills.padding.right = 40;
                skills.cellSize = new Vector2(220, 20);
                skills.spacing = new Vector2(20, 1);
            }
            else if (infos.Count > 3)
            {
                skills.startAxis = GridLayoutGroup.Axis.Vertical;
                skills.padding.top = 10; skills.padding.bottom = 10;
                skills.padding.left = 40; skills.padding.right = 40;
                skills.cellSize = new Vector2(220, 20);
                skills.spacing = new Vector2(20, 4);
            }
            else
            {
                skills.startAxis = GridLayoutGroup.Axis.Vertical;
                skills.padding.top = 20; skills.padding.bottom = 20;
                skills.padding.left = 40; skills.padding.right = 40;
                skills.cellSize = new Vector2(220, 20);
                skills.spacing = new Vector2(20, 4);
            }
        }
        else
        {
            Debug.LogError("ERROR: advisor not linked to any character");
        }
    }

    public void hideAdvice()
    {
        advicePanel.SetActive(false);
        clearStats();
    }

}
