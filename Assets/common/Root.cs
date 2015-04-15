// use simulation flag to run simulation tests
#define SIMULATION

using UnityEngine;
using System.Collections;

static public class Root
{
    public enum State
    {
        MainMenu,
        Location,
        StarMap,
        Event
    }

    static public CharacterPortraitManager PortraitManager { get; private set; }
    static public Game game {get; private set;}
    static public GameUI ui { get; private set; }
    static public State state;

    static Root()
    {
        PortraitManager = new CharacterPortraitManager();
        ui = new GameUI();

        createNewGame();
    }

    static void createNewGame()
    {
#if !SIMULATION
        game = new Game();
        game.initLocations();
        game.initEvents();
        game.initNPCShips();
        game.initPlayer();
#else
        game = new Game();
        game.initLocations();
#endif

//        EventAdder.addAllEvents();
    }

    internal static void eventDone()
    {
        state = State.StarMap;
        ui.hideEventWindow();
    }
    internal static void startRandomStarMapEvent()
    {
        game.events.startRandomStarMapEvent(new EventManager.AllDoneDelegate(eventDone));
        ui.showEventWindow();
        state = State.Event;
    }
}
