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
        character = Root.game.player.getAdvisor(job);
        if (character != Character.Empty)
        {
            characterImage.sprite = Root.PortraitManager.getPortraitSprite(character.portrait.id);
            characterImage.enabled = true;
        }
        else
        {
            characterImage.enabled = false;
        }
    }

    void updateView()
    {
        setImage();
    }
}
