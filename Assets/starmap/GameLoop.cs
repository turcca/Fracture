using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    void Awake()
    {
        //Application.LoadLevelAdditive("uiScene");
    }

    float time = 0;
    void Update()
    {
        Game.universe.tick(Time.deltaTime*30.0f);
        //time += Time.deltaTime;
        //if (time > 1.0f)
        //{
        //    Game.universe.tick(1.0f);
        //    time -= 1.0f;
        //}
    }
}
