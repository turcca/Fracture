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
        character = Game.getUniverse().player.getCharacter(job);
        setImage();
    }

    private void setImage()
    {
        characterImage.sprite = Game.PortraitManager.getPortraitSprite(character.getPortrait().id);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
