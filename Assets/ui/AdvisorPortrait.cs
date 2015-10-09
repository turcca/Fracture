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
        //setImage(); // TODO: use setImage for dynamic portrait-assigning. Currently it just keeps the portraits assigned from the eventScene.unity & locationScene.unity
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
