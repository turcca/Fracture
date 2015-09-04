using UnityEngine;
using System.Collections;

public class PerFrameRaycast : MonoBehaviour 
{
    public GameObject target;
    RaycastHit hitInfo;

    
    void Update () 
    {
        // Cast a ray to find out the end point of the laser
        hitInfo = new RaycastHit ();
        Physics.Raycast (this.gameObject.transform.position, target.gameObject.transform.position, out hitInfo, 100.0f);
    }
    
    public RaycastHit GetHitInfo () 
    {
        return hitInfo;
    }
}
