using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectDialog : MonoBehaviour
{
    public delegate void SelectionDoneCallback(bool ok, int id);
    SelectionDoneCallback callback;

    public GameObject characterInfoPrefab;
    public GameObject characterGrid;
    public GameObject characterGridPanel;

    private Character character;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void initilise(SelectionDoneCallback cb)
    {
        callback = cb;

        for (int i = characterGrid.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.DestroyImmediate(characterGrid.transform.GetChild(i).gameObject);
        }

        Character[] chars = Game.universe.player.getCharacters();
        foreach (Character c in chars)
        {
            createCharInfo(c);
        }

        ((RectTransform)characterGrid.transform).sizeDelta =
            new Vector2(580, 150 * chars.Length);
    }

    private void createCharInfo(Character c)
    {
        GameObject characterInfo = (GameObject)GameObject.Instantiate(characterInfoPrefab);
        characterInfo.GetComponent<CharacterInfo>().initialise(c);

        characterInfo.transform.parent = characterGrid.transform;
        characterInfo.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => characterSelected(c)));
    }

    public void characterSelected(Character c)
    {
        character = c;
    }

    public void onClickOK()
    {
        callback(true, character.id);
    }

    public void onClickCancel()
    {
        callback(false, 0);
    }
}
