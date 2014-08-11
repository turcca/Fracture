using UnityEngine;
using System.Collections;

public class CharacterSelectDialog : MonoBehaviour
{
    public delegate void SelectionDoneCallback(bool ok, int id);
    SelectionDoneCallback callback;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void initilise(SelectionDoneCallback cb)
    {
        callback = cb;
    }

    public void onClickOK()
    {
        callback(true, 0);
    }

    public void onClickCancel()
    {
        callback(false, 0);
    }
}
