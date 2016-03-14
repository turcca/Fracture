using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// state is a stack, and empty will return 'State.Starmap'
/// (fixed bottom condition)
/// </summary>
static public class GameState
{
    public enum State { None, MainMenu, Event, Starmap, Location, Pause, Simulation };

    static private Stack<State> stateStack = new Stack<State>();

    //static private State currentState = State.Starmap;


    static GameState()
    {
    }


    static public bool requestState(State s)
    {
        //currentState = s;
        stateStack.Push(s);
        Debug.Log("[GameState --> " + ToDebugString() + "]");
        return true;
    }

    static public void returnFromState(State state = State.None)
    {
        if (stateStack.Count == 0) { Debug.LogWarning("returnFromState("+state+")  while stae stack was empty (Starmap)");  return; }

        State oldState = getState();
        if (state != State.None && state != stateStack.Peek())
        {
            Debug.LogWarning("OBSERVE: returning from state: '" + state + "', but stateStack was\n" + ToDebugString());
        }
        stateStack.Pop();
        Debug.Log("[GameState <-- " + ToDebugString() + "  (out: "+oldState+") ]");
    }

    static public State getState()
    {
        return (stateStack.Count > 0) ? stateStack.Peek() : State.Starmap;
    }
    static public bool isState(State queryState)
    {
        // pause is special case
        if (queryState == State.Pause) return hasState(queryState);

        return queryState == getState();
    }
    static public bool hasState(State queryState)
    {
        return stateStack.Contains(queryState);
    }

    public static string ToDebugString()
    {
        string rv = "{ ";
        foreach (State state in stateStack)
        {
            rv += state.ToString() + " / ";
        }
        return rv + "Starmap }";
    }
}
