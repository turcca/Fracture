using UnityEngine;
using System.Collections;

public class EventRoot : MonoBehaviour
{
    public GameObject eventUI;

    void Awake()
    {
        NGUITools.SetActive(eventUI, false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
