using UnityEngine;
using System.Collections;

static public class Game
{
    static public CharacterPortraitManager PortraitManager { get; private set; }
    static public Universe universe;

    static Game()
    {
        PortraitManager = new CharacterPortraitManager();
        createNewGame();
    }

    static void createNewGame()
    {
        universe = new Universe();
        universe.initLocations();
        universe.initPlayer();

        EventAdder.addAllEvents();
    }

    static public Universe getUniverse()
    {
        return universe;
    }
}
