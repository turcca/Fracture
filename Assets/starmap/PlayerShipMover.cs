using UnityEngine;
using System.Collections;

public class PlayerShipMover : MonoBehaviour
{
    Vector3 target = new Vector3(0, 0, 0);
    float speed = 8.0f;
    float rotationSpeed = 2.0f;

    // Use this for initialization
    void Start()
    {
        target = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target - gameObject.transform.position).normalized * Time.deltaTime * speed;
        if (dir.sqrMagnitude < (target - gameObject.transform.position).sqrMagnitude)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, Mathf.Min(speed, (target - transform.position).magnitude) * Time.deltaTime));
        }
    }

    public void setTarget(Vector3 pos)
    {
        target = pos;
    }
}
