using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulationTestSetup : MonoBehaviour
{
    public float speedMul = 1.5f; // /2.0f = default
    private float elapsedDays = 0.0f;

    void Start()
    {
        GameState.requestState(GameState.State.Simulation);
    }

    void Update()
    {
        float tick = Time.deltaTime * Mathf.Max(speedMul, 0.0f);
        Root.game.tick(tick);
        elapsedDays += tick;
        if (tick > 1.0f) Debug.LogWarning("Tick length: " + Mathf.Round(tick *100)/100);
    }
    public int getElapsedDays()
    {
        return (int)elapsedDays;
    }
}
