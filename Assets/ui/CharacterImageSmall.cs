using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterImageSmall : MonoBehaviour
{
    public int characterId;
    public Image image;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void initialise(int c)
    {
        characterId = c;
        image.sprite = Game.PortraitManager.getPortraitSprite(characterId);
    }
}
