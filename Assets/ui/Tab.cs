using UnityEngine;
using System.Collections;

public class Tab : MonoBehaviour
{
    public void activate()
    {
        gameObject.transform.FindChild("Selected").gameObject.SetActive(true);
        gameObject.transform.FindChild("NotSelected").gameObject.SetActive(false);
    }

    public void deactivate()
    {
        gameObject.transform.FindChild("Selected").gameObject.SetActive(false);
        gameObject.transform.FindChild("NotSelected").gameObject.SetActive(true);
    }
}
