using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    void Awake()
    {
        Application.LoadLevelAdditive("generalUIScene");
        Application.LoadLevelAdditive("eventScene");
    }
    void Start()
    {
        Debug.Log ("*** GameLoop Start ***");
        GameState.requestState(GameState.State.Starmap);
        Simulation.NPCShipVisualisation.initNPCShipVisuals();
    }

    void Update()
    {
        Root.game.tick(Time.deltaTime * Simulation.Parameters.gameSpeed);
    }

}
