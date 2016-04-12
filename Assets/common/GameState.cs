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

    static private bool playerPaused = false;


    static GameState()
    {
    }


    static public bool requestState(State s)
    {
        if (playerPaused) return false; // freeze state if playerPaused
        //currentState = s;
        stateStack.Push(s);
        Debug.Log("[GameState --> " + ToDebugString() + "]");
        return true;
    }

    static public void returnFromState(State state = State.None)
    {
        if (stateStack.Count == 0) { Debug.LogWarning("returnFromState("+state+")  while state stack was empty (Starmap)");  return; }

        State oldState = getState();
        if (state != State.None && state != stateStack.Peek())
            Debug.LogWarning("Tried to return from state: '" + state + "', but stateStack was\n" + ToDebugString());
        else
        {
            stateStack.Pop();
            Debug.Log("[GameState <-- " + ToDebugString() + "  (out: " + oldState + ") ]");
        }
    }
    /// <summary>
    /// Toggles manual pause/unpauses by the player and returns new playerPaused -value
    /// </summary>
    /// <returns></returns>
    static public bool playerPause()
    {
        playerPaused = !playerPaused;
        // mute/unmute master volume
        Mixer.Instance.pauseMaster(playerPaused);
        return playerPaused;
    }

    static public State getState()
    {
        return (playerPaused) ? State.Pause : (stateStack.Count > 0) ? stateStack.Peek() : State.Starmap;
    }
    static public bool isState(State queryState)
    {
        // pause is special case
        if (queryState == State.Pause) return hasState(queryState);

        return queryState == getState();
    }
    static public bool hasState(State queryState)
    {
        if (playerPaused && queryState == State.Pause) return true;
        return stateStack.Contains(queryState);
    }

    public static string ToDebugString()
    {
        string rv = "{ ";
        if (playerPaused) rv += " (Player Paused) / ";
        foreach (State state in stateStack)
        {
            rv += state.ToString() + " / ";
        }
        return rv + "Starmap }";
    }
}
