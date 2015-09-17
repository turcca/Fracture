// use simulation flag to run simulation tests
//#define SIMULATION

using UnityEngine;
using System.Collections;

static public class Root
{

    static public CharacterPortraitManager PortraitManager { get; private set; }
    static public Game game {get; private set;}
    static public GameUI ui { get; private set; }

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
        game.initNPCShips();
#endif

//        EventAdder.addAllEvents();
    }
}
