using UnityEngine;
using System.Collections;

public class NavpointId : MonoBehaviour
{
    static int idCount = 0;
    private int id = -1;

    public string getId()
    {
        if (id == -1)
        {
            ++idCount;
            id = idCount;
        }
        return id.ToString();
    }
}
