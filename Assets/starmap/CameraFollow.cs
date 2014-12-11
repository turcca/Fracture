using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;

    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    void Start()
    {
        ///@todo set camera starting position
    }

    void LateUpdate()
    {
        if (!target)
            return;

        // Calculate the current rotation angles
        var wantedRotationAngle = 0;
        var wantedHeight = target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);

        // vvv old follow behaviour 

        //if (!target)
        //    return;

        //// Calculate the current rotation angles
        //var wantedRotationAngle = target.eulerAngles.y;
        //var wantedHeight = target.position.y + height;

        //var currentRotationAngle = transform.eulerAngles.y;
        //var currentHeight = transform.position.y;

        //// Damp the rotation around the y-axis
        //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        //// Damp the height
        //currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        //// Convert the angle into a rotation
        //var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        //// Set the position of the camera on the x-z plane to:
        //// distance meters behind the target
        //transform.position = target.position;
        //transform.position -= currentRotation * Vector3.forward * distance;

        //// Set the height of the camera
        //transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        //// Always look at the target
        //transform.LookAt(target);

    }
}
