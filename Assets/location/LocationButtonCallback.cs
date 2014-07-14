using UnityEngine;
using System.Collections;

public class LocationButtonCallback : MonoBehaviour
{
    public delegate void CallbackDelegate(string param);
    public CallbackDelegate callback;
    public string param;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        callback(param);
    }
}
