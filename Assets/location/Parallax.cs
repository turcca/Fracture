using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallax : MonoBehaviour
{
    List<Transform> layers = new List<Transform>();

    // Use this for initialization
    void Awake()
    {
        // front to back
        layers.Add(gameObject.transform.FindChild("fg"));
        layers.Add(gameObject.transform.FindChild("mg"));
        layers.Add(gameObject.transform.FindChild("bg"));
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        for (int i=0; i<layers.Count; i++)
        {
//            layers[i].position = new Vector3((int)(-300.0f * i * x/1920.0f), 0, 0);
            layers[i].localPosition = new Vector3((int)(-100.0f * i * x), 0, 0);
        }
    }
}
