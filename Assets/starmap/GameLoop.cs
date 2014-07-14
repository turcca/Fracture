using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    void Awake()
    {
        Application.LoadLevelAdditive("uiScene");
    }

    float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1.0f)
        {
            Game.getUniverse().tick(1.0f);
            time -= 1.0f;
        }
    }
}
