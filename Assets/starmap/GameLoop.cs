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
        if (GameState.isState(GameState.State.Starmap) || GameState.isState(GameState.State.Simulation))
        {
            Root.game.tick(Time.deltaTime * Simulation.Parameters.gameSpeed);
        }
    }

}
