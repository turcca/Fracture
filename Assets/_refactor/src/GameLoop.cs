using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    void Awake()
    {
        Application.LoadLevelAdditive("ui");
    }

    float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1.0f)
        {
            Universe.singleton.tick(1.0f);
            time -= 1.0f;
        }
    }
}
