using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterInfo : MonoBehaviour
{
    public Character character;
    public CharacterImageSmall image;
    public Text nameText;

    public void initialise(Character c)
    {
        character = c;
        image.initialise(character.portrait.id);
        nameText.text = c.name;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
