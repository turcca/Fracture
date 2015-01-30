using UnityEngine;
using System.Collections;

public class GameUI
{
    private bool isAdvisors = false;
    private bool isEvent = false;

    internal void showEventWindow()
    {
        SideWindow.get().showEvent();
        isEvent = true;
    }
    internal void hideEventWindow()
    {
        if (isAdvisors)
        {
            SideWindow.get().showAdvisors();
        }
        else
        {
            SideWindow.get().hide();
        }
        isEvent = false;
    }

    internal void showAdvisors()
    {
        if (!isEvent)
        {
            SideWindow.get().showAdvisors();
            isAdvisors = true;
        }
    }
    internal void hideAdvisors()
    {
        if (!isEvent)
        {
            SideWindow.get().hide();
            isAdvisors = false;
        }
    }

    internal void toggleAdvisors()
    {
        if (isAdvisors)
        {
            hideAdvisors();
        }
        else
        {
            showAdvisors();
        }
    }

    internal bool isEventWindow()
    {
        return isEvent;
    }
}
