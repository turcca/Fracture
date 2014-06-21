using System;
using System.Collections.Generic;
using UnityEngine;

static class Tools
{
    static public string STRING_NOT_ASSIGNED = "Not assigned!";
    static public int INT_NOT_ASSIGNED = 999999;

    static public KeyCode getKeyAlpha(int i)
    {
        if (i == 1) return KeyCode.Alpha1;
        if (i == 2) return KeyCode.Alpha2;
        if (i == 3) return KeyCode.Alpha3;
        if (i == 4) return KeyCode.Alpha4;
        if (i == 5) return KeyCode.Alpha5;
        else
        {
            Debug.LogError("ERROR: over 5 choices - need to expand key bindings...");
            return KeyCode.Alpha6;
        }
    }

    static public KeyCode getKeyFunction(int i)
    {
        if (i == 1) return KeyCode.F1;
        if (i == 2) return KeyCode.F2;
        if (i == 3) return KeyCode.F3;
        if (i == 4) return KeyCode.F4;
        if (i == 5) return KeyCode.F5;
        if (i == 6) return KeyCode.F6;
        else
        {
            Debug.LogError("ERROR: over 6 choices - need to expand key bindings...");
            return KeyCode.F7;
        }
    }

}
