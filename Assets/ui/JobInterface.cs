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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReAssignJob()
    {
        characterDialog.GetComponent<CharacterSelectDialog>().initilise(characterSelected);
        characterDialog.SetActive(true);
    }

    void characterSelected(bool ok, int id)
    {
        Game.universe.player.setAdvisor(job, id);
        characterDialog.SetActive(false);
        SendMessageUpwards("notifyChange");
    }
}
