using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static public class GameState
{
    public enum State { MainMenu, Event, Starmap, Location, Pause, Simulation };

    static private State currentState = State.Starmap;

    static public bool requestState(State s)
    {
        Debug.Log("[GameState <-- " + s.ToString() + "]");
        currentState = s;
        return true;
    }

    static public void returnFromState(State s)
    {
        currentState = State.Starmap;
    }

    static public State getState()
    {
        return currentState;
    }
    static public bool isState(State queryState)
    {
        if (currentState == queryState) return true;
        return false;
    }
}
