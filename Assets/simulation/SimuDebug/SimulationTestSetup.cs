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
        Root.game.tick(Time.deltaTime / 10.0f);
    }
}
