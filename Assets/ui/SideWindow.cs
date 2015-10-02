using UnityEngine;
using System.Collections;

public class SideWindow : MonoBehaviour
{
    private Animator animator;

    enum State : int
    {
        Hidden = 0,
        Advisor = 1,
        Event = 2
    }

    void Awake()
    {
        //animator = gameObject.GetComponent<Animator>();
		hide();
    }
    public void showEvent()
    {
        //animator.SetInteger("State", (int)State.Event);
        if (gameObject != null)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
	}
    public void showAdvisors()
    {
        //animator.SetInteger("State", (int)State.Advisor);
    }
    public void hide()
    {
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
		//animator.SetInteger("State", (int)State.Hidden);
        //AdvisorManager.get().hideAdvices();
    }

    public static SideWindow get()
    {
        GameObject sideObj = GameObject.Find("SideWindow");
        SideWindow side = null;
        if (sideObj)
        {
            side = sideObj.GetComponent<SideWindow>();
            if (side == null) Debug.LogError("ERROR: SideWindow was not found");
        }
        return side;
    }
}
