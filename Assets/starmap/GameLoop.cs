using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class GameLoop : MonoBehaviour
{

    void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("generalUIScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene("eventScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        // check for simulation scene setup (if running simulation scene in editor)s
        GameObject obj = GameObject.Find("Debug");
        if (obj)
        {
            TradeNetVisualisation visual = obj.GetComponent<TradeNetVisualisation>();
            if (visual) GameState.requestState(GameState.State.Simulation);
        }
    }
    void Start()
    {
        Debug.Log ("*** GameLoop Start ***");
        //GameState.requestState(GameState.State.Starmap); // TODO from menu when ready. Starmap is already default state
        Simulation.StarmapVisualization.initNPCShipVisuals();
    }

    void Update()
    {
        // drive game ticks
        if ((GameState.hasState(GameState.State.Pause) == false) && 
            (GameState.isState(GameState.State.Starmap) || GameState.isState(GameState.State.Simulation)))
        {
            Root.game.tick(Time.deltaTime * Simulation.Parameters.gameSpeed);
        }
        if (Input.GetButtonDown("Pause") || Input.GetKeyDown("pause"))
        {
            if (GameState.hasState(GameState.State.Location) == false && GameState.hasState(GameState.State.MainMenu) == false && GameState.hasState(GameState.State.Event) == false)
            {
                Simulation.StarmapVisualization.Ui.setPauseText(GameState.playerPause());
            }
        }
    }

}
