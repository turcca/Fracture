using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterPortrait : MonoBehaviour
{
    public int characterId;
    public Image characterImage;

    private Character character;

    // Use this for initialization
    void Start()
    {
        character = Root.game.player.getCharacter(characterId);
        setImage();
    }

    private void setImage()
    {
        characterImage.sprite = Root.PortraitManager.getPortraitSprite(character.portrait.id);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
