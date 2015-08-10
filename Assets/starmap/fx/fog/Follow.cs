using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public GameObject target;
    Renderer[] rend;

    // Use this for initialization
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position =
            new Vector3(target.transform.position.x, gameObject.transform.position.y, target.transform.position.z);

        rend = GetComponentsInChildren<Renderer>(); 
        foreach (Renderer r in rend)
        {
            r.material.SetVector("_WorldPosition", transform.position);
        //rend.material.SetColor("Color", Color.blue);
        }
    }
}
