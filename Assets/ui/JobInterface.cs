using UnityEngine;
using System.Collections;

public class JobInterface : MonoBehaviour
{
    public Character.Job job;
    private GameObject characterDialog;

    // Use this for initialization
    void Start()
    {
        characterDialog = 
            GameObject.Find("GameCanvas").GetComponent<GameMenuSystem>().characterSelectDialog;
        characterDialog.GetComponent<CharacterSelectDialog>().initilise(characterSelected);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReAssignJob()
    {
        characterDialog.SetActive(true);
    }

    void characterSelected(bool ok, int id)
    {
        Tools.debug("wohoo!");
        characterDialog.SetActive(false);
    }
}
