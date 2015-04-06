using UnityEngine;
using System.Collections;

static public class Game
{
    public enum State
    {
        MainMenu,
        Location,
        StarMap,
        Event
    }

    static public CharacterPortraitManager PortraitManager { get; private set; }
    static public Universe universe {get; private set;}
    static public GameUI ui { get; private set; }
    static public State state;

    static Game()
    {
        PortraitManager = new CharacterPortraitManager();
        ui = new GameUI();

        createNewGame();
    }

    static void createNewGame()
    {
        universe = new Universe();
        universe.initLocations();
        universe.initNPCShips();
        universe.initPlayer();

//        EventAdder.addAllEvents();
    }

    internal static void eventDone()
    {
        state = State.StarMap;
        ui.hideEventWindow();
    }
    internal static void startRandomStarMapEvent()
    {
        universe.eventManager.startRandomStarMapEvent(new EventManager.AllDoneDelegate(eventDone));
        ui.showEventWindow();
        state = State.Event;
    }
}
