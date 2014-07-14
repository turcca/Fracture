using UnityEngine;
using System.Collections;

public class UiLocationMain : MonoBehaviour
{
    GameObject tradeWindow;
    GameObject mainWindow;
    GameObject diplomacyWindow;

    void Awake()
    {
        mainWindow = GameObject.Find("UILocationMenu");
        tradeWindow = GameObject.Find("UItrade");
        diplomacyWindow = GameObject.Find ("UIDiplomacyMenu");
        mainWindow.GetComponent<UIPanel>().enabled = false;
        tradeWindow.GetComponent<UIPanel>().enabled = false;
        diplomacyWindow.GetComponent<UIPanel>().enabled = false;
    }

    // Use this for initialization
    void Start()
    {
    }

    public void go()
    {
        mainWindow.GetComponent<WindowEffects>().show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void msgButtonExit()
    {
        mainWindow.GetComponent<WindowEffects>().hide(new EventDelegate(exit));
        tradeWindow.GetComponent<WindowEffects>().hide(null);
    }

    void exit()
    {
        Application.LoadLevel(0);
    }

    public void msgButtonTrade()
    {
        if (!tradeWindow.GetComponent<UIPanel>().enabled)
        {
            //diplomacyWindow.GetComponent<WindowEffects>().hide(null);
            tradeWindow.GetComponent<WindowEffects>().show();
        }
        else
        {
            tradeWindow.GetComponent<WindowEffects>().hide(null);
        }
    }

    public void msgButtonDiplomacy()
    {
        if (!diplomacyWindow.GetComponent<UIPanel>().enabled)
        {
            //tradeWindow.GetComponent<WindowEffects>().hide(null);
            diplomacyWindow.GetComponent<WindowEffects>().show();
        }
        else
        {
            diplomacyWindow.GetComponent<WindowEffects>().hide(null);
        }
    }
}
