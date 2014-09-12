using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvisorPortrait : MonoBehaviour
{
    public Character.Job job;
    public Image characterImage;

    private Character character;

    // Use this for initialization
    void Start()
    {
        setImage();
    }

    private void setImage()
    {
        character = Game.universe.player.getCharacter(job);
        if (character != Character.Empty)
        {
            characterImage.sprite = Game.PortraitManager.getPortraitSprite(character.portrait.id);
            characterImage.enabled = true;
        }
        else
        {
            characterImage.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateView()
    {
        setImage();
    }
}
