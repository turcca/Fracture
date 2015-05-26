using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Backgrounds : MonoBehaviour
{
    Transform trade;
    Transform diplomacy;
    Transform midRight;

//    TweenPosition tween;

    Transform from;
    Transform to;

    void Awake()
    {
        trade = GameObject.Find("bg_trade").transform;
        diplomacy = GameObject.Find("bg_diplomacy").transform;
        midRight = GameObject.Find("bg_mid_right").transform;

//        tween = GetComponent<TweenPosition>();
//        tween.onFinished.Add(new EventDelegate(done));

        to = null;
        from = null;
    }
    void Start()
    {
//        NGUITools.SetActive(trade.gameObject, false);
//        NGUITools.SetActive(diplomacy.gameObject, false);
//        NGUITools.SetActive(midRight.gameObject, false);
    }
    void Update()
    {
    }

    private void setTarget(Transform t)
    {
        int delta = 0;
        from = to;

        if (from)
        {
            from.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            delta = 300;
//            NGUITools.SetActive(midRight.gameObject, true);
            midRight.localPosition = new Vector3(delta, 0, 0);
        }

        to = t;
        if (to)
            to.localPosition = new Vector3(1920 + delta, 0, 0);

//        tween.to = new Vector3(-1920 - delta, 0, 0);
//        tween.ResetToBeginning();
//        tween.PlayForward();
    }

    public void done()
    {
//        if (from)
//            NGUITools.SetActive(from.gameObject, false);
//        else
//            NGUITools.SetActive(midRight.gameObject, false);
        Debug.Log("done done");
    }

    public void gotoTrade()
    {
        if (to == trade)
            return;
//        NGUITools.SetActive(trade.gameObject, true);
        setTarget(trade);
    }

    public void gotoDiplomacy()
    {
        if (to == diplomacy)
            return;
//        NGUITools.SetActive(diplomacy.gameObject, true);
        setTarget(diplomacy);
    }

    public void gotoSpace()
    {
        setTarget(null);
    }
}
