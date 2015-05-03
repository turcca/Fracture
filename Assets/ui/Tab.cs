using UnityEngine;
using System.Collections;

public class Tab : MonoBehaviour
{
    bool active = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        active = !active;
        if (active)
        {
            gameObject.transform.FindChild("Selected").gameObject.SetActive(true);
            gameObject.transform.FindChild("NotSelected").gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.FindChild("Selected").gameObject.SetActive(false);
            gameObject.transform.FindChild("NotSelected").gameObject.SetActive(true);
        }
    }
}
