using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static public class GameState
{
    public enum State { MainMenu, Event, Starmap, Location };
    
    private static State currentState = State.Starmap;

    static public bool requestState(State s)
    {
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
}
