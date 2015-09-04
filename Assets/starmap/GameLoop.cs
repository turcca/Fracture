using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    void Awake()
    {
        Application.LoadLevelAdditive("generalUIScene");
    }
    void Start()
    {
        Debug.Log ("GameLoop Start()");
        Simulation.NPCShipVisualisation.initNPCShipVisuals();
    }

    float time = 0;
    void Update()
    {
        Root.game.tick(Time.deltaTime/5.0f);
        //time += Time.deltaTime;
        //if (time > 1.0f)
        //{
        //    Game.universe.tick(1.0f);
        //    time -= 1.0f;
        //}
    }
}
