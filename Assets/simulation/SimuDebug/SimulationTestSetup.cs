using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulationTestSetup : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        //Root.game.tick(Time.deltaTime / 5.0f);
        Root.game.tick(Time.deltaTime *1.5f);
    }
}
