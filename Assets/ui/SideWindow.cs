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
        animator = gameObject.GetComponent<Animator>();
    }
    public void showEvent()
    {
        animator.SetInteger("State", (int)State.Event);
    }
    public void showAdvisors()
    {
        animator.SetInteger("State", (int)State.Advisor);
    }
    public void hide()
    {
        animator.SetInteger("State", (int)State.Hidden);
        //AdvisorManager.get().hideAdvices();
    }

    public static SideWindow get()
    {
        return GameObject.Find("SideWindow").GetComponent<SideWindow>();
    }
}
