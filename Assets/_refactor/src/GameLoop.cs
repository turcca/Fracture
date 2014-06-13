using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    public Universe universe;

    void Start()
    {
        universe = new Universe();
    }

    float time = 0;
    void Update()
    {
        
        time += Time.deltaTime;
        if (time > 1.0f)
        {
            universe.tick(1.0f);
            time -= 1.0f;
        }
    }
}
