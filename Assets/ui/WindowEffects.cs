using UnityEngine;
using System.Collections;

public class WindowEffects : MonoBehaviour
{
    //EventDelegate endCallback;
    //// Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public void show()
    //{
    //    gameObject.GetComponent<UIPanel>().enabled = true;
    //    //gameObject.GetComponent<TweenPosition>().enabled = true;
    //    gameObject.GetComponent<TweenPosition>().PlayForward();
    //    gameObject.GetComponent<TweenPosition>().ResetToBeginning();
    //    gameObject.GetComponent<TweenPosition>().PlayForward();
    //    //gameObject.GetComponent<TweenAlpha>().enabled = true;
    //    gameObject.GetComponent<TweenAlpha>().PlayForward();
    //    gameObject.GetComponent<TweenAlpha>().ResetToBeginning();
    //    gameObject.GetComponent<TweenAlpha>().PlayForward();
    //}

    //public void hide(EventDelegate _endCallback)
    //{
    //    endCallback = _endCallback;
    //    gameObject.GetComponent<TweenPosition>().PlayReverse();
    //    gameObject.GetComponent<TweenAlpha>().PlayReverse();
    //    gameObject.GetComponent<TweenPosition>().onFinished.Clear();
    //    gameObject.GetComponent<TweenPosition>().onFinished.Add(new EventDelegate(afterHide));
    //}

    //void afterHide()
    //{
    //    gameObject.GetComponent<TweenPosition>().onFinished.Clear();
    //    if (endCallback != null)
    //    {
    //        endCallback.Execute();
    //        endCallback.Clear();
    //    }
    //    gameObject.GetComponent<UIPanel>().enabled = false;
    //}
}
